CREATE TABLE [Permission].[UserPolicy]
(
	[UserID] INT NOT NULL , 
    [PolicyID] SMALLINT NOT NULL, 
    PRIMARY KEY ([UserID], [PolicyID]), 
    CONSTRAINT [FK_UserPolicy_Account_UserID] FOREIGN KEY ([UserID]) REFERENCES [User].[Account]([UserID]) ON DELETE CASCADE,
	CONSTRAINT [FK_UserPolicy_Policy_PolicyID] FOREIGN KEY ([PolicyID]) REFERENCES [Permission].[Policy]([PolicyID]) ON DELETE CASCADE
)
