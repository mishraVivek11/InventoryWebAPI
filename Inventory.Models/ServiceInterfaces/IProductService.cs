using Inventory.Models.DTOs;
using System.Threading.Tasks;

namespace Inventory.Models.ServiceInterfaces
{
    public interface IProductService
    {
        Task CreateProductDefinition(IProductDTO productDTO);
    }
}
