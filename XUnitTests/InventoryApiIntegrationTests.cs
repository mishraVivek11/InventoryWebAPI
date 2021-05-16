using InventoryWebAPI;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTests
{
    public class InventoryApiIntegrationTests
    {
        private readonly HttpClient client;

        public InventoryApiIntegrationTests()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder=>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(InventoryWebAPI.ADContext));
                        services.AddDbContext<InventoryWebAPI.ADContext>(options =>
                        {
                            options.UseInMemoryDatabase("TestDB").UseLazyLoadingProxies();

                        });
                    });
                });
            client = appFactory.CreateClient();
        }

        [Fact]
        public async Task Test()
        {
           var response = await client.GetAsync("/api/inventory/product/682407").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            string value = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
    }
}
