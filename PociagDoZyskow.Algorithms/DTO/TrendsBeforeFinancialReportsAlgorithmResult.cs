using System;
using System.Collections.Generic;

namespace PociagDoZyskow.Algorithms.DTO
{
    public class TrendsBeforeFinancialReportsAlgorithmResult
    {
        public string CompanyShortName { get; set; }

        public string ResultDescription { get; set; }

        public bool IsBuy { get; set; }

        public DateTime FinancialReportTime { get; set; }

        public IEnumerable<StockMarketValue> StockMarketValues { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
