using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PociagDoZyskow.HtmlReportsGenerator.Factories.Interfaces;

namespace PociagDoZyskow.HtmlReportsGenerator.Factories
{
    public class TrendsBeforeFinancialReportsReportFactory : ITrendsBeforeFinancialReportsReportFactory
    {
        public Task<IEnumerable<string>> CreateHtmlReports(int daysFromNowToIncludeReports, string exchangeShortName)
        {
            throw new NotImplementedException();
        }
    }
}
