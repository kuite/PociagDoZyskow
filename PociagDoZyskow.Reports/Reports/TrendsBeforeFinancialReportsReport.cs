using System;
using PociagDoZyskow.Algorithms.DTO;

namespace PociagDoZyskow.Reports.Reports
{
    public class TrendsBeforeFinancialReportsReport : BaseReport
    {
        public override string TemplateName => nameof(TrendsBeforeFinancialReportsReport);

        public TrendsBeforeFinancialReportsAlgorithmResult AlgorithmResult { get; set; }

        public override string GetFilledTemplate()
        {
            var template = GetTemplate();
            throw new NotImplementedException();
        }
    }
}
