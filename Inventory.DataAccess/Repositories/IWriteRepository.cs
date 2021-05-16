using System.Threading.Tasks;

namespace Inventory.DataAccess
{
    public interface IWriteRepository : IReadRepository
    {
        T Create<T>()
           where T : class, new();

        void Add<T>(T entity)
            where T : class;

        Task<bool> SaveChanges();
    }
}
