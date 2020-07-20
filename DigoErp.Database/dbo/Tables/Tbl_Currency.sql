CREATE TABLE [dbo].[Tbl_Currency] (
    [Id]                  BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]                NVARCHAR (500) NULL,
    [Code]                NVARCHAR (10)  NULL,
    [Rate]                DECIMAL (18)   NULL,
    [Precision]           INT            NULL,
    [Symbol]              NVARCHAR (50)  NULL,
    [SymbolPosition]      NVARCHAR (50)  NULL,
    [DecimalMark]         NVARCHAR (20)  NULL,
    [Thousands_Separator] NVARCHAR (50)  NULL,
    [Enabled]             BIT            NULL,
    [Default_Currency]    BIT            NULL,
    [Created_At]          DATETIME       NULL,
    [Updated_At]          DATETIME       NULL,
    CONSTRAINT [PK_Tbl_Currency] PRIMARY KEY CLUSTERED ([Id] ASC)
);



