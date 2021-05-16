using InventoryWebAPI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Inventory.DataAccess
{
    public class BaseRepository : IReadRepository 
    {
        protected readonly ADContext aDContext;

        public BaseRepository(ADContext aDContext)
        {
            this.aDContext = aDContext;
        }

        public IQueryable<T> GetAll<T>(Expression<Func<T, bool>> predicate = null, string[] dependencies = null, bool isTracking = false) where T : class
        {
            IQueryable<T> query;

            if (predicate is null)
            {
                query = aDContext.Set<T>();
            }
            else
            {
                query = aDContext.Set<T>().Where(predicate);
            }

            return IncludeDependencies(dependencies, query, isTracking);

        }

        private static IQueryable<T> IncludeDependencies<T>(string[] dependencies, IQueryable<T> query, bool isTracking = false)
            where T : class
        {
            if (dependencies != null)
            {
                if (isTracking)
                {
                    foreach (var entity in dependencies)
                    {
                        query = query.Include(entity);
                    }
                }
                else
                {
                    foreach (var entity in dependencies)
                    {
                        query = query.Include(entity).AsNoTracking();
                    }
                }
            }

            if (dependencies == null && !isTracking)
            {
                query = query.AsNoTracking();
            }

            return query;
        }

        public async Task<int> Count<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await aDContext.Set<T>().CountAsync(predicate).ConfigureAwait(false);
        }

        public async Task<bool> Any<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return  await aDContext.Set<T>().AnyAsync(predicate).ConfigureAwait(false);
        }
    }
}
