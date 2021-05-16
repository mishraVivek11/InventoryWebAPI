using Inventory.Models.Converters;
using Inventory.Models.DTOs;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace InventoryWebAPI
{
    public static class DtoJsonConverters
    {
        public static List<JsonConverter> GetDtoJsonConverters()
        {
            return new List<JsonConverter>()
            {
                new InterfaceModelConverter<IProductDTO, ProductDTO>(),
                new InterfaceModelConverter<IProductInventoryDTO, ProductInventoryDTO>()
            };
        }
    }
}
