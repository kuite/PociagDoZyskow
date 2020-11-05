using System;
using System.Collections.Generic;

namespace PociagDoZyskow.Algorithms.DTO
{
    public class TrendsBeforeFinancialReportsAlgorithmResult
    {
        public string CompanyShortName { get; set; }

        public DateTime FinancialReportTime { get; set; }

        public IEnumerable<StockMarketValue> StockMarketValues { get; set; }
    }
}
