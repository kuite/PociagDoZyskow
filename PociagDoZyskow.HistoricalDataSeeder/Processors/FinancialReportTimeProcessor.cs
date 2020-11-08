using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PociagDoZyskow.ExternalDataHandler.ReportsReaders;
using PociagDoZyskow.ExternalDataHandler.ReportsWritter;
using PociagDoZyskow.HistoricalDataSeeder.Processors.Interfaces;
using PociagDoZyskow.HistoricalDataSeeder.Services.Interfaces;

namespace PociagDoZyskow.HistoricalDataSeeder.Processors
{
    public class FinancialReportTimeProcessor : IProcessor
    {
        private const string _exchangeShortName = "GPW";
        private readonly FinancialReportTimeReader _financialReportTimeReader;
        private readonly FinancialReportTimeWriter _financialReportTimeWriter;
        private readonly IFinancialReportService _financialReportService;

        public FinancialReportTimeProcessor(
            FinancialReportTimeReader financialReportTimeReader,
            FinancialReportTimeWriter financialReportTimeWriter,
            IFinancialReportService financialReportService
            )
        {
            _financialReportTimeReader = financialReportTimeReader;
            _financialReportTimeWriter = financialReportTimeWriter;
            _financialReportService = financialReportService;
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

                var relatedCompaniesDto = _financialReportService.CreateCompaniesFromReportScans(financialReports);
                var relatedCompaniesEntities = await _financialReportService.SaveCompaniesToDatabase(relatedCompaniesDto, _exchangeShortName);
                var savedReportScans =
                        await _financialReportTimeWriter.SaveReportDataScansToDatabase(financialReports, relatedCompaniesEntities);
                var updatedCompanies = await _financialReportService.UpdateCompaniesFromReports(financialReports);
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
