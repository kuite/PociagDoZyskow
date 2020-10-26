using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using HtmlAgilityPack;
using PociagDoZyskow.DTO;
using PociagDoZyskow.ExternalDataReader.Helpers;
using PociagDoZyskow.ExternalDataReader.Reports.Interfaces;

namespace PociagDoZyskow.ExternalDataReader.Reports
{
    public class FinancialReportTimeReader : IFinancialReportTimeReader
    {
        private readonly string PublishedReportsBaseUrl =
            "https://strefainwestorow.pl/dane/raporty/lista-dat-publikacji-raportow-okresowych/";

        private readonly string FutureReportsBaseUrl =
            "https://www.money.pl/gielda/raporty/";


        private readonly int TickerIndex = 0;
        private readonly int ShortCompanyNameIndex = 1;
        private readonly int FullCompanyNameIndex = 2;
        private readonly int ReportDateIndex = 3;
        private readonly int ReportTypeNameIndex = 4;
        private readonly WebClient _client;

        public FinancialReportTimeReader(WebClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<FinancialReportTimeScan>> GetIncomingFinancialReportTimeScans()
        {
            //"https://www.money.pl/gielda/raporty/"
            var filledUrl = String.Concat(PublishedReportsBaseUrl, "wszystkie");
            var scans = GetFinancialScansFromUrl(filledUrl);

            return scans;
        }

        public async Task<IEnumerable<FinancialReportTimeScan>> GetPublishedFinancialReportTimeScans()
        {
            var filledUrl = String.Concat(PublishedReportsBaseUrl, "opublikowane");
            var scans = GetFinancialScansFromUrl(filledUrl);

            return scans;
        }

        private IEnumerable<FinancialReportTimeScan> GetFinancialScansFromUrl(string url)
        {
            var result = _client.DownloadString(url);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(result);

            var scans = new List<FinancialReportTimeScan>();

            foreach (HtmlNode table in doc.DocumentNode.SelectNodes("//*[@id=\"block-system-main\"]/table/tbody"))
            {

                foreach (HtmlNode row in table.SelectNodes("tr"))
                {
                    var reportDataScan = new FinancialReportTimeScan();
                    var cells = row.SelectNodes("th|td");
                    reportDataScan.CompanyTicker = cells[TickerIndex].InnerText.CleanString();
                    reportDataScan.ShortCompanyName = cells[ShortCompanyNameIndex].InnerText.CleanString();
                    reportDataScan.FullCompanyName = cells[FullCompanyNameIndex].InnerText;
                    reportDataScan.ReportType = cells[ReportTypeNameIndex].InnerText;
                    reportDataScan.ReportDate = DateTime.Parse(cells[ReportDateIndex].InnerText.CleanString());

                    scans.Add(reportDataScan);
                }
            }

            return scans;
        }
    }
}
