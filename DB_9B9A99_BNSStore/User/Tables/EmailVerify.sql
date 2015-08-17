CREATE TABLE [User].[EmailVerify]
(
	[EmailAddress] VARCHAR(254) NOT NULL PRIMARY KEY, 
    [VerifyString] CHAR(64) NOT NULL, 
    [UserID] INT NOT NULL,
	CONSTRAINT [FK_EmailVerify_Account] FOREIGN KEY ([UserID]) REFERENCES [User].[Account] ([UserID]) ON DELETE CASCADE ON UPDATE CASCADE
)
