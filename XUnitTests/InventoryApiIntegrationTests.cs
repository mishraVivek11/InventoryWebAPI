using Inventory.Models.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTests
{
    public class InventoryApiIntegrationTests : IClassFixture<ConfigurationFixture>
    {
        private readonly ConfigurationFixture configurationFixture;  
        
        public InventoryApiIntegrationTests(ConfigurationFixture configurationFixture)
        {
            this.configurationFixture = configurationFixture;
            SeedInMemoryDB();
        }

        private void SeedInMemoryDB()
        {
            //seed products
            foreach(var product in SeedTestData.ProductDescList)
            {
                var json = JsonConvert.SerializeObject(product);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = configurationFixture.Client.PostAsync("/api/product", data).GetAwaiter().GetResult();
            }

            //seed inventory
            foreach (var inventory in SeedTestData.ProductInventoryList)
            {
                var json = JsonConvert.SerializeObject(inventory);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var responseInv = configurationFixture.Client.PostAsync("/api/inventory", data).GetAwaiter().GetResult();
            }

        }


        [Theory]
        [InlineData(3319361, "Sanford LLC", 407205, "Beans - Kidney, Red Dry")]
        [InlineData(6314853123, "Gleichner, Rodriguez and Wilkinson", 877, "Scallops - In Shell")]
        [InlineData(39266333, "Mertz, O'Conner and Heaney", 21526, "Fruit Mix - Light")]
        [InlineData(213645, "McGlynn Inc", 6152432, "Pickles - Gherkins")]
        [InlineData(178504, "Thompson LLC", 2577266, "Straws - Cocktale")]
        public async Task Post_CreateProduct_SavesToDB(long companyPrefix, string companyName, int itemReference, string productName)
        {
            var product = new ProductDTO() { CompanyName = companyName, CompanyPrefix = Convert.ToInt64(companyPrefix), ItemReference = Convert.ToInt32(itemReference), ProductName = productName };
            var json = JsonConvert.SerializeObject(product);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await configurationFixture.Client.PostAsync("/api/product", data).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData(3319361, "Sanford LLC", 407205)]
        public async Task Post_CreateProduct_APIValidationMissingMandatoryField_ThowsInternalServerError(long companyPrefix, string companyName, int itemReference)
        {
            var product = new ProductDTO() { CompanyName = companyName, CompanyPrefix = Convert.ToInt64(companyPrefix), ItemReference = Convert.ToInt32(itemReference) };
            var json = JsonConvert.SerializeObject(product);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await configurationFixture.Client.PostAsync("/api/product", data).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Post_CreateInventory_SavesToDB()
        {
            ProductInventoryDTO productInventoryDTO = new ProductInventoryDTO()
            {
                InventoryId = Guid.NewGuid(),
                InventoryDate = DateTime.Today,
                InventoryLocation = "Graz",
                SgtinTags = new List<string>() { "303A3BF2D03FF2003A119BBF" }
            };
            var json = JsonConvert.SerializeObject(productInventoryDTO);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await configurationFixture.Client.PostAsync("/api/inventory", data).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }       


        [Theory]
        [InlineData(127083, 13)]
        [InlineData(793681, 8)]
        [InlineData(342472, 0)]
        [InlineData(3319361, 3)]
        public async Task Get_CheckInventoryCountbyCompanyId(long companyPrefix, int countResult)
        {
            configurationFixture.Client.DefaultRequestHeaders.Accept.Clear();
            var responseGet = await configurationFixture.Client.GetAsync($"api/inventory/company/{companyPrefix}").ConfigureAwait(false);
            string countString = await responseGet.Content.ReadAsStringAsync().ConfigureAwait(false);

            Assert.Equal(countResult, Convert.ToInt32(countString));
        }

        [Theory]
        [InlineData(1254)]
        [InlineData(6234)]
        public async Task Get_CheckInventoryCountbyCompanyId_CompanyCodeDoesntExistReturnsNotFound(int companyPrefixTestValue)
        {
            configurationFixture.Client.DefaultRequestHeaders.Accept.Clear();
            var responseGet = await configurationFixture.Client.GetAsync($"api/inventory/company/{companyPrefixTestValue}").ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.NotFound, responseGet.StatusCode);
        }

        [Theory]
        [InlineData(5256251, 13)]
        [InlineData(9774282, 8)]
        [InlineData(6004566, 0)]
        public async Task Get_CheckInventoryCountbyDayAndProductId( int itemReference, int countRes)
        {
            var responseGet = await configurationFixture.Client.GetAsync($"api/inventory/product/{itemReference}").ConfigureAwait(false);
            string countString = await responseGet.Content.ReadAsStringAsync().ConfigureAwait(false);

            Assert.Equal(countRes, Convert.ToInt32(countString));
        }

        [Theory]
        [InlineData(52562)]
        [InlineData(4987)]
        [InlineData(900536)]
        public async Task Get_CheckInventoryCountbyDayAndProductId_ProductDescDoesnotExist_returnsNotFound(int itemReference)
        {
            var responseGet = await configurationFixture.Client.GetAsync($"api/inventory/product/{itemReference}").ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.NotFound, responseGet.StatusCode);
        }


        [Theory]
        [InlineData("BF2EFD40-853D-469A-BDDB-3818DC83CBBF", 5256251,9)]
        [InlineData("8F1FF952-0D64-4680-A76F-1879A2FFD64A", 5256251,4)]
        [InlineData("BF2EFD40-853D-469A-BDDB-3818DC83CBBF", 9774282,4)]
        [InlineData("8F1FF952-0D64-4680-A76F-1879A2FFD64A", 9774282,4)]
        [InlineData("BF2EFD40-853D-469A-BDDB-3818DC83CBBF", 6004566,0)]
        [InlineData("8F1FF952-0D64-4680-A76F-1879A2FFD64A", 6004566,0)]
        public async Task Get_CheckInventoryCountbyInventoryIdAndProductId(string inventoryId,int itemReference,int countExp)
        {
            var responseGet = await configurationFixture.Client.GetAsync($"api/inventory/{inventoryId}/product/{itemReference}").ConfigureAwait(false);

            string countString = await responseGet.Content.ReadAsStringAsync().ConfigureAwait(false);

            Assert.Equal(countExp, Convert.ToInt32(countString));
        }

        [Theory]
        [InlineData("BF2EFD40-853D-463A-BDDB-3818DC83CBBF", 5256251)]
        [InlineData("8F1FF952-0D64-4630-A76F-1879A2FFD64A", 5256251)]
        public async Task Get_CheckInventoryCountbyInventoryIdAndProductId_InventoryIdDoesntExist_ReturnsNotFound(string inventoryId, int itemReference)
        {
            var responseGet = await configurationFixture.Client.GetAsync($"api/inventory/{inventoryId}/product/{itemReference}").ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.NotFound, responseGet.StatusCode);
        }

        [Theory]
        [InlineData("BF2EFD40-853D-469A-BDDB-3818DC83CBBF", 800265)]
        [InlineData("8F1FF952-0D64-4680-A76F-1879A2FFD64A", 74509)]
        public async Task Get_CheckInventoryCountbyInventoryIdAndProductId_ProductDoesNotExist_ReturnsNotFound(string inventoryId, int itemReference)
        {
            var responseGet = await configurationFixture.Client.GetAsync($"api/inventory/{inventoryId}/product/{itemReference}").ConfigureAwait(false);
            string returnCode = await responseGet.Content.ReadAsStringAsync().ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.NotFound, responseGet.StatusCode);
        }



    }
}
