using System.Collections.Generic;
using PociagDoZyskow.DataAccess.Entities.Algorithms;

namespace PociagDoZyskow.DataAccess.Entities
{
    public class Company
    {
        public int Id { get; set; }

        public string Ticker { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public int ExchangeId { get; set; }

        public Exchange Exchange { get; set; }

        public ICollection<CompanyDataScan> CompanyDataScans { get; set; }

        public ICollection<AlgorithmResult> AlgorithmResults { get; set; }

        public ICollection<FinancialReportTimeScan> FinancialReportTimeScans { get; set; }
    }
}
