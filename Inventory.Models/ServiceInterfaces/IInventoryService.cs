using Inventory.Models.DTOs;
using System;
using System.Threading.Tasks;

namespace Inventory.Models.ServiceInterfaces
{
    public interface IInventoryService
    {
        Task CreateProductInventoryData(IProductInventoryDTO productInventoryDTO);
        Task<int> GetInventoryCountByProductIdInventoryId(Guid id, int productid);
        Task<int> GetInventoryCountByDay(int productid);
        Task<int> GetInventoryCountByCompany(long companyId);
    }
}
