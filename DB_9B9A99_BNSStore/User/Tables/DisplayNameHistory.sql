CREATE TABLE [User].[DisplayNameHistory] (
    [UserID]              INT            NOT NULL,
    [DisplayName]         NVARCHAR (100) NOT NULL,
    [DisplayNameDateTime] DATETIME2 (0)  NOT NULL,
    CONSTRAINT [FK_DisplayNameHistory_DisplayName] FOREIGN KEY ([UserID]) REFERENCES [User].[Account] ([UserID]) ON DELETE CASCADE ON UPDATE CASCADE
);

