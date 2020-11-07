using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PociagDoZyskow.HtmlReportsGenerator
{
    public class HtmlReportGeneratorProcessor : IHtmlReportGeneratorProcessor
    {

        public Task ProcessReports(int daysFromNowToProcess)
        {
            //get financial reports (potrzebne tylko do jednego typu projektu, czyli kazdy raport w osobnej klasie
            throw new NotImplementedException();
        }
    }
}
