using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PociagDoZyskow.HtmlReportsGenerator.DependencyInjection;

namespace PociagDoZyskow.HtmlReportsGenerator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = ServiceResolver.GetProvider();

            Console.WriteLine("Enter days to include financial reports, eg. 10 means program will take financial reports up to 10 days from today");
            int fromDaysAgo = Convert.ToInt32(Console.ReadLine());

            var htmlReportGeneratorProcessor = services.GetService<IHtmlReportGenerator>();
            var newReports = await htmlReportGeneratorProcessor.GenerateReports(fromDaysAgo, "GPW");
        }
    }
}
