using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PociagDoZyskow.HtmlReportsGenerator.Factories.Interfaces;

namespace PociagDoZyskow.HtmlReportsGenerator
{
    public class HtmlReportGenerator : IHtmlReportGenerator
    {
        private ITrendsBeforeFinancialReportsReportFactory _trendsBeforeFinancialReportsReportFactory;

        public HtmlReportGenerator(ITrendsBeforeFinancialReportsReportFactory trendsBeforeFinancialReportsReportFactory)
        {
            _trendsBeforeFinancialReportsReportFactory = trendsBeforeFinancialReportsReportFactory;
        }

        public Task<IEnumerable<string>> GenerateReports(int daysFromNowToProcess, string exchangeShortName)
        {
            //get all reports factories
            //get stock exchanges
            //get every reports for selected exchange
            //return reports

            throw new NotImplementedException();
        }
    }
}
