using System;

namespace PociagDoZyskow.DataAccess.Entities.ExternalDataReads
{
    public class FinancialReportTimeScan
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public string CompanyTicker { get; set; }

        public string ShortCompanyName { get; set; }

        public string FullCompanyName { get; set; }

        public string ReportType { get; set; }

        public DateTime ReportDate { get; set; }
    }
}
