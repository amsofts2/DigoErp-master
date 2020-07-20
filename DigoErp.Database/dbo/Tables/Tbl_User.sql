CREATE TABLE [dbo].[Tbl_User] (
    [Id]                BIGINT         IDENTITY (1, 1) NOT NULL,
    [FullName]          NVARCHAR (500) NULL,
    [Email]             NVARCHAR (256) NULL,
    [PhoneNumber]       NVARCHAR (MAX) NULL,
    [BranchId]          INT            NULL,
    [RoleId]            INT            NULL,
    [Photo]             NVARCHAR (150) NULL,
    [Password]          NVARCHAR (MAX) NULL,
    [ResetPasswordCode] NVARCHAR (MAX) NULL,
    [Language]          NVARCHAR (20)  NULL,
    [LandingPage]       NVARCHAR (20)  NULL,
    [IsEnabled]         BIT            NULL,
    [Created_At]        DATETIME       NULL,
    [Updated_At]        DATETIME       NULL,
    CONSTRAINT [PK_Tbl_User] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Tbl_User_Tbl_Branch] FOREIGN KEY ([BranchId]) REFERENCES [dbo].[Tbl_Branch] ([Id]),
    CONSTRAINT [FK_Tbl_User_Tbl_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Tbl_Role] ([Id])
);





