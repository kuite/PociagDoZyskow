using System;

namespace PociagDoZyskow.DTO
{
    public class CompanyDataScan
    {
        public DateTime ScanTime { get; set; }

        public decimal OpenPrice { get; set; }

        public decimal LowestPrice { get; set; }

        public decimal HighestPrice { get; set; }

        public decimal LastPrice { get; set; }

        public decimal ChangePrice { get; set; }

        public int TotalTransactionVolumeStockCount { get; set; }

        public decimal TotalTransactionValue { get; set; }

        public int TransactionsCount { get; set; }

        public string CompanyName { get; set; }
    }
}
