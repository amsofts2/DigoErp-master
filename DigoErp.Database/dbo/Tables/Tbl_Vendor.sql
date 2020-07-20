CREATE TABLE [dbo].[Tbl_Vendor] (
    [Id]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (500) NULL,
    [Email]         NVARCHAR (500) NULL,
    [PhoneNumber]   NVARCHAR (MAX) NULL,
    [UnPaid]        DECIMAL (18)   NULL,
    [IsEnabled]     BIT            NULL,
    [Created_At]    DATETIME       NULL,
    [Updated_At]    DATETIME       NULL,
    [TaxNumber]     NVARCHAR (MAX) NULL,
    [CurrencyId]    BIGINT         NULL,
    [Website]       NVARCHAR (500) NULL,
    [Address]       NVARCHAR (MAX) NULL,
    [Photo]         NVARCHAR (500) NULL,
    [Refrence]      NVARCHAR (500) NULL,
    [AccountNumber] NVARCHAR (50)  NULL,
    [SadadNumber]   NVARCHAR (50)  NULL,
    CONSTRAINT [PK_Tbl_Vendor] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Tbl_Vendor_Tbl_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Tbl_Currency] ([Id])
);



