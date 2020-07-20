CREATE TABLE [dbo].[Tbl_Bill_Item] (
    [Id]         BIGINT          IDENTITY (1, 1) NOT NULL,
    [Bill_Id]    BIGINT          NULL,
    [ItemId]     BIGINT          NULL,
    [Name]       NVARCHAR (500)  NULL,
    [Quantity]   INT             NULL,
    [Price]      DECIMAL (18, 2) NULL,
    [TaxId]      BIGINT          NULL,
    [Tax]        DECIMAL (18, 2) NULL,
    [Created_At] DATETIME        NULL,
    [Updated_At] DATETIME        NULL,
    CONSTRAINT [PK_Tbl_Bill_Item] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Tbl_Bill_Item_Tbl_Bill] FOREIGN KEY ([Bill_Id]) REFERENCES [dbo].[Tbl_Bill] ([Id])
);





