using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PociagDoZyskow.DTO;
using PociagDoZyskow.QuotationsReader.Reports.Interfaces;

namespace PociagDoZyskow.QuotationsReader.Reports
{
    public class FinancialReportTimeReader : IFinancialReportTimeReader
    {
        public async Task<IEnumerable<FinancialReportTimeDataScan>> GetFinancialReportTimeScans(DateTime date)
        {
            WebClient client = new WebClient();

            

            return null;
        }
    }
}
