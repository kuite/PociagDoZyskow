using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PociagDoZyskow.HtmlReportsGenerator.Factories.Interfaces
{
    public interface IHtmlReportFactory
    {
        Task<IEnumerable<string>> CreateHtmlReports(int daysFromNowToIncludeReports, string exchangeShortName);
    }
}
