CREATE TABLE [dbo].[Tbl_Bill] (
    [Id]                  BIGINT          IDENTITY (1, 1) NOT NULL,
    [VendorId]            BIGINT          NULL,
    [CurrencyId]          BIGINT          NULL,
    [BillDate]            NVARCHAR (50)   NULL,
    [DueDate]             NVARCHAR (50)   NULL,
    [Number]              NVARCHAR (500)  NULL,
    [Amount]              DECIMAL (18, 2) NULL,
    [Status]              NVARCHAR (20)   NULL,
    [Notes]               NVARCHAR (500)  NULL,
    [OrderNumber]         NVARCHAR (500)  NULL,
    [CategoryId]          BIGINT          NULL,
    [Recurring]           INT             NULL,
    [Attachment]          NVARCHAR (500)  NULL,
    [Created_At]          DATETIME        NULL,
    [Updated_At]          DATETIME        NULL,
    [SubTotal]            DECIMAL (18, 2) NULL,
    [Discount_Percentage] DECIMAL (18, 2) NULL,
    [Discount]            DECIMAL (18, 2) NULL,
    [Tax]                 DECIMAL (18, 2) NULL,
    [GrandTotal]          DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_Tbl_Bill] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Tbl_Bill_Tbl_Category] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Tbl_Category] ([Id]),
    CONSTRAINT [FK_Tbl_Bill_Tbl_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Tbl_Currency] ([Id]),
    CONSTRAINT [FK_Tbl_Bill_Tbl_Vendor] FOREIGN KEY ([VendorId]) REFERENCES [dbo].[Tbl_Vendor] ([Id])
);







