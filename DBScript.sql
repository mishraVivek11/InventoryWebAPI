USE [AveryDennison]
GO

/****** Object: Table [dbo].[ProductDefinition] Script Date: 5/16/2021 11:27:02 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[InventoryTag];
DROP TABLE [dbo].[ProductDefinition];
DROP TABLE [dbo].[ProductInventory];


GO
CREATE TABLE [dbo].[ProductDefinition] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [CompanyPrefix] BIGINT           NOT NULL,
    [CompanyName]   NVARCHAR (255)   NOT NULL,
    [ItemReference] INT              NOT NULL,
    [ProductName]   NVARCHAR (255)   NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UK_ProdDef_CmpnyPrfx_ItmRef] UNIQUE NONCLUSTERED ([CompanyPrefix] ASC, [ItemReference] ASC)
);




GO
CREATE TABLE [dbo].[ProductInventory] (
    [InventoryId]   UNIQUEIDENTIFIER NOT NULL,
    [Location]      NVARCHAR (255)   NOT NULL,
    [InventoryDate] DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([InventoryId] ASC)
);

GO
CREATE TABLE [dbo].[InventoryTag] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [SgtinEPC]     NVARCHAR (50)    NOT NULL,
    [SerialNumber] BIGINT           NOT NULL,
    [ProductId]    UNIQUEIDENTIFIER NOT NULL,
    [InventoryId]  UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Inventory_Tag_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[ProductDefinition] ([Id]),
    CONSTRAINT [FK_Inventory_Tag_Definition] FOREIGN KEY ([InventoryId]) REFERENCES [dbo].[ProductInventory] ([InventoryId])
);



