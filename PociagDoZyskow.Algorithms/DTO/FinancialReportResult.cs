using System;

namespace PociagDoZyskow.Algorithms.DTO
{
    public class FinancialReportResult
    {
        public string CompanyFullName { get; set; }

        public string CompanyShortName { get; set; }

        public string Ticker { get; set; }

        public string Type { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
