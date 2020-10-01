using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PociagDoZyskow.DTO;

namespace PociagDoZyskow.QuotationsReader.Reports.Interfaces
{
    public interface IFinancialReportTimeReader
    {
        Task<IEnumerable<FinancialReportTimeDataScan>> GetFinancialReportTimeScans(DateTime date);
    }
}
