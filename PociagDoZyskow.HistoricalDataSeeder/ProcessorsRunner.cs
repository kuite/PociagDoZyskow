using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using PociagDoZyskow.DataAccess.Contexts;
using PociagDoZyskow.DataAccess.Entities;
using PociagDoZyskow.ExternalDataReader.Reports;
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
