using PociagDoZyskow.Algorithms.Interfaces;
using PociagDoZyskow.Algorithms.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PociagDoZyskow.Algorithms.Reports
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
