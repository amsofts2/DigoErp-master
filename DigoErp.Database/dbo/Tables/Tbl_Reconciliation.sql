CREATE TABLE [dbo].[Tbl_Reconciliation] (
    [Id]             BIGINT          IDENTITY (1, 1) NOT NULL,
    [AccountId]      BIGINT          NULL,
    [StartDate]      NVARCHAR (50)   NULL,
    [EndDate]        NVARCHAR (50)   NULL,
    [ClosingBalance] DECIMAL (18, 2) NULL,
    [Status]         NVARCHAR (20)   NULL,
    [Created_By]     BIGINT          NULL,
    [Created_At]     DATETIME        NULL,
    [Updated_At]     DATETIME        NULL,
    CONSTRAINT [PK_Tbl_Reconciliation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Tbl_Reconciliation_Tbl_Account] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[Tbl_Account] ([Id]),
    CONSTRAINT [FK_Tbl_Reconciliation_Tbl_User] FOREIGN KEY ([Created_By]) REFERENCES [dbo].[Tbl_User] ([Id])
);

