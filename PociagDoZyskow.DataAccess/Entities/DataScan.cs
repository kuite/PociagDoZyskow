using System;

namespace PociagDoZyskow.DataAccess.Entities
{
    public class DataScan
    {
        public int Id { get; set; }

        public DateTime ScanTime { get; set; }

        public DateTime LastTransactionTime { get; set; }

        public decimal ReferencePrice { get; set; }

        public decimal OpenPrice { get; set; }

        public decimal LowestPrice { get; set; }

        public decimal HighestPrice { get; set; }

        public decimal LastPrice { get; set; }

        public int LastTransactionVolume { get; set; }

        public int TotalTransactionVolume { get; set; }

        public int TransactionsCount { get; set; }

        public int CompanyId { get; set; }

        public Company Company { get; set; }
    }
}
