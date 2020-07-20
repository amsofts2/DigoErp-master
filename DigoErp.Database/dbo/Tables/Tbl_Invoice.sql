CREATE TABLE [dbo].[Tbl_Invoice] (
    [Id]                  BIGINT          IDENTITY (1, 1) NOT NULL,
    [CustomerId]          BIGINT          NULL,
    [CurrencyId]          BIGINT          NULL,
    [InvoiceDate]         NVARCHAR (50)   NULL,
    [DueDate]             NVARCHAR (50)   NULL,
    [InvoiceNumber]       NVARCHAR (50)   NULL,
    [OrderNumber]         NVARCHAR (50)   NULL,
    [Notes]               NVARCHAR (500)  NULL,
    [Footer]              NVARCHAR (MAX)  NULL,
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
    [Status]              NVARCHAR (20)   NULL,
    CONSTRAINT [PK_Tbl_Invoice] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Tbl_Invoice_Tbl_Category] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Tbl_Category] ([Id]),
    CONSTRAINT [FK_Tbl_Invoice_Tbl_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Tbl_Currency] ([Id]),
    CONSTRAINT [FK_Tbl_Invoice_Tbl_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Tbl_Customer] ([Id])
);







