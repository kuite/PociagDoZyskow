using PociagDoZyskow.Algorithms.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PociagDoZyskow.Algorithms.DTO;

namespace PociagDoZyskow.Algorithms
{
    public class TrendsBeforeFinancialReportsAlgorithm : BaseAlgorithm
    {
        public override string AlgorithmName => nameof(TrendsBeforeFinancialReportsAlgorithm);

        public override Task<IEnumerable<AlgorithmResult>> GetResults(Configuration cfg)
        {
            throw new NotImplementedException();
        }
    }
}
