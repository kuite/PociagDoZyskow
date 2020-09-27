using System;
using System.Collections.Generic;
using PociagDoZyskow.DataAccess.Entities;

namespace PociagDoZyskow.Algorithms.Model
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
