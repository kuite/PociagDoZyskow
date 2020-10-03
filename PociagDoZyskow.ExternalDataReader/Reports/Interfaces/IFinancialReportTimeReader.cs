using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PociagDoZyskow.DTO;

namespace PociagDoZyskow.ExternalDataReader.Reports.Interfaces
{
    public interface IFinancialReportTimeReader
    {
        Task<IEnumerable<FinancialReportTimeDataScan>> GetAllIncomingFinancialReportTimeScans();

        Task<IEnumerable<FinancialReportTimeDataScan>> GetPublishedFinancialReportTimeScans();
    }
}
