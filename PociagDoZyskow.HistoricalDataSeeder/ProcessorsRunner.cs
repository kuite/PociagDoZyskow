using System;
using System.Threading.Tasks;
using PociagDoZyskow.HistoricalDataSeeder.Processors;

namespace PociagDoZyskow.HistoricalDataSeeder
{
    class ProcessorsRunner
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("FinancialReportTimeReadsProcessor start");
            var reportsProcessor = new FinancialReportTimeReadsProcessor();
            await reportsProcessor.Start();
            Console.WriteLine("FinancialReportTimeReadsProcessor ended");
            Console.WriteLine("GpwExternalDataReadsProcessor start");
            var gpwQuotationsProcessor = new GpwExternalDataReadsProcessor();
            await gpwQuotationsProcessor.Start();
            Console.WriteLine("GpwExternalDataReadsProcessor ended");

            Console.WriteLine("ProcessorsRunner ended");
        }
    }
}
