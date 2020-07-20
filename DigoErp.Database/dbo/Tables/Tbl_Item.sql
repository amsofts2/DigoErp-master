CREATE TABLE [dbo].[Tbl_Item] (
    [Id]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (500) NULL,
    [CategoryId]    BIGINT         NULL,
    [SalePrice]     DECIMAL (18)   NULL,
    [PurchasePrice] DECIMAL (18)   NULL,
    [IsEnabled]     BIT            NULL,
    [Created_At]    DATETIME       NULL,
    [Updated_At]    DATETIME       NULL,
    [TaxId]         BIGINT         NULL,
    [Description]   NVARCHAR (50)  NULL,
    [Picture]       NVARCHAR (500) NULL,
    CONSTRAINT [PK_Tbl_Item] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Tbl_Item_Tbl_Category] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Tbl_Category] ([Id]),
    CONSTRAINT [FK_Tbl_Item_Tbl_Tax] FOREIGN KEY ([TaxId]) REFERENCES [dbo].[Tbl_Tax] ([Id])
);



