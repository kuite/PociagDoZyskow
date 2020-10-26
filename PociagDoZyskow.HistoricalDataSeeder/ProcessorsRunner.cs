using System;
using System.Threading.Tasks;
using PociagDoZyskow.HistoricalDataSeeder.Processors;

namespace PociagDoZyskow.HistoricalDataSeeder
{
    class ProcessorsRunner
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter days to include company scans...");
            int intTemp = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"GpwExternalDataReadsProcessor start from {intTemp} days ago");
            var gpwQuotationsProcessor = new GpwExternalDataReadsProcessor();
            await gpwQuotationsProcessor.Start(intTemp);
            Console.WriteLine("GpwExternalDataReadsProcessor ended");

            Console.WriteLine($"FinancialReportTimeReadsProcessor start");
            var reportsProcessor = new FinancialReportTimeReadsProcessor();
            await reportsProcessor.Start(0);
            Console.WriteLine("FinancialReportTimeReadsProcessor ended");


            Console.WriteLine("ProcessorsRunner ended");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
