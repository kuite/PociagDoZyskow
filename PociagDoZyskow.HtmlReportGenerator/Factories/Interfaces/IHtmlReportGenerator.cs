using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PociagDoZyskow.HtmlReportsGenerator.Factories.Interfaces
{
    public interface IHtmlReportGenerator
    {
        Task<IEnumerable<string>> GetHtmlReports();
    }
}
