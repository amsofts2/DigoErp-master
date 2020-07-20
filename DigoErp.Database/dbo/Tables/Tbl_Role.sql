CREATE TABLE [dbo].[Tbl_Role] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (256) NULL,
    [Code]        NVARCHAR (256) NULL,
    [Description] NVARCHAR (256) NULL,
    CONSTRAINT [PK_Tbl_Roles] PRIMARY KEY CLUSTERED ([Id] ASC)
);



