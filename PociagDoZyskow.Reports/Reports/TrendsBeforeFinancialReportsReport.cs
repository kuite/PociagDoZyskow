using System;
using System.Collections.Generic;
using PociagDoZyskow.Algorithms.DTO;
using PociagDoZyskow.HtmlReports.Factories.Interfaces;
using PociagDoZyskow.HtmlReports.Model;

namespace PociagDoZyskow.HtmlReports.Reports
{
    public class TrendsBeforeFinancialReportsReport : BaseReport<TrendsBeforeFinancialReportsAlgorithmResult>
    {
        public TrendsBeforeFinancialReportsReport(ITemplateInfoFactory templateInfoFactory) : base(templateInfoFactory)
        {

        }

        public override string GetFilledTemplate(TrendsBeforeFinancialReportsAlgorithmResult algorithmResult)
        {
            var templateInfo = _templateInfoFactory.Create("TrendsBeforeFinancialReportsReport");
            var rawContent = templateInfo.Content;
            
            var filledContent = rawContent
                .Replace("{{CompanyShortName}}", algorithmResult.CompanyShortName)
                .Replace("{{StockMarketValues}}", GetStockMarketValues(algorithmResult.StockMarketValues))
                .Replace("{{ReportDateTime}}", algorithmResult.FinancialReportTime.ToShortDateString());


            return filledContent;
        }

        private string GetStockMarketValues(IEnumerable<StockMarketValue> stockMarketValues)
        {
            var values = string.Empty;
            foreach (var value in stockMarketValues)
            {
                var change = value.ChangePrice >= 0 ? "green" : "red";
                var sign = value.ChangePrice >= 0 ? "+" : string.Empty;
                var filledTemplate = $@"		
                <div class=""timmeline-item"">
		            <p class=""{change}"">{sign}{value.ChangePrice}</p>
		            <p>{value.TotalTransactionValue:N0}</p>
		            <p class=""date"">{value.ScanDateTime.ToShortDateString()}</p>
	            </div>";
                values = string.Concat(values, filledTemplate);
            }

            return values;
        }
    }
}
