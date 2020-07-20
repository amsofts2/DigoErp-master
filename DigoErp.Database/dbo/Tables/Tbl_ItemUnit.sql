CREATE TABLE [dbo].[Tbl_ItemUnit] (
    [Id]         INT            NOT NULL,
    [NameAr]     NVARCHAR (250) NULL,
    [NameEn]     NVARCHAR (250) NULL,
    [Created_At] DATETIME       NULL,
    [Updated_At] DATETIME       NULL,
    CONSTRAINT [PK_Tbl_ItemUnit] PRIMARY KEY CLUSTERED ([Id] ASC)
);

