using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace InventoryWebAPI
{
    public partial class InventoryTag
    {
        public Guid Id { get; set; }
        public string SgtinEpc { get; set; }
        public long SerialNumber { get; set; }
        public Guid ProductId { get; set; }
        public Guid InventoryId { get; set; }

        public virtual ProductInventory Inventory { get; set; }
        public virtual ProductDefinition Product { get; set; }
    }
}
