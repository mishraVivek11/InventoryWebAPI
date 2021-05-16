using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Inventory.DataAccess
{
    public interface IReadRepository
    {
        IQueryable<T> GetAll<T>(Expression<Func<T, bool>> predicate = null, string[] dependencies = null, bool isTracking = false)
            where T : class;

        Task<int> Count<T>(Expression<Func<T, bool>> predicate)
            where T : class;

        Task<bool> Any<T>(Expression<Func<T, bool>> predicate)
            where T : class;
    }
}
