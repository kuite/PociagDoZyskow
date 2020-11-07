using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PociagDoZyskow.HtmlReportsGenerator
{
    public interface IHtmlReportGeneratorProcessor
    {
        Task ProcessReports(int daysFromNowToProcess);
    }
}
