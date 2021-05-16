using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace InventoryWebAPI
{
    public partial class ProductDefinition
    {
        public ProductDefinition()
        {
            InventoryTag = new HashSet<InventoryTag>();
        }

        public Guid Id { get; set; }
        public long CompanyPrefix { get; set; }
        public string CompanyName { get; set; }
        public int ItemReference { get; set; }
        public string ProductName { get; set; }

        public virtual ICollection<InventoryTag> InventoryTag { get; set; }
    }
}
