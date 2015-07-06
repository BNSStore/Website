CREATE TABLE [User].[Description] (
    [UserID]      INT            NOT NULL,
    [Description] NVARCHAR (250) NULL,
    CONSTRAINT [PK_Description] PRIMARY KEY CLUSTERED ([UserID] ASC),
    CONSTRAINT [FK_Description_Account] FOREIGN KEY ([UserID]) REFERENCES [User].[Account] ([UserID]) ON DELETE CASCADE ON UPDATE CASCADE
);

