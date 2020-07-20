CREATE TABLE [dbo].[Tbl_Account] (
    [Id]             BIGINT         IDENTITY (1, 1) NOT NULL,
    [AccountName]    NVARCHAR (500) NULL,
    [Number]         NVARCHAR (50)  NULL,
    [IBANNumber]     NVARCHAR (50)  NULL,
    [CurrencyId]     BIGINT         NULL,
    [OpeningBalance] DECIMAL (18)   NULL,
    [BankName]       NVARCHAR (500) NULL,
    [BankPhone]      NVARCHAR (500) NULL,
    [BankAddress]    NVARCHAR (500) NULL,
    [DefaultAccount] BIT            NULL,
    [IsEnabled]      BIT            NULL,
    [Updated_At]     DATETIME       NULL,
    [Created_At]     DATETIME       NULL,
    CONSTRAINT [PK_Tbl_Account] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Tbl_Account_Tbl_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Tbl_Currency] ([Id])
);









