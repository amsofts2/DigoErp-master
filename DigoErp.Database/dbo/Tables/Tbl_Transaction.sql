CREATE TABLE [dbo].[Tbl_Transaction] (
    [Id]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [Date]            NVARCHAR (50)  NULL,
    [Amount]          DECIMAL (18)   NULL,
    [AccountId]       BIGINT         NULL,
    [CustomerId]      BIGINT         NULL,
    [VendorId]        BIGINT         NULL,
    [Description]     NVARCHAR (500) NULL,
    [CategoryId]      BIGINT         NULL,
    [Recurring]       INT            NULL,
    [PaymentMethod]   INT            NULL,
    [Reference]       NVARCHAR (500) NULL,
    [Attachment]      NVARCHAR (500) NULL,
    [Invoice]         BIGINT         NULL,
    [BillId]          BIGINT         NULL,
    [TransactionType] INT            NULL,
    [Created_At]      DATETIME       NULL,
    [Updated_At]      DATETIME       NULL,
    CONSTRAINT [PK_Tbl_Transaction] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Tbl_Transaction_Tbl_Account] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[Tbl_Account] ([Id]),
    CONSTRAINT [FK_Tbl_Transaction_Tbl_Bill] FOREIGN KEY ([BillId]) REFERENCES [dbo].[Tbl_Bill] ([Id]),
    CONSTRAINT [FK_Tbl_Transaction_Tbl_Category] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Tbl_Category] ([Id]),
    CONSTRAINT [FK_Tbl_Transaction_Tbl_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Tbl_Customer] ([Id]),
    CONSTRAINT [FK_Tbl_Transaction_Tbl_Vendor] FOREIGN KEY ([VendorId]) REFERENCES [dbo].[Tbl_Vendor] ([Id])
);



