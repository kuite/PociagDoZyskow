using System;

namespace PociagDoZyskow.DataAccess.Entities
{
    public class CompanyDataScan
    {
        public int Id { get; set; }

        public DateTime ScanTime { get; set; }

        public decimal ReferencePrice { get; set; }

        public decimal OpenPrice { get; set; }

        public decimal LowestPrice { get; set; }

        public decimal HighestPrice { get; set; }

        public decimal LastPrice { get; set; }

        public decimal ChangePrice { get; set; }

        public int TotalTransactionVolume { get; set; }

        public decimal TotalTransactionValue { get; set; }

        public int TransactionsCount { get; set; }

        public int CompanyId { get; set; }

        public Company Company { get; set; }
    }
}
