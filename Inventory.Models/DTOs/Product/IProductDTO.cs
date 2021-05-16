using System.ComponentModel.DataAnnotations;

namespace Inventory.Models.DTOs
{
    public interface IProductDTO
    {
        [Required]
        long CompanyPrefix { get; set; }

        [Required]
        [StringLength(255)]
        string CompanyName { get; set; }

        [Required]
        int ItemReference { get; set; }

        [Required]
        [StringLength(255)]
        string ProductName { get; set; }
    }
}
