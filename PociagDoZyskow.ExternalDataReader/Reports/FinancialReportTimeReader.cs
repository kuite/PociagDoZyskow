using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using PociagDoZyskow.DTO;
using PociagDoZyskow.ExternalDataReader.Helpers;
using PociagDoZyskow.ExternalDataReader.Reports.Interfaces;

namespace PociagDoZyskow.ExternalDataReader.Reports
{
    public class FinancialReportTimeReader : IFinancialReportTimeReader
    {
        private readonly string BaseUrl =
            "https://strefainwestorow.pl/dane/raporty/lista-dat-publikacji-raportow-okresowych/";

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

        public async Task<IEnumerable<FinancialReportTimeDataScan>> GetAllIncomingFinancialReportTimeScans()
        {
            var service = FirefoxDriverService.CreateDefaultService();
            FirefoxOptions options = new FirefoxOptions();
            //options.AddArguments("--headless");
            IWebDriver webDriver = new FirefoxDriver(service, options);
            webDriver.Navigate().GoToUrl(@"https://www.money.pl/gielda/raporty/");

            webDriver.FindElement(By.XPath("/html/body/div[3]/div/div[2]/div[3]/div/button[2]")).Click();

            return null;
        }

        public async Task<IEnumerable<FinancialReportTimeDataScan>> GetPublishedFinancialReportTimeScans()
        {
            var filledUrl = String.Concat(BaseUrl, "opublikowane");
            var result = _client.DownloadString(filledUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(result);

            var scans = new List<FinancialReportTimeDataScan>();
            var scanTime = DateTime.Now;

            foreach (HtmlNode table in doc.DocumentNode.SelectNodes("//*[@id=\"block-system-main\"]/table/tbody"))
            {

                foreach (HtmlNode row in table.SelectNodes("tr"))
                {
                    var reportDataScan = new FinancialReportTimeDataScan();
                    var cells = row.SelectNodes("th|td");
                    reportDataScan.Ticker = cells[TickerIndex].InnerText.CleanString();
                    reportDataScan.ShortCompanyName = cells[ShortCompanyNameIndex].InnerText.CleanString();
                    reportDataScan.FullCompanyName = cells[FullCompanyNameIndex].InnerText;
                    reportDataScan.ReportType = cells[ReportTypeNameIndex].InnerText;
                    reportDataScan.ReportDate = DateTime.Parse(
                        cells[ReportDateIndex].InnerText.CleanString());

                    scans.Add(reportDataScan);
                }
            }

            return scans;
        }
    }
}
