CREATE TABLE [User].[DisplayName] (
    [UserID]              INT            NOT NULL,
    [DisplayName]         NVARCHAR (100) NOT NULL,
    [DisplayNameDateTime] DATETIME2 (0)  CONSTRAINT [DF_DisplayName_DisplayNameDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_DisplayName] PRIMARY KEY CLUSTERED ([UserID] ASC),
    CONSTRAINT [FK_DisplayName_Account] FOREIGN KEY ([UserID]) REFERENCES [User].[Account] ([UserID]) ON DELETE CASCADE ON UPDATE CASCADE
);

