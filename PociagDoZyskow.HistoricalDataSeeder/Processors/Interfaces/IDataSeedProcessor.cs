using System.Threading.Tasks;

namespace PociagDoZyskow.HistoricalDataSeeder.Processors.Interfaces
{
    public interface IDataSeedProcessor
    {
        Task Start(int fromDayAgo);
    }
}
