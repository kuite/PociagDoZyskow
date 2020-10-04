using System;
using System.Collections.Generic;
using System.Text;

namespace PociagDoZyskow.DTO
{
    public class FinancialReportTimeScan
    {
        public string CompanyTicker { get; set; }

        public string ShortCompanyName { get; set; }

        public string FullCompanyName { get; set; }

        public string ReportType { get; set; }

        public DateTime ReportDate { get; set; }
    }
}
