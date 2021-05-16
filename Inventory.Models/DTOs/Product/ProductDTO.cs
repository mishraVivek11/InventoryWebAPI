using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Inventory.Models.DTOs
{
    [DataContract]
    public class ProductDTO : IProductDTO
    {
        [Required]
        [DataMember(Name = "companyPrefix")]
        public long CompanyPrefix { get ; set; }

        [Required]
        [StringLength(255)]
        [DataMember(Name = "companyName")]
        public string CompanyName { get; set; }

        [Required]
        [DataMember(Name = "itemReference")]
        public int ItemReference { get; set; }

        [Required]
        [StringLength(255)]
        [DataMember(Name = "productName")]
        public string ProductName { get; set; }

    }
}
