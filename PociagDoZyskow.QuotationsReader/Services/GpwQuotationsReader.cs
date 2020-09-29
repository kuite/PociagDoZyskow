using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using PociagDoZyskow.DTO;
using PociagDoZyskow.QuotationsReader.Services.Interfaces;

namespace PociagDoZyskow.QuotationsReader.Services
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
        private readonly int TotalTransactionVolumeIndex = 8;
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

            foreach (HtmlNode table in doc.DocumentNode.SelectNodes("/html/body/section[2]/table"))
            {
                 
                foreach (HtmlNode row in table.SelectNodes("tr"))
                {
                    var companyDataScan = new CompanyDataScan();
                    var cells = row.SelectNodes("th|td");
                    companyDataScan.CompanyName = cells[CompanyNameIndex].InnerText;
                    companyDataScan.OpenPrice = decimal.Parse(cells[OpenPriceIndex].InnerText);
                    companyDataScan.HighestPrice = decimal.Parse(cells[HighestPriceIndex].InnerText);
                    companyDataScan.LowestPrice = decimal.Parse(cells[LowestPriceIndex].InnerText);
                    companyDataScan.LastPrice = decimal.Parse(cells[LastPriceIndex].InnerText);
                    companyDataScan.ChangePrice = decimal.Parse(cells[ChangePriceIndex].InnerText);
                    companyDataScan.TotalTransactionVolume = int.Parse(cells[TotalTransactionVolumeIndex].InnerText);
                    companyDataScan.TransactionsCount = int.Parse(cells[TransactionCountIndex].InnerText);
                    companyDataScan.TotalTransactionValue = int.Parse(cells[TotalTransactionValueIndex].InnerText);

                    scans.Add(companyDataScan);
                }
            }

            return scans;
        }
    }
}
