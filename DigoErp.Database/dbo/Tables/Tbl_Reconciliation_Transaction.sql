CREATE TABLE [dbo].[Tbl_Reconciliation_Transaction] (
    [Id]               BIGINT          IDENTITY (1, 1) NOT NULL,
    [ReconciliationId] BIGINT          NULL,
    [Date]             NVARCHAR (50)   NULL,
    [Description]      NVARCHAR (500)  NULL,
    [Contact]          NVARCHAR (250)  NULL,
    [Deposit]          DECIMAL (18, 2) NULL,
    [Withdrawal]       DECIMAL (18, 2) NULL,
    [IsClear]          BIT             NULL,
    CONSTRAINT [PK_Tbl_Reconciliation_Transaction] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Tbl_Reconciliation_Transaction_Tbl_Reconciliation] FOREIGN KEY ([ReconciliationId]) REFERENCES [dbo].[Tbl_Reconciliation] ([Id])
);

