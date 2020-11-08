using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PociagDoZyskow.HtmlReportsGenerator
{
    public interface IHtmlReportGenerator
    {
        Task<IEnumerable<string>> GenerateReports(int daysFromNowToProcess, string exchangeShortName);
    }
}
