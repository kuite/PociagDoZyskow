using System;
using System.Linq;
using System.Threading.Tasks;
using PociagDoZyskow.Services.QuotationsReaders;
using PociagDoZyskow.HistoricalDataSeeder.Processors.Interfaces;
using PociagDoZyskow.Services.Interfaces;

namespace PociagDoZyskow.HistoricalDataSeeder.Processors
{
    class GpwQuotationsDataProcessor : IProcessor
    {
        private const string ExchangeShortName = "GPW";

        private readonly GpwQuotationsReader _gpwQuotationsReader;

        private readonly ICreateCompanyDataScanService _createCompanyDataService;

        private readonly ICreateCompanyService _createCompanyService;

        public GpwQuotationsDataProcessor(
            GpwQuotationsReader gpwQuotationsReader,
            ICreateCompanyDataScanService createCompanyDataService, 
            ICreateCompanyService createCompanyService)
        {
            _gpwQuotationsReader = gpwQuotationsReader;
            _createCompanyDataService = createCompanyDataService;
            _createCompanyService = createCompanyService;
        }

        public async Task Start(int fromDaysAgo)
        {
            try
            {
                var date = DateTime.Now.Subtract(TimeSpan.FromDays(fromDaysAgo));
                var processingDate = date.Date;
                while (processingDate < DateTime.Now)
                {
                    var dailyQuotationReads = await _gpwQuotationsReader.GetQuotationDataScansForDate(processingDate);
                    var relatedCompaniesDto = _createCompanyService.CreateCompaniesFromQuotationsScans(dailyQuotationReads);
                    var relatedCompaniesEntities = await _createCompanyService.SaveCompaniesToDatabase(relatedCompaniesDto, ExchangeShortName);
                    var quotationReadEntities = 
                        await _createCompanyDataService.SaveQuotationDataScansToDatabase(dailyQuotationReads, relatedCompaniesEntities);
                    Console.WriteLine($"Saved {quotationReadEntities.Count()} quotations from {processingDate.ToShortDateString()} day to database.");

                    processingDate = processingDate.AddDays(1);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}
