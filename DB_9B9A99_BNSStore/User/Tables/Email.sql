CREATE TABLE [User].[Email] (
    [UserID]       INT            NOT NULL,
    [EmailAddress] NVARCHAR (254) NOT NULL,
    CONSTRAINT [PK_Email_1] PRIMARY KEY CLUSTERED ([UserID] ASC, [EmailAddress] ASC),
    CONSTRAINT [FK_Email_Account] FOREIGN KEY ([UserID]) REFERENCES [User].[Account] ([UserID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [IX_Email] UNIQUE NONCLUSTERED ([EmailAddress] ASC)
);


GO
ALTER TABLE [User].[Email] SET (LOCK_ESCALATION = AUTO);

