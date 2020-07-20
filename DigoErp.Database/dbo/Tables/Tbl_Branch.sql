CREATE TABLE [dbo].[Tbl_Branch] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (256) NULL,
    [Email]      NVARCHAR (256) NULL,
    [TaxNumber]  NVARCHAR (150) NULL,
    [Phone]      NVARCHAR (50)  NULL,
    [Address]    NVARCHAR (MAX) NULL,
    [Logo]       NVARCHAR (150) NULL,
    [Created_At] DATETIME       NULL,
    [Updated_At] DATETIME       NULL,
    CONSTRAINT [PK_Tbl_Branch] PRIMARY KEY CLUSTERED ([Id] ASC)
);

