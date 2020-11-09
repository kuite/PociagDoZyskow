using System;
using System.Linq;
using System.Threading.Tasks;
using PociagDoZyskow.Services.QuotationsReaders;
using PociagDoZyskow.Services.WriteServices;
using PociagDoZyskow.HistoricalDataSeeder.Processors.Interfaces;
using PociagDoZyskow.HistoricalDataSeeder.Services.Interfaces;
using PociagDoZyskow.Services.Interfaces;

namespace PociagDoZyskow.HistoricalDataSeeder.Processors
{
    class GpwQuotationsDataProcessor : IProcessor
    {
        private const string ExchangeShortName = "GPW";

        private readonly GpwQuotationsReader _gpwQuotationsReader;

        private readonly GpwQuotationsWriter _gpwWriteServices;

        private readonly ICompanyService _companyService;
        private readonly ICompanyCreateService _companyCreateService;

        public GpwQuotationsDataProcessor(
            GpwQuotationsReader gpwQuotationsReader,
            GpwQuotationsWriter gpwWriteServices, 
            ICompanyService companyService)
        {
            _gpwQuotationsReader = gpwQuotationsReader;
            _gpwWriteServices = gpwWriteServices;
            _companyService = companyService;
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
                    var relatedCompaniesDto = _companyService.CreateCompaniesFromQuotationsScans(dailyQuotationReads);
                    var relatedCompaniesEntities = await _companyService.SaveCompaniesToDatabase(relatedCompaniesDto, ExchangeShortName);
                    var quotationReadEntities = 
                        await _gpwWriteServices.SaveQuotationDataScansToDatabase(dailyQuotationReads, relatedCompaniesEntities);
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
