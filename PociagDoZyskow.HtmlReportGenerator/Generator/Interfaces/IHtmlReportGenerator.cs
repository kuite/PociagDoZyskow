using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PociagDoZyskow.HtmlReportsGenerator.Generator.Interfaces
{
    public interface IHtmlReportGenerator
    {
        Task<IEnumerable<string>> GetHtmlReports(int daysFromNow);
    }
}
