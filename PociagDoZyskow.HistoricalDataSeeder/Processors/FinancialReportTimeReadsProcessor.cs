﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PociagDoZyskow.DataAccess.Contexts;
using PociagDoZyskow.DataAccess.Entities;
using PociagDoZyskow.ExternalDataReader.Reports;
using PociagDoZyskow.HistoricalDataSeeder.Factories;
using PociagDoZyskow.HistoricalDataSeeder.Processors.Interfaces;

namespace PociagDoZyskow.HistoricalDataSeeder.Processors
{
    public class FinancialReportTimeReadsProcessor : IDataSeedProcessor
    {
        public async Task Start()
        {
            try
            {
                Console.WriteLine("Start FinancialReportTimeReadsProcessor");
                var client = new WebClient();
                var reportReader = new FinancialReportTimeReader(client);
                var financialReports = await reportReader.GetPublishedFinancialReportTimeScans();
                Console.WriteLine($"Read {financialReports.Count()} published reports.");

                Console.WriteLine($"Transform published reports newScans to database entities.");
                var reportScans = new List<FinancialReportTimeScan>();
                var context = new DatabaseContext();
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<DTO.FinancialReportTimeScan, FinancialReportTimeScan>().ReverseMap();
                });
                IMapper iMapper = config.CreateMapper();
                var echanges = context.StockExchanges.ToList();
                foreach (DTO.FinancialReportTimeScan financialReportTimeDataScan in financialReports)
                {
                    var financialReportFactory = new FinancialReportTimeScanFactory(iMapper);
                    var reportEntity = financialReportFactory.GetFinancialReportTimeScanEntity(echanges, financialReportTimeDataScan);
                    reportScans.Add(reportEntity);
                }
                Console.WriteLine($"Transformed reports newScans to database entities.");

                Console.WriteLine("Prepare data to avoid duplications or data errors.");
                reportScans = FlushFromAlreadyInsertedDataScans(context, reportScans).ToList();
                await context.FinancialReportTimeDataScans.AddRangeAsync(reportScans);
                await context.SaveChangesAsync();
                Console.WriteLine($"Saved {reportScans.Count} financial reports time date scan to database.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        private IEnumerable<FinancialReportTimeScan> FlushFromAlreadyInsertedDataScans(DatabaseContext context, List<FinancialReportTimeScan> newScans)
        {
            var freshFinancialReportTimeScanEntities = new List<FinancialReportTimeScan>();
            var existedFinancialReportScans = context.FinancialReportTimeDataScans.ToList();
            var companies = context.Companies.ToList();

            foreach (FinancialReportTimeScan scan in newScans)
            {
                if (existedFinancialReportScans.Any(r =>
                    r.ShortCompanyName == scan.ShortCompanyName && 
                    r.ReportDate == scan.ReportDate))
                {
                    continue;
                }

                var cleanedScan = AssignAlreadyInsertedCompany(companies, scan);
                freshFinancialReportTimeScanEntities.Add(cleanedScan);
            }

            return freshFinancialReportTimeScanEntities;
        }

        private static FinancialReportTimeScan AssignAlreadyInsertedCompany(List<Company> companies, FinancialReportTimeScan scan)
        {
            var financialReportValidScan = scan;
            var relatedCompany = companies.FirstOrDefault(c => c.ShortName == scan.ShortCompanyName);
            if (relatedCompany != null)
            {
                financialReportValidScan.Company = relatedCompany;
            }



            return financialReportValidScan;
        }

    }
}