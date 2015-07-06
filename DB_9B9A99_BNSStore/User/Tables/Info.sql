CREATE TABLE [User].[Info] (
    [UserID]     INT            NOT NULL,
    [Gender]     CHAR (2)       CONSTRAINT [DF_Info_Gender] DEFAULT ('U') NULL,
    [Birthdate]  DATE           NULL,
    [FirstName]  NVARCHAR (100) NULL,
    [MiddleName] NVARCHAR (100) NULL,
    [LastName]   NVARCHAR (100) NULL,
    CONSTRAINT [PK_Info] PRIMARY KEY CLUSTERED ([UserID] ASC),
    CONSTRAINT [FK_Info_Account] FOREIGN KEY ([UserID]) REFERENCES [User].[Account] ([UserID]) ON DELETE CASCADE ON UPDATE CASCADE
);

