using PociagDoZyskow.Algorithms.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PociagDoZyskow.Algorithms.DTO;

namespace PociagDoZyskow.Algorithms
{
    public class TrendsBeforeFinancialReportsAlgorithm : BaseAlgorithm<TrendsBeforeFinancialReportsAlgorithmResult>
    {
        public override string AlgorithmName => nameof(TrendsBeforeFinancialReportsAlgorithm);

        public override Task<IEnumerable<TrendsBeforeFinancialReportsAlgorithmResult>> GetResults(Configuration cfg)
        {
            //get financial report date(s)
            //get data scans 60 days before financial report
            //prepare result
            throw new NotImplementedException();
        }
    }
}
