using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace InventoryWebAPI
{
    public partial class ProductInventory
    {
        public ProductInventory()
        {
            InventoryTag = new HashSet<InventoryTag>();
        }

        public Guid InventoryId { get; set; }
        public string Location { get; set; }
        public DateTime InventoryDate { get; set; }

        public virtual ICollection<InventoryTag> InventoryTag { get; set; }
    }
}
