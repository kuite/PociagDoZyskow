using System.Collections.Generic;
using System.Threading.Tasks;
using PociagDoZyskow.Algorithms.Model;

namespace PociagDoZyskow.Algorithms.Services
{
    public interface IFinancialReportReader
    {
        Task<IEnumerable<FinancialReportResult>> GetReports();
    }
}
