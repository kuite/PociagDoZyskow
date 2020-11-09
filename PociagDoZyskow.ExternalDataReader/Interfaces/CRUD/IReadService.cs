using System.Threading.Tasks;

namespace PociagDoZyskow.Services.Interfaces.CRUD
{
    public interface IReadService<T>
    {
        Task<T> GetAll();
    }
}
