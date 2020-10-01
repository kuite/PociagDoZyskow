using System;
using System.Net;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using PociagDoZyskow.DataAccess.Entities;

namespace PociagDoZyskow.Start
{
    class Program
    {
        static void Main(string[] args)
        {
            var model = new Company();
            using (var client = new WebClient())
            {
                string result = client.DownloadString("https://www.money.pl/gielda/raporty/");
                var service = FirefoxDriverService.CreateDefaultService();
                FirefoxOptions options = new FirefoxOptions();
                //options.AddArguments("--headless");
                IWebDriver webDriver = new FirefoxDriver(service, options);
                webDriver.Navigate().GoToUrl(@"https://www.money.pl/gielda/raporty/");
                webDriver.FindElement(By.XPath("/html/body/div[3]/div/div[2]/div[3]/div/button[2]")).Click();
                var result2 = webDriver.PageSource;

                HtmlDocument doc = new HtmlDocument();
                //doc.LoadHtml(result);
                doc.LoadHtml(result2);

                foreach (HtmlNode table in doc.DocumentNode.SelectNodes("/html/body/section[2]/table"))
                {
                    ///This is the table.    
                    foreach (HtmlNode row in table.SelectNodes("tr"))
                    {
                        ///This is the row.
                        foreach (HtmlNode cell in row.SelectNodes("th|td"))
                        {
                            ///This the cell.
                            Console.Write(cell.InnerText + " ");
                        }
                        //Console.WriteLine();
                    }
                }

                string removedBefore = result.Substring(result.LastIndexOf("</thead>"));
                string removedAfter = removedBefore.Substring(0, removedBefore.LastIndexOf("</tr>") + 5);
            }
            Console.WriteLine("dotnet ef database update --project ../PociagDoZyskow.DataAccess -c StockExchangeContext");
        }
    }
}