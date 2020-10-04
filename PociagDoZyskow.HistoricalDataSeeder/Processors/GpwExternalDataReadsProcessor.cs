using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PociagDoZyskow.DataAccess.Contexts;
using PociagDoZyskow.DataAccess.Entities;
using PociagDoZyskow.ExternalDataReader.Quotations;
using PociagDoZyskow.ExternalDataReader.Reports;
using PociagDoZyskow.HistoricalDataSeeder.Factories;
using PociagDoZyskow.HistoricalDataSeeder.Processors.Interfaces;

namespace PociagDoZyskow.HistoricalDataSeeder.Processors
{
    class GpwExternalDataReadsProcessor : IDataSeedProcessor
    {
        public async Task Start()
        {
            try
            {
                Console.WriteLine("Start GpwExternalDataReadsProcessor");
                var client = new WebClient();
                var context = new DatabaseContext();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<DTO.CompanyDataScan, CompanyDataScan>().ReverseMap();
                });

                Console.WriteLine($"Transforming quotations scans to database entities...");
                var quotationsReader = new GpwQuotationsReader(client);
                var date = DateTime.Now.Subtract(TimeSpan.FromDays(180));
                var processingDate = date.Date;
                IMapper iMapper = config.CreateMapper();
                var exchanges = context.StockExchanges.ToList();
                var companies = context.Companies
                    .Include(c => c.Exchange)
                    .ToList();
                while (processingDate < DateTime.Now)
                {
                    companies = context.Companies
                        .Include(c => c.Exchange)
                        .ToList();
                    var dailyQuotationReads =
                        (await quotationsReader.GetCompanyDailyDataScans(processingDate)).ToList();
                    var quotationFactory = new GpwCompanyDataScanFactory(iMapper);

                    Console.WriteLine("Map quotation reads to entities.");
                    var quotationEntities =
                        quotationFactory.GetCompanyDataScanEntity(companies, exchanges, dailyQuotationReads).ToList();

                    Console.WriteLine("Prepare data to avoid duplications or data errors.");
                    var flushedQuotationEntities =
                        FlushFromAlreadyInsertedDataScans(context, quotationEntities).ToList();

                    await context.CompanyDataScans.AddRangeAsync(flushedQuotationEntities);
                    await context.SaveChangesAsync();
                    Console.WriteLine($"Saved {flushedQuotationEntities.Count} quotations from {processingDate} day to database...");
                    processingDate = processingDate.AddDays(1);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private IEnumerable<CompanyDataScan> FlushFromAlreadyInsertedDataScans(DatabaseContext context, List<CompanyDataScan> newScans)
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
