using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using HtmlAgilityPack;
using PociagDoZyskow.DTO;
using PociagDoZyskow.QuotationsReader.Helpers;
using PociagDoZyskow.QuotationsReader.Quotations.Interfaces;

namespace PociagDoZyskow.QuotationsReader.Quotations
{
    public class GpwQuotationsReader : IQuotationsReader
    {
        private readonly string GpwBaseUrl = "https://www.gpw.pl/archiwum-notowan-full?type=10&instrument=&date=";
        private readonly int CompanyNameIndex = 0;
        private readonly int OpenPriceIndex = 3;
        private readonly int HighestPriceIndex = 4;
        private readonly int LowestPriceIndex = 5;
        private readonly int LastPriceIndex = 6; 
        private readonly int ChangePriceIndex = 7; 
        private readonly int TotalTransactionVolumeStockCountIndex = 8;
        private readonly int TransactionCountIndex = 9; 
        private readonly int TotalTransactionValueIndex = 10;
        private readonly WebClient _client;

        public GpwQuotationsReader(WebClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<CompanyDataScan>> GetExchangeScans(DateTime date)
        {
            String formattedDate = date.ToString("dd-MM-yyyy");
            var filledUrl = String.Concat(GpwBaseUrl, formattedDate);
            var result = _client.DownloadString(filledUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(result);

            var scans = new List<CompanyDataScan>();
            var scanTime = DateTime.Now;

            foreach (HtmlNode table in doc.DocumentNode.SelectNodes("/html/body/section[2]/table"))
            {
                 
                foreach (HtmlNode row in table.SelectNodes("tr"))
                {
                    var companyDataScan = new CompanyDataScan();
                    var cells = row.SelectNodes("th|td");
                    companyDataScan.ScanTime = scanTime;
                    companyDataScan.CompanyName = cells[CompanyNameIndex].InnerText.CleanString();
                    companyDataScan.OpenPrice = decimal.Parse(cells[OpenPriceIndex].InnerText.CleanString());
                    companyDataScan.HighestPrice = decimal.Parse(cells[HighestPriceIndex].InnerText.CleanString());
                    companyDataScan.LowestPrice = decimal.Parse(cells[LowestPriceIndex].InnerText.CleanString());
                    companyDataScan.LastPrice = decimal.Parse(cells[LastPriceIndex].InnerText.CleanString());
                    companyDataScan.ChangePrice = decimal.Parse(cells[ChangePriceIndex].InnerText.CleanString());
                    companyDataScan.TotalTransactionVolumeStockCount = int.Parse(cells[TotalTransactionVolumeStockCountIndex].InnerText.CleanString());
                    companyDataScan.TransactionsCount = int.Parse(cells[TransactionCountIndex].InnerText.CleanString());
                    companyDataScan.TotalTransactionValue = decimal.Parse(cells[TotalTransactionValueIndex].InnerText.CleanString()) * 1000;

                    scans.Add(companyDataScan);
                }
            }

            return scans;
        }
    }
}
