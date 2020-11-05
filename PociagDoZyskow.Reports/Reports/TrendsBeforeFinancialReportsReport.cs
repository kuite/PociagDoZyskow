using System;
using PociagDoZyskow.Algorithms.DTO;
using PociagDoZyskow.EmailReports.Factories.Interfaces;
using PociagDoZyskow.EmailReports.Model;

namespace PociagDoZyskow.EmailReports.Reports
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
                .Replace("{{ReportDateTime}}", algorithmResult.FinancialReportTime.ToShortDateString());


            return filledContent;
        }

    }
}
