using System;
using System.Collections.Generic;
using System.Text;

namespace PociagDoZyskow.DataAccess.Entities
{
    public class FinancialReportTimeDataScan
    {
        public int Id { get; set; }

        public string Ticker { get; set; }

        public int CompanyId { get; set; }

        public Company Company { get; set; }

        public string ShortCompanyName { get; set; }

        public string FullCompanyName { get; set; }

        public string ReportType { get; set; }

        public DateTime ReportDate { get; set; }
    }
}
