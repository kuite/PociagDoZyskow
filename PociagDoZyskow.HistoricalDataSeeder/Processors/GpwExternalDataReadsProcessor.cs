using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PociagDoZyskow.DataAccess.Contexts;
using PociagDoZyskow.DataAccess.Entities;
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
                var context = new DatabaseContext();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<DTO.CompanyDataScan, CompanyDataScan>().ReverseMap();
                });

                var quotationsReader = new GpwQuotationsReader(client);
                var date = DateTime.Now.Subtract(TimeSpan.FromDays(fromDaysAgo));
                var processingDate = date.Date;
                IMapper iMapper = config.CreateMapper();
                var exchanges = context.Exchanges.ToList();
                var companies = context.Companies
                    .Include(c => c.Exchange)
                    .ToList();
                var quotationFactory = new GpwCompanyDataScanEntityFactory(iMapper);
                while (processingDate < DateTime.Now)
                {
                    companies = context.Companies
                        .Include(c => c.Exchange)
                        .ToList();
                    var dailyQuotationReads =
                        (await quotationsReader.GetCompanyDailyDataScans(processingDate)).ToList();
                    
                    var quotationEntities =
                        quotationFactory.GetCompanyDataScanEntity(companies, exchanges, dailyQuotationReads).ToList();

                    var freshQuotationEntities =
                        RemoveFromAlreadyInsertedDataScans(context, quotationEntities).ToList();

                    await context.CompanyDataScans.AddRangeAsync(freshQuotationEntities);
                    await context.SaveChangesAsync();
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

        private IEnumerable<CompanyDataScan> RemoveFromAlreadyInsertedDataScans(DatabaseContext context, List<CompanyDataScan> newScans)
        {
            var freshFCompanyDataScansEntities = new List<CompanyDataScan>();
            var existingCompanyDataScans = context.CompanyDataScans.ToList();
            var companies = context.Companies.ToList();

            foreach (CompanyDataScan scan in newScans)
            {
                if (existingCompanyDataScans.Any(r =>
                    r.Company.Ticker == scan.Company.Ticker &&
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
