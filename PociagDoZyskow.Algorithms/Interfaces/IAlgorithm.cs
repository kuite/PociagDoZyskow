using System.Collections.Generic;
using System.Threading.Tasks;
using PociagDoZyskow.Algorithms.DTO;

namespace PociagDoZyskow.Algorithms.Interfaces
{
    public interface IAlgorithm
    {
        string GetAlgorithmName();
        Task<IEnumerable<AlgorithmResult>> GetResults(Configuration cfg);
    }
}
