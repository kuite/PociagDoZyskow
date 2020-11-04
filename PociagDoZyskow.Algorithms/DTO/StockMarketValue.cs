using System;
using System.Collections.Generic;
using System.Text;

namespace PociagDoZyskow.Algorithms.DTO
{
    public class StockMarketValue
    {
        public decimal TotalTransactionValue { get; set; }

        public decimal ChangePrice { get; set; }

        public DateTime ScanDateTime { get; set; }
    }
}
