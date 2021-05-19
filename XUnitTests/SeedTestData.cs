using Inventory.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTests
{
    public static class SeedTestData
    {
        private static readonly List<string> SgtinTags = new List<string>()
                {
                    "30587C1AD40D0EC028911E41",
                    "30B87C1AD40D0EC01175D053",
                    "30787C1AD40D0EC0275E2032",
                    "30B87C1AD40D0EC01175D053",
                    "30387C1AD40D0EC018338C07",
                    "30D87C1AD40D0EC0022E33CF",
                    "30387C1AD40D0EC03569186C",
                    "30D87C1AD40D0EC00506F17B",
                    "30787C1AD40D0EC00A9E1B24",
                    "30187C1AD40D0EC03B03C32D",
                    "301B0714654932802B49D0B0",
                    "301B0714654932802611CD17",
                    "303B0714654932802A5AB10F",
                    "305B0714654932802C0ADDB1",
        };

        private static readonly List<string> SgtinTags2 = new List<string>()
                {
                    "30587C1AD40D0EC028911E41",
                    "30B87C1AD40D0EC01175D053",
                    "30787C1AD40D0EC0275E2032",
                    "30B87C1AD40D0EC01175D053",
                    "30187C1AD40D0EC03B03C32D",
                    "301B0714654932802B49D0B0",
                    "301B0714654932802611CD17",
                    "303B0714654932802A5AB10F",
                    "305B0714654932802C0ADDB1",
                    "3054CA99058DA94038CA19C1",
                    "3094CA99058DA9403A7DE97D",
                    "3014CA99058DA9400BBD2C00"
        };

        public static readonly List<ProductInventoryDTO> ProductInventoryList = new List<ProductInventoryDTO>()
        {
            new ProductInventoryDTO(){ InventoryId=new Guid("BF2EFD40-853D-469A-BDDB-3818DC83CBBF"), InventoryDate= DateTime.Today, InventoryLocation="Wien", SgtinTags = SgtinTags},
            new ProductInventoryDTO(){ InventoryId= new Guid("8F1FF952-0D64-4680-A76F-1879A2FFD64A"), InventoryDate= DateTime.Today, InventoryLocation="Graz", SgtinTags = SgtinTags2}
        };

        public static readonly List<ProductDTO> ProductDescList = new List<ProductDTO>()
        {
            new ProductDTO() { CompanyName = "Watsica-Labadie", CompanyPrefix = 127083, ItemReference = 5256251, ProductName = "Crush - Grape, 355 Ml" },
            new ProductDTO() { CompanyName = "Carroll-Hammes", CompanyPrefix = 793681, ItemReference = 9774282, ProductName = "Jicama" },
            new ProductDTO() { CompanyName = "Sanford LLC", CompanyPrefix = 3319361, ItemReference = 407205, ProductName = "Beans - Kidney, Red Dry" },
            new ProductDTO() {CompanyName = "Macejkovic, Towne and Spencer", CompanyPrefix = 342472, ItemReference = 6004566, ProductName = "Tamarind Paste" }
        };

    }
}
