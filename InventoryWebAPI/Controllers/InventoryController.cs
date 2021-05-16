using Inventory.Models.DTOs;
using Inventory.Models.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace InventoryWebAPI.Controllers
{
    [Route("api/inventory")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService inventoryService;

        /// <summary>
        /// InventoryController
        /// </summary>
        /// <param name="inventoryService">inventory service</param>
        public InventoryController(IInventoryService inventoryService)
        {
            this.inventoryService = inventoryService;
        }

        /// <summary>
        /// Post inventory data.
        /// </summary>
        /// <param name="productInventoryDTO">IProductInventoryDTO</param>
        /// <returns>http status code</returns>
        [HttpPost]
        public async Task<ActionResult> AddInventoryData(IProductInventoryDTO productInventoryDTO)
        {
            await inventoryService.CreateProductInventoryData(productInventoryDTO).ConfigureAwait(false);
            return Ok();
        }

        /// <summary>
        /// Gets the count of inventoried items based on the provide inventory id and product id.
        /// </summary>
        /// <param name="id">Inventory id</param>
        /// <param name="productid">product id</param>
        /// <returns>count of inventoried items</returns>
        [HttpGet("{id}/product/{productid}")]
        public async Task<ActionResult<int>> GetProductsCountById(Guid id, int productid)
        {            
            return Ok(await inventoryService.GetInventoryCountByProductIdInventoryId(id, productid).ConfigureAwait(false));
        }

        /// <summary>
        /// Gets the count of inventoried items by product id for the day.
        /// </summary>
        /// <param name="productid">product id</param>
        /// <returns>count of inventoried</returns>
        [HttpGet("product/{productid}")]
        public async Task<ActionResult<int>> GetProductsCountByDay(int productid)
        {
            return Ok(await inventoryService.GetInventoryCountByDay(productid).ConfigureAwait(false));
        }

        /// <summary>
        /// Gets the count of inventoried items by company prefix.
        /// </summary>
        /// <param name="companyprefix">company prefix</param>
        /// <returns>count of inventoried</returns>
        [HttpGet("company/{companyprefix}")]
        public async Task<ActionResult<int>> GetProductsCountByCompany(long companyprefix)
        {
            return Ok(await inventoryService.GetInventoryCountByCompany(companyprefix).ConfigureAwait(false));
        }
    }
}
