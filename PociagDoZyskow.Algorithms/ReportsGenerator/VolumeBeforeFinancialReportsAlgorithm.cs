using PociagDoZyskow.Algorithms.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PociagDoZyskow.Algorithms.DTO;

namespace PociagDoZyskow.Algorithms.ReportsGenerator
{
    public class VolumeBeforeFinancialReportsAlgorithm : BaseAlgorithm
    {
        public override string AlgorithmName => "VolumeBeforeFinancialReports";

        public override Task<IEnumerable<AlgorithmResult>> GetResults(Configuration cfg)
        {
            throw new NotImplementedException();
        }
    }
}
