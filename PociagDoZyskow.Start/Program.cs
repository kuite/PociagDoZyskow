using System;
using System.IO;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using PociagDoZyskow.DataAccess.Entities;
using System.Web;
using OpenQA.Selenium.Support.UI;
using Cookie = System.Net.Cookie;

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
                WebDriverWait wait = new WebDriverWait(webDriver, new TimeSpan(0,0,1));
                var result2 = webDriver.PageSource;

                //HttpWebRequest httpRequest = (HttpWebRequest) WebRequest.Create("https://www.money.pl/api/graphql?query=query%20gielda_gpw_stock_espi_list(%24isin%3A%20String%2C%20%24limit%3A%20Int%2C%20%24offset%3A%20Int%2C%20%24year%3A%20Int%2C%20%24date_from%3A%20String%2C%20%24date_to%3A%20String)%20%7B%0A%20%20reportsData%3A%20gpw_reports(isin%3A%20%24isin%2C%20limit%3A%20%24limit%2C%20offset%3A%20%24offset%2C%20year%3A%20%24year%2C%20date_from%3A%20%24date_from%2C%20date_to%3A%20%24date_to)%20%7B%0A%20%20%20%20data%20%7B%0A%20%20%20%20%20%20title%0A%20%20%20%20%20%20date%3A%20creation_date%0A%20%20%20%20%20%20stockName%3A%20stock_name%0A%20%20%20%20%20%20stockUrlRel%3A%20stock_url_rel%0A%20%20%20%20%20%20year%0A%20%20%20%20%20%20quarter%0A%20%20%20%20%20%20urlRel%3A%20url_rel%0A%20%20%20%20%20%20__typename%0A%20%20%20%20%7D%0A%20%20%20%20paginator%20%7B%0A%20%20%20%20%20%20count%0A%20%20%20%20%20%20offset%0A%20%20%20%20%20%20limit%0A%20%20%20%20%20%20__typename%0A%20%20%20%20%7D%0A%20%20%20%20__typename%0A%20%20%7D%0A%7D%0A&operationName=gielda_gpw_stock_espi_list&variables=%7B%22limit%22%3A20%2C%22offset%22%3A0%2C%22year%22%3A%222020%22%7D");
                //var cookie = new Cookie("cookie", HttpUtility.UrlEncode("STabid=bbf6df9465f7ba83de0efc89617b8081:1601675874.970:v1; STabnoid=1; mny_ver=n2b; WPabs=82ad48; PWA_adbd=0; _hjTLDTest=1; _hjid=11cbfe47-d97d-42fc-9062-8430cdf7f1cf; WPdp=ij6EkhhOScUUxEUBBgUU1gaSx5FS1AHX1oHX10DUVIGX1IORUhVAkgMW0YUChlGAA4UU0htWEYERVkaXUYDRVwaXkYORVMaWFprS0YUBB4UU1kaSx9XS1AHFEYULj1mS1BNSwdES1AERUhCGkgMWFwGWFwBXFIOWVwOUUYUCgEUU1gaSwlFGQNSS1AUMlsaW0YFRV4aXEYARV0aUUYPRVsGNEgaSwdCS1AFFEYUPToUUxEUBBgUU1gaSx5FS1AHX1oHX10DUVIGX1IORUhVAkgMW0YUChlGAA4UU0htWDcURUhbHUgMWhdL; WPtcs2=CO6rFfiO6rFfiBIACCPLA6CgAP_AAH_AAB5YGxtd_X_fb3_j-_5999t0eY1f9_7_v2wzjgeds-8Nyd_X_L8X62M7vB36pq4KuR4Eu3LBAQFlHOHcTQmQ4IkVqTLsak2Mr7NKJ7LEilMbO2dYGHtfn9XTuZKY797s___z__-_-___77f_r-3_3_uBsYBJhqXwEWQljASTRpVCiBCFcSHQAgAooRhaJLCAlcFOyuAj1BAwAQGoCMCIEGIKMWQQAAAABJREAIAeCARAEQCAAEAKkBCAAiQBBYASBgEAAoBoWAEUQQgSEGRwVHKIEBEi0UE8kYEkFzsYQQgAAAAA.YAAAAAAAAAAA; WPs2snc=1; _ga=GA1.2.1404847239.1601675881; _gid=GA1.2.1195929433.1601675881; _gat=1; sa31=350f99a97a790e3d67c56407a77e609a; sa32=validate_error; __gfp_64b=JH6KdJAGqHaEtXMd9Xf4ILaPOaDEkEwvM62FtisVdxv.P7; _fbp=fb.1.1601675881110.256354840; id5id.1st=%20%7B%20%22created_at%22%3A%20%222020-10-02T21%3A58%3A01.091Z%22%2C%20%22id5_consent%22%3A%20true%2C%20%22original_uid%22%3A%20%22ID5-ZHMOeiE6pCiVijBIzy_VwPvhtA_luya_4jW31YMwhA%22%2C%20%22universal_uid%22%3A%20%22ID5-ZHMOeiE6pCiVijBIzy_VwPvhtA_luya_4jW31YMwhA%22%2C%20%22signature%22%3A%20%22ID5_AdSmu_uX1voykvdR5EVVtAwHOJJR9I8v7IaGc8zKUytfFKMBgrQWRe5SsWIa5UlFvUVRFo9NgD1jlsxoXcnAyk8%22%2C%20%22link_type%22%3A%200%2C%20%22cascade_needed%22%3A%20true%7D; id5id.1st_last=1601675881124; id5id.1st_334_nb=1; __gads=ID=fe8662136d989d72-22b6b801eeb80002:T=1601675881:S=ALNI_MbJVlkVIMGrIUn1_ovB5nfyAESczg")) 
                //    { Domain = "money.pl" };
                //httpRequest.CookieContainer = new CookieContainer();
                //httpRequest.CookieContainer.Add(cookie);
                //var response = httpRequest.GetResponse();
                //Stream receiveStream = response.GetResponseStream();
                //StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                //var readResponse = readStream.ReadToEnd();

                HtmlDocument doc = new HtmlDocument();
                //doc.LoadHtml(result);
                //doc.LoadHtml(result2);

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

                //string removedBefore = result.Substring(result.LastIndexOf("</thead>"));
                //string removedAfter = removedBefore.Substring(0, removedBefore.LastIndexOf("</tr>") + 5);
            }
            Console.WriteLine("dotnet ef database update --project ../PociagDoZyskow.DataAccess -c StockExchangeContext");
            Console.WriteLine("dotnet ef migrations add InitialCreate --project ../PociagDoZyskow.DataAccess -c StockExchangeContext");
        }
    }
}