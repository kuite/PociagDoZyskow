using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PociagDoZyskow.DataAccess.Contexts;
using PociagDoZyskow.DataAccess.Entities;
using PociagDoZyskow.DataAccess.Entities.ExternalDataReads;
using PociagDoZyskow.ExternalDataReader.QuotationsReaders;
using PociagDoZyskow.HistoricalDataSeeder.Factories;
using PociagDoZyskow.HistoricalDataSeeder.Processors.Interfaces;

namespace PociagDoZyskow.HistoricalDataSeeder.Processors
{
    class GpwExternalDataReadsProcessor : IDataSeedProcessor
    {
        public async Task Start(int fromDaysAgo)
        {
            try
            {
                var client = new WebClient();
                var externalDataReadsContext = new ExternalDataReadsContext();
                var context = new DatabaseContext();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<DTO.CompanyDataScan, CompanyDataScan>().ReverseMap();
                });
                var quotationsReader = new GpwQuotationsReader(client);
                IMapper iMapper = config.CreateMapper();

                var date = DateTime.Now.Subtract(TimeSpan.FromDays(fromDaysAgo));
                var processingDate = date.Date;
                var exchanges = context.Exchanges.ToList();
                var companies = context.Companies
                    .Include(c => c.Exchange)
                    .ToList();
                var quotationFactory = new GpwCompanyDataScanEntityFactory(iMapper);
                while (processingDate < DateTime.Now)
                {
                    var dailyQuotationReads =
                        (await quotationsReader.GetCompanyDailyDataScans(processingDate)).ToList();

                    var newCompanies = CreateCompanyForScans(context, dailyQuotationReads);
                    await context.Companies.AddRangeAsync(newCompanies);
                    await context.SaveChangesAsync();

                    companies = context.Companies
                        .Include(c => c.Exchange)
                        .ToList();

                    var quotationEntities =
                        quotationFactory.GetCompanyDataScanEntity(companies, exchanges, dailyQuotationReads).ToList();

                    var freshQuotationEntities =
                        RemoveFromAlreadyInsertedDataScans(externalDataReadsContext, quotationEntities).ToList();

                    await externalDataReadsContext.CompanyDataScans.AddRangeAsync(freshQuotationEntities);
                    await externalDataReadsContext.SaveChangesAsync();
                    Console.WriteLine($"Saved {freshQuotationEntities.Count} quotations from {processingDate.ToShortDateString()} day to database...");
                    processingDate = processingDate.AddDays(1);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private IEnumerable<Company> CreateCompanyForScans(DatabaseContext context, List<DTO.CompanyDataScan> dailyQuotationReads)
        {
            var companies = new List<Company>();
            var exsitingCompanies = context.Companies.ToList();
            foreach (DTO.CompanyDataScan scan in dailyQuotationReads)
            {
                //TODO: Refactor to:  exchanges.FirstOrDefault(e => e.Companies.Any(r => r.Name == companyEntity.FullCompanyName));
                var company = exsitingCompanies.FirstOrDefault(c => c.ShortName == scan.CompanyShortName);
                var exchange = context.Exchanges.FirstOrDefault(e => e.ShortName == "GPW");
                if (exchange == null)
                {
                    throw new Exception("Not found GPW exchange.");
                }
                if (company == null)
                {
                    //TODO: Save company  
                    company = new Company();
                    company.Exchange = exchange;
                    company.ShortName = scan.CompanyShortName;
                    companies.Add(company);
                }
            }

            return companies;
        }

        private IEnumerable<CompanyDataScan> RemoveFromAlreadyInsertedDataScans(ExternalDataReadsContext externalDataReadsContext, List<CompanyDataScan> newScans)
        {
            var freshFCompanyDataScansEntities = new List<CompanyDataScan>();
            var existingCompanyDataScans = externalDataReadsContext.CompanyDataScans.ToList();

            foreach (CompanyDataScan scan in newScans)
            {
                if (existingCompanyDataScans.Any(r =>
                    r.CompanyId == scan.CompanyId &&
                    r.ScanReferenceTime == scan.ScanReferenceTime))
                {
                    continue;
                }

                //var cleanedScan = AssignAlreadyInsertedCompany(companies, scan);
                freshFCompanyDataScansEntities.Add(scan);
            }

            return freshFCompanyDataScansEntities;
        }
    }
}
