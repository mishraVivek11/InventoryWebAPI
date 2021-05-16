using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Models.DTOs
{
    public interface IProductInventoryDTO
    {
        [Required]
        Guid InventoryId { get; set; }

        [Required]
        string InventoryLocation { get; set; }

        [Required]
        DateTime InventoryDate { get; set; }

        [Required]
        List<string> SgtinTags { get; set; }
    }
}
