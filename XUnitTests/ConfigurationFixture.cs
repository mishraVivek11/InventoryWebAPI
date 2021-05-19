using InventoryWebAPI;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Net.Http;
namespace XUnitTests
{
    public class ConfigurationFixture : IDisposable
    {
        public HttpClient Client { get; private set; }

        public ConfigurationFixture()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                  .WithWebHostBuilder(builder =>
                  {
                      builder.ConfigureServices(services =>
                      {
                          services.RemoveAll(typeof(DbContextOptions<ADContext>));
                          services.AddDbContext<ADContext>(options =>
                          {
                              options.UseInMemoryDatabase("TestDB").UseLazyLoadingProxies();

                          });
                      });
                  });
            Client = appFactory.CreateClient();
        }


        public void Dispose()
        {
            Client?.Dispose();
        }
    }
}
