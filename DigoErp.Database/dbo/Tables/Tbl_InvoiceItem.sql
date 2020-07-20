CREATE TABLE [dbo].[Tbl_InvoiceItem] (
    [Id]         BIGINT          IDENTITY (1, 1) NOT NULL,
    [InvoiceId]  BIGINT          NULL,
    [ItemId]     BIGINT          NULL,
    [Name]       NVARCHAR (500)  NULL,
    [Quantity]   INT             NULL,
    [Price]      DECIMAL (18, 2) NULL,
    [TaxId]      BIGINT          NULL,
    [Tax]        DECIMAL (18, 2) NULL,
    [Created_At] DATETIME        NULL,
    [Updated_At] DATETIME        NULL,
    CONSTRAINT [PK_Tbl_InvoiceItem] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Tbl_InvoiceItem_Tbl_Invoice] FOREIGN KEY ([InvoiceId]) REFERENCES [dbo].[Tbl_Invoice] ([Id])
);





