using Inventory.Models.DTOs;
using Inventory.Models.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InventoryWebAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        /// <summary>
        /// ProductController 
        /// </summary>
        /// <param name="productService">product service</param>
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        /// <summary>
        /// Creates a new product definition based on the provided input.
        /// </summary>
        /// <param name="productDTO">input model</param>
        /// <returns>Http status code</returns>
        [HttpPost]
        public async Task<ActionResult> CreateProductDefinition(IProductDTO productDTO)
        {
           await productService.CreateProductDefinition(productDTO).ConfigureAwait(false);
           return Ok();
        }
    }
}
