using System.Collections.Generic;
using System.Threading.Tasks;
using PociagDoZyskow.DTO;

namespace PociagDoZyskow.ExternalDataReader.Reports.Interfaces
{
    public interface IFinancialReportTimeReader
    {
        Task<IEnumerable<FinancialReportTimeScan>> GetIncomingFinancialReportTimeScans();

        Task<IEnumerable<FinancialReportTimeScan>> GetPublishedFinancialReportTimeScans();
    }
}
