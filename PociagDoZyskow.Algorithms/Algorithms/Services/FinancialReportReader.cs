using System.Collections.Generic;
using System.Threading.Tasks;
using PociagDoZyskow.Algorithms.Model;

namespace PociagDoZyskow.Algorithms.Services
{
    public class FinancialReportReader : IFinancialReportReader
    {
        public Task<IEnumerable<FinancialReportResult>> GetReports()
        {
            // read from https://strefainwestorow.pl/dane/raporty/lista-dat-publikacji-raportow-okresowych/wszystkie
            throw new System.NotImplementedException();
        }
    }
}
