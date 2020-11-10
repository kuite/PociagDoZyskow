using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PociagDoZyskow.DataAccess.Entities;
using PociagDoZyskow.HtmlReportsGenerator.Factories.Interfaces;
using PociagDoZyskow.Services.Interfaces;
using PociagDoZyskow.Services.Interfaces.CRUD;

namespace PociagDoZyskow.HtmlReportsGenerator
{
    public class HtmlReportsGenerator : IHtmlReportsGenerator
    {
        private ITrendsBeforeFinancialReportsReportFactory _trendsBeforeFinancialReportsReportFactory;

        private IReadService<Exchange> _getExchangeService;

        public HtmlReportsGenerator(ITrendsBeforeFinancialReportsReportFactory trendsBeforeFinancialReportsReportFactory)
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
