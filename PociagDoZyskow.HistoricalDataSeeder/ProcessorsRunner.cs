﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PociagDoZyskow.HistoricalDataSeeder.Processors;
using static PociagDoZyskow.HistoricalDataSeeder.DependencyInjection.ServiceResolver;

namespace PociagDoZyskow.HistoricalDataSeeder
{
    class ProcessorsRunner
    {
        static async Task Main(string[] args)
        {
            var services = GetProvider();

            Console.WriteLine("Enter days to include company scans...");
            int fromDaysAgo = Convert.ToInt32(Console.ReadLine());

            var gpwQuotationsProcessor = services.GetService<GpwQuotationsDataProcessor>();
            await gpwQuotationsProcessor.Start(fromDaysAgo);

            var financialReportsProcessor = services.GetService<FinancialReportTimeProcessor>();
            await financialReportsProcessor.Start(0);


            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
