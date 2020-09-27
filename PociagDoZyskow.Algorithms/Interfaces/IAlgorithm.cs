using System.Collections.Generic;
using System.Threading.Tasks;
using PociagDoZyskow.Algorithms.Model;

namespace PociagDoZyskow.Algorithms.Interfaces
{
    public interface IAlgorithm
    {
        string GetAlgorithmName();
        Task<IEnumerable<AlgorithmResult>> GetResults(Configuration cfg);
    }
}
