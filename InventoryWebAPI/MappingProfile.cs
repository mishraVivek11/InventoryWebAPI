using AutoMapper;
using Inventory.Models.DTOs;

namespace InventoryWebAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IProductDTO, ProductDefinition>();
            CreateMap<ProductDefinition, IProductDTO>();
        }
    }
}
