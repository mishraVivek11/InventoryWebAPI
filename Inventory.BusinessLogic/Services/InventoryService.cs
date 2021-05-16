using Inventory.DataAccess;
using Inventory.Models.DTOs;
using Inventory.Models.ServiceInterfaces;
using InventoryWebAPI;
using Microsoft.Extensions.Logging;
using SGTIN.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.BusinessLogic.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly ILogger<InventoryService> logger;
        private readonly IWriteRepository repository;
        private readonly ISGTINConverter sGTINConverter;

        public InventoryService(ILogger<InventoryService> logger, IWriteRepository repository,ISGTINConverter sGTINConverter )
        {
            this.logger = logger;
            this.repository = repository;
            this.sGTINConverter = sGTINConverter;
        }
        public async Task CreateProductInventoryData(IProductInventoryDTO productInventoryDTO)
        {
            try
            {
                ProductInventory productInv = repository.Create<ProductInventory>();
                productInv.InventoryId = productInventoryDTO.InventoryId;
                productInv.InventoryDate = productInventoryDTO.InventoryDate;
                productInv.Location = productInventoryDTO.InventoryLocation;

                repository.Add<ProductInventory>(productInv);

                foreach (var epcTag in productInventoryDTO.SgtinTags.Distinct())
                {
                    try
                    {
                        var decodedTag = sGTINConverter.GetTagFromEPC(epcTag);
                        var res = repository.GetAll<ProductDefinition>(x => x.CompanyPrefix == decodedTag.CompanyPrefix && x.ItemReference == decodedTag.ItemReference, new string[] { "InventoryTag" }, false).ToList();

                        if (res.Any())
                        {
                            InventoryTag productInvTag = repository.Create<InventoryTag>();
                            productInvTag.Id = Guid.NewGuid();
                            productInvTag.SerialNumber = decodedTag.SerialReference;
                            productInvTag.ProductId = res.FirstOrDefault().Id;
                            productInvTag.SgtinEpc = epcTag;

                            productInv.InventoryTag.Add(productInvTag);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.LogError($"Error while processing tag : {epcTag}. {ex}");
                    }
                }
                await repository.SaveChanges().ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                logger.LogError($"Error while inventoryData for id {productInventoryDTO.InventoryId}. {ex}");
                throw;
            }
        }

        public async Task<int> GetInventoryCountByCompany(long companyId)
        {
            int count = await repository.Count<InventoryTag>(x => x.Product.CompanyPrefix == companyId).ConfigureAwait(false);
            if(count==0)
            {
                if(!await repository.Any<ProductDefinition>(x=> x.CompanyPrefix == companyId).ConfigureAwait(false))
                {
                    throw new KeyNotFoundException($"Company prefix {companyId} not found.");
                }
            }
            return count;
        }

        public async Task<int> GetInventoryCountByDay(int productid)
        {
            int count = await repository.Count<InventoryTag>(x => x.Product.ItemReference == productid && x.Inventory.InventoryDate.Date == DateTime.Today.Date).ConfigureAwait(false);
            if (count == 0)
            {
                if (! await repository.Any<ProductDefinition>(x => x.ItemReference == productid).ConfigureAwait(false))
                {
                    throw new KeyNotFoundException($"Product {productid} not found.");
                }
            }
            return count;
        }

        public async Task<int> GetInventoryCountByProductIdInventoryId(Guid id, int productid)
        {
          int count = await repository.Count<InventoryTag>(x => x.InventoryId == id && x.Product.ItemReference == productid).ConfigureAwait(false);
            if (count == 0)
            {
                if (!await repository.Any<InventoryTag>(x => x.InventoryId == id).ConfigureAwait(false) )
                {
                    throw new KeyNotFoundException($"Inventory id {id} not found.");
                }
                if (!await repository.Any<ProductDefinition>(x => x.ItemReference == productid).ConfigureAwait(false))
                {
                    throw new KeyNotFoundException($"Product {productid} not found.");
                }
            }
            return count;
        }
    }
}
