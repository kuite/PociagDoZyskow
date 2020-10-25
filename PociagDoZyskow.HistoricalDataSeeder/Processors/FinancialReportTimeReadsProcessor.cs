using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PociagDoZyskow.DataAccess.Contexts;
using PociagDoZyskow.DataAccess.Entities;
using PociagDoZyskow.ExternalDataReader.Reports;
using PociagDoZyskow.HistoricalDataSeeder.Factories;
using PociagDoZyskow.HistoricalDataSeeder.Processors.Interfaces;

namespace PociagDoZyskow.HistoricalDataSeeder.Processors
{
    public class FinancialReportTimeReadsProcessor : IDataSeedProcessor
    {
        public async Task Start(int fromDayAgo)
        {
            try
            {
                Console.WriteLine("Start FinancialReportTimeReadsProcessor");
                Console.WriteLine("Skipping use of fromDayAgo, as reading only visible financial reports.");
                var client = new WebClient();
                var reportReader = new FinancialReportTimeReader(client);
                var publishedFinancialReports = await reportReader.GetPublishedFinancialReportTimeScans();
                Console.WriteLine($"Read {publishedFinancialReports.Count()} published reports.");
                var incomingFinancialReports = await reportReader.GetIncomingFinancialReportTimeScans();
                Console.WriteLine($"Read {incomingFinancialReports.Count()} incoming reports.");

                Console.WriteLine($"Transform published reports newScans to database entities.");
                var reportScans = new List<FinancialReportTimeScan>();
                var context = new DatabaseContext();
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<DTO.FinancialReportTimeScan, FinancialReportTimeScan>().ReverseMap();
                });
                IMapper iMapper = config.CreateMapper();
                var financialReports = new List<DTO.FinancialReportTimeScan>();
                financialReports.AddRange(publishedFinancialReports);
                financialReports.AddRange(incomingFinancialReports);
                foreach (DTO.FinancialReportTimeScan financialReportTimeDataScan in financialReports)
                {
                    var company = context.Companies.Include(c => c.Exchange)
                        .FirstOrDefault(c =>
                        c.Ticker == financialReportTimeDataScan.CompanyTicker);
                    if (company == null)
                    {
                        //var x = 5;
                        //throw new Exception("Not found company for financialReportScan");
                        Console.WriteLine($"Not found company {financialReportTimeDataScan.FullCompanyName} for financialReportScan");
                        break;
                    }
                    var exchange = company.Exchange;
                    var financialReportFactory = new FinancialReportTimeScanEntityFactory(iMapper);
                    var reportEntity = financialReportFactory.GetFinancialReportTimeScanEntity(exchange, financialReportTimeDataScan);
                    reportScans.Add(reportEntity);
                }
                Console.WriteLine($"Transformed reports newScans to database entities.");

                Console.WriteLine("Prepare data to avoid duplications or data errors.");
                reportScans = RemoveDuplications(context, reportScans).ToList();
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

        private IEnumerable<FinancialReportTimeScan> RemoveDuplications(DatabaseContext context, List<FinancialReportTimeScan> newScans)
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
