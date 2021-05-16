using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Inventory.Models.DTOs
{
    [DataContract]
    public class ProductInventoryDTO : IProductInventoryDTO
    {
        [Required]
        [DataMember(Name = "inventoryId")]
        public Guid InventoryId { get ; set ; }

        [Required]
        [DataMember(Name = "inventoryLocation")]
        [StringLength(255)]
        public string InventoryLocation { get ; set ; }

        [Required]
        [DataMember(Name = "inventoryDate")]
        public DateTime InventoryDate { get ; set ; }

        [Required]
        [DataMember(Name = "sgtinTags")]
        public List<string> SgtinTags { get ; set ; }
    }
}
