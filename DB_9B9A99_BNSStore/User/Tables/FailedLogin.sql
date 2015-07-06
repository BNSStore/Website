CREATE TABLE [User].[FailedLogin] (
    [UserID]        INT           NOT NULL,
    [FailedLoginID] TINYINT       NOT NULL,
    [LoginDateTime] DATETIME2 (0) CONSTRAINT [DF_FailedLogin_LoginDate] DEFAULT (getdate()) NOT NULL,
    [LoginIPv4]     VARCHAR (15)  NULL,
    [LoginIPv6]     VARCHAR (45)  NULL,
    CONSTRAINT [PK_FailedLogin] PRIMARY KEY CLUSTERED ([UserID] ASC, [FailedLoginID] ASC),
    CONSTRAINT [FK_FailedLogin_Account] FOREIGN KEY ([UserID]) REFERENCES [User].[Account] ([UserID]) ON DELETE CASCADE ON UPDATE CASCADE
);

