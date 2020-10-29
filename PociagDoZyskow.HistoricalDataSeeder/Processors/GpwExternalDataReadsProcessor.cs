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
using PociagDoZyskow.ExternalDataHandler.QuotationsReaders;
using PociagDoZyskow.HistoricalDataSeeder.Factories;
using PociagDoZyskow.HistoricalDataSeeder.Processors.Interfaces;

namespace PociagDoZyskow.HistoricalDataSeeder.Processors
{
    class GpwExternalDataReadsProcessor : IProcessor
    {
        private readonly GpwQuotationsReader _gpwQuotationsReader;

        private readonly GpwCompanyDataScanEntityFactory _gpwCompanyDataScanEntityFactory;

        private readonly ExternalDataReadsContext _externalDataReadsContext; 

        private readonly DatabaseContext _databaseContext;

        public GpwExternalDataReadsProcessor(
            GpwQuotationsReader gpwQuotationsReader,
            GpwCompanyDataScanEntityFactory gpwCompanyDataScanEntityFactory,
            ExternalDataReadsContext externalDataReadsContext, 
            DatabaseContext databaseContext)
        {
            _gpwQuotationsReader = gpwQuotationsReader;
            _gpwCompanyDataScanEntityFactory = gpwCompanyDataScanEntityFactory;
            _externalDataReadsContext = externalDataReadsContext;
            _databaseContext = databaseContext;
        }

        public async Task Start(int fromDaysAgo)
        {
            try
            {
                var date = DateTime.Now.Subtract(TimeSpan.FromDays(fromDaysAgo));
                var processingDate = date.Date;
                var exchanges = _databaseContext.Exchanges.ToList();
                while (processingDate < DateTime.Now)
                {
                    var dailyQuotationReads =
                        (await _gpwQuotationsReader.GetCompanyDailyDataScans(processingDate)).ToList();

                    var newCompanies = CreateCompanyForScans(_databaseContext, dailyQuotationReads);
                    await _databaseContext.Companies.AddRangeAsync(newCompanies);
                    await _databaseContext.SaveChangesAsync();

                    var companies = _databaseContext.Companies
                        .Include(c => c.Exchange)
                        .ToList();

                    var quotationEntities =
                        _gpwCompanyDataScanEntityFactory.GetCompanyDataScanEntity(companies, exchanges, dailyQuotationReads).ToList();

                    var freshQuotationEntities =
                        RemoveFromAlreadyInsertedDataScans(_externalDataReadsContext, quotationEntities).ToList();

                    await _externalDataReadsContext.CompanyDataScans.AddRangeAsync(freshQuotationEntities);
                    await _externalDataReadsContext.SaveChangesAsync();
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
            var existingCompanies = context.Companies.ToList();
            foreach (DTO.CompanyDataScan scan in dailyQuotationReads)
            {
                //TODO: Refactor to:  exchanges.FirstOrDefault(e => e.Companies.Any(r => r.Name == companyEntity.FullCompanyName));
                var company = existingCompanies.FirstOrDefault(c => c.ShortName == scan.CompanyShortName);
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
