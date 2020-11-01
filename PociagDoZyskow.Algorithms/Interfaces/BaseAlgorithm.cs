using System.Collections.Generic;
using System.Threading.Tasks;
using PociagDoZyskow.Algorithms.DTO;

namespace PociagDoZyskow.Algorithms.Interfaces
{
    public abstract class BaseAlgorithm<T>
    {
        public abstract string AlgorithmName { get; }

        public abstract Task<IEnumerable<T>> GetResults(Configuration cfg);
    }
}
