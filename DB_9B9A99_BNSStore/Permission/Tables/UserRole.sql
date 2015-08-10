CREATE TABLE [Permission].[UserRole]
(
	[UserID] INT NOT NULL , 
    [RoleID] SMALLINT NOT NULL, 
    PRIMARY KEY ([UserID], [RoleID]), 
    CONSTRAINT [FK_UserRole_Account_UserID] FOREIGN KEY ([UserID]) REFERENCES [User].[Account]([UserID]) ON DELETE CASCADE,
	CONSTRAINT [FK_UserRole_Role_RoleID] FOREIGN KEY ([RoleID]) REFERENCES [Permission].[Role]([RoleID]) ON DELETE CASCADE
)
