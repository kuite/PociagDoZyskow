using System;

namespace PociagDoZyskow.Model
{
    public class CompanyDataScan
    {
        public DateTime LastTransactionTime {get;set;}
        public decimal ReferencePrice {get;set;}
        public decimal OpenPrice {get;set;}
        public decimal LowestPrice {get;set;}
        public decimal HighestPrice {get;set;}
        public decimal LastPrice {get;set;}
        public decimal PercentageChageToReferencePrice {get;set;}
        public int LastTransactionVolume {get;set;}
        public int TotalTransactionVolume {get;set;}
        public int TransactionsCount {get;set;}
    }
}
