using System;
using System.Net;
using HtmlAgilityPack;
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
                string result = client.DownloadString("https://www.gpw.pl/archiwum-notowan-full?type=10&instrument=&date=25-09-2020");
                // TODO: do something with the downloaded result from the remote

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(result);

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