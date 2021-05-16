using InventoryWebAPI;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Inventory.DataAccess
{
    public class WriteRepository : BaseRepository, IWriteRepository
    {
        public WriteRepository(ADContext context)
           : base(context)
        {
            aDContext.ChangeTracker.AutoDetectChangesEnabled = true;
            aDContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
        }

        public void Add<T>(T entity) 
            where T : class
        {
             aDContext.Add<T>(entity);
        }

        public T Create<T>() where T : class, new()
        {
            var newEntity = new T();
            return newEntity;
        }

        public async Task<bool> SaveChanges()
        {
            bool success = false;
            if (await aDContext.SaveChangesAsync().ConfigureAwait(false) > 0)
            {
                success = true;
            }
            return success;
        }
    }
}
