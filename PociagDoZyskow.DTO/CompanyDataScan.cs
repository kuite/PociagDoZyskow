using System;

namespace PociagDoZyskow.DTO
{
    public class CompanyDataScan
    {
        public DateTime ScanReferenceTime { get; set; }

        public decimal OpenPrice { get; set; }

        public decimal LowestPrice { get; set; }

        public decimal HighestPrice { get; set; }

        public decimal LastPrice { get; set; }

        public decimal ChangePrice { get; set; }

        public int TotalTransactionVolumeStockCount { get; set; }

        public decimal TotalTransactionValue { get; set; }

        public int TransactionsCount { get; set; }

        public string CompanyTicker { get; set; }

        public string ExchangeShortName { get; set; }
    }
}
