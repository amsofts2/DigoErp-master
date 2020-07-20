CREATE TABLE [dbo].[Tbl_Category] (
    [Id]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (500) NULL,
    [Color]      NVARCHAR (500) NULL,
    [Type]       INT            NULL,
    [Enabled]    BIT            NULL,
    [Created_At] DATETIME       NULL,
    [Updated_At] DATETIME       NULL,
    CONSTRAINT [PK_Tbl_Category] PRIMARY KEY CLUSTERED ([Id] ASC)
);





