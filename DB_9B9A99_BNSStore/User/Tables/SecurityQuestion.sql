CREATE TABLE [User].[SecurityQuestion] (
    [UserID]   INT            NOT NULL,
    [Question] NVARCHAR (254) NOT NULL,
    [Answer]   NVARCHAR (254) NOT NULL,
    CONSTRAINT [PK_Security] PRIMARY KEY CLUSTERED ([UserID] ASC, [Question] ASC),
    CONSTRAINT [FK_SecurityQuestion_Account] FOREIGN KEY ([UserID]) REFERENCES [User].[Account] ([UserID]) ON DELETE CASCADE ON UPDATE CASCADE
);

