CREATE TABLE [dbo].[Tbl_Transfer] (
    [Id]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [FromAccount]   BIGINT         NULL,
    [ToAccount]     BIGINT         NULL,
    [Amount]        NVARCHAR (500) NULL,
    [Date]          NVARCHAR (50)  NULL,
    [Description]   NVARCHAR (500) NULL,
    [PaymentMethod] INT            NULL,
    [Reference]     NVARCHAR (500) NULL,
    [Created_At]    DATETIME       NULL,
    [Updated_At]    DATETIME       NULL,
    CONSTRAINT [PK_Tbl_Transfers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Tbl_Transfers_Tbl_Account] FOREIGN KEY ([FromAccount]) REFERENCES [dbo].[Tbl_Account] ([Id]),
    CONSTRAINT [FK_Tbl_Transfers_Tbl_Account1] FOREIGN KEY ([ToAccount]) REFERENCES [dbo].[Tbl_Account] ([Id])
);

