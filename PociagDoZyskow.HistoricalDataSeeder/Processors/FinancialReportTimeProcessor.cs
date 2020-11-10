using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PociagDoZyskow.Services.ReportsReaders;
using PociagDoZyskow.HistoricalDataSeeder.Processors.Interfaces;
using PociagDoZyskow.Services.Interfaces;

namespace PociagDoZyskow.HistoricalDataSeeder.Processors
{
    public class FinancialReportTimeProcessor : IProcessor
    {
        private const string ExchangeShortName = "GPW";
        private readonly FinancialReportTimeReader _financialReportTimeReader;
        private readonly ICreateFinancialTimeReportService _createFinancialTimeReportService;
        private readonly ICreateCompanyService _createCompanyService;

        public FinancialReportTimeProcessor(
            FinancialReportTimeReader financialReportTimeReader,
            ICreateFinancialTimeReportService createFinancialTimeReportService, 
            ICreateCompanyService createCompanyService)
        {
            _financialReportTimeReader = financialReportTimeReader;
            _createFinancialTimeReportService = createFinancialTimeReportService;
            _createCompanyService = createCompanyService;
        }

        public async Task Start(int fromDaysAgo)
        {
            try
            {
                var publishedFinancialReports = await _financialReportTimeReader.GetPublishedFinancialReportTimeScans();
                var incomingFinancialReports = await _financialReportTimeReader.GetIncomingFinancialReportTimeScans();
                var financialReports = new List<DTO.FinancialReportTimeScan>();
                financialReports.AddRange(publishedFinancialReports);
                financialReports.AddRange(incomingFinancialReports);

                var relatedCompaniesDto = _createCompanyService.CreateCompaniesFromReportScans(financialReports);
                var relatedCompaniesEntities = await _createCompanyService.SaveCompaniesToDatabase(relatedCompaniesDto, ExchangeShortName);
                var savedReportScans =
                        await _createFinancialTimeReportService.SaveReportDataScansToDatabase(financialReports, relatedCompaniesEntities);
                var updatedCompanies = await _createCompanyService.UpdateCompaniesFromReports(financialReports);
                Console.WriteLine($"Updated {updatedCompanies.Count()} companies from financial reports.");
                Console.WriteLine($"Saved {savedReportScans.Count()} financial report time to database.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
