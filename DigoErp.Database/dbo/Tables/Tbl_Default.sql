CREATE TABLE [dbo].[Tbl_Default] (
    [Id]             BIGINT         IDENTITY (1, 1) NOT NULL,
    [AccountId]      BIGINT         NULL,
    [CurrencyId]     BIGINT         NULL,
    [TaxId]          BIGINT         NULL,
    [PaymentMethod]  INT            NULL,
    [Language]       NVARCHAR (20)  NULL,
    [RecordsPerPage] INT            NULL,
    [Logo]           NVARCHAR (500) NULL,
    [CreatedBy]      BIGINT         NULL,
    CONSTRAINT [PK_Tbl_Default] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Tbl_Default_Tbl_Account] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[Tbl_Account] ([Id]),
    CONSTRAINT [FK_Tbl_Default_Tbl_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Tbl_Currency] ([Id]),
    CONSTRAINT [FK_Tbl_Default_Tbl_Tax] FOREIGN KEY ([TaxId]) REFERENCES [dbo].[Tbl_Tax] ([Id]),
    CONSTRAINT [FK_Tbl_Default_Tbl_User] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Tbl_User] ([Id])
);





