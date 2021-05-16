using AutoMapper;
using Inventory.DataAccess;
using Inventory.Models.DTOs;
using Inventory.Models.ServiceInterfaces;
using InventoryWebAPI;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Inventory.BusinessLogic.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> logger;
        private readonly IMapper mapper;
        private readonly IWriteRepository repository;

        public ProductService(ILogger<ProductService> logger, IMapper mapper, IWriteRepository repository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.repository = repository;
        }
        public async Task CreateProductDefinition(IProductDTO productDTO)
        {
            try
            {
                var product = mapper.Map<ProductDefinition>(productDTO);
                product.Id = System.Guid.NewGuid();
                repository.Add<ProductDefinition>(product);
                await repository.SaveChanges().ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                logger.LogError($"Error while processing CreateProductDefinition. {ex}");
                throw;
            }

        }
    }
}
