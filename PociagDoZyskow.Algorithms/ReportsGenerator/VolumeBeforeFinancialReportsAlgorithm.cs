using PociagDoZyskow.Algorithms.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PociagDoZyskow.Algorithms.DTO;

namespace PociagDoZyskow.Algorithms.ReportsGenerator
{
    public class VolumeBeforeFinancialReportsAlgorithm : IAlgorithm
    {
        public string GetAlgorithmName()
        {
            return "VolumeBeforeFinancialReports";
        }

        public Task<IEnumerable<AlgorithmResult>> GetResults(Configuration cfg)
        {
            throw new NotImplementedException();
        }
    }
}
