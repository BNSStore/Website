CREATE TABLE [User].[Session] (
    [UserID]        INT            NOT NULL,
    [SessionToken]  CHAR (32)      NOT NULL,
    [SessionName]   NVARCHAR (100) NULL,
    [LoginDateTime] DATETIME2 (0)  CONSTRAINT [DF_Session_LoginDate] DEFAULT (getdate()) NOT NULL,
    [LoginIPv4]     VARCHAR (15)   NULL,
    [LoginIPv6]     VARCHAR (45)   NULL,
    CONSTRAINT [PK_Session_1] PRIMARY KEY CLUSTERED ([UserID] ASC, [SessionToken] ASC),
    CONSTRAINT [FK_Session_Account] FOREIGN KEY ([UserID]) REFERENCES [User].[Account] ([UserID]) ON DELETE CASCADE ON UPDATE CASCADE
);

