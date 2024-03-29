﻿using System;

namespace PociagDoZyskow.DataAccess.Entities.ExternalDataReads
{
    public class CompanyDataScan
    {
        public int Id { get; set; }

        public DateTime ScanReferenceTime { get; set; }

        public decimal ReferencePrice { get; set; }

        public decimal OpenPrice { get; set; }

        public decimal LowestPrice { get; set; }

        public decimal HighestPrice { get; set; }

        public decimal LastPrice { get; set; }

        public decimal ChangePrice { get; set; }

        public int TotalTransactionVolumeStockCount { get; set; }

        public decimal TotalTransactionValue { get; set; }

        public int TransactionsCount { get; set; }

        public int CompanyId { get; set; }
    }
}
