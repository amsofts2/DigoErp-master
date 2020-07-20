CREATE TABLE [dbo].[Tbl_Customer] (
    [Id]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (500) NULL,
    [Email]         NVARCHAR (256) NULL,
    [PhoneNumber]   NVARCHAR (MAX) NULL,
    [TaxNumber]     NVARCHAR (150) NULL,
    [CurrencyId]    BIGINT         NULL,
    [Website]       NVARCHAR (500) NULL,
    [Address]       NVARCHAR (MAX) NULL,
    [IsEnabled]     BIT            NULL,
    [Refrence]      NVARCHAR (250) NULL,
    [Created_At]    DATETIME       NULL,
    [Updated_At]    DATETIME       NULL,
    [CanLogin]      BIT            NULL,
    [BranchId]      INT            NULL,
    [AccountNumber] NVARCHAR (50)  NULL,
    [SadadNumber]   NVARCHAR (50)  NULL,
    CONSTRAINT [PK_Tbl_Customer] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Tbl_Customer_Tbl_Branch] FOREIGN KEY ([BranchId]) REFERENCES [dbo].[Tbl_Branch] ([Id]),
    CONSTRAINT [FK_Tbl_Customer_Tbl_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Tbl_Currency] ([Id])
);





