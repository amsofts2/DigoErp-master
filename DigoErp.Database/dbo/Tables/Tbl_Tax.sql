CREATE TABLE [dbo].[Tbl_Tax] (
    [Id]      BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (500) NULL,
    [Rate]    DECIMAL (18)   NULL,
    [Type]    NVARCHAR (50)  NULL,
    [Enabled] BIT            NULL,
    CONSTRAINT [PK_Tbl_Tax] PRIMARY KEY CLUSTERED ([Id] ASC)
);



