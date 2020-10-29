using System.Threading.Tasks;

namespace PociagDoZyskow.HistoricalDataSeeder.Processors.Interfaces
{
    public interface IProcessor
    {
        Task Start(int fromDayAgo);
    }
}
