CREATE TABLE [Permission].[RolePolicy]
(
	[RoleID] SMALLINT NOT NULL , 
    [PolicyID] SMALLINT NOT NULL, 
    PRIMARY KEY ([RoleID], [PolicyID]), 
    CONSTRAINT [FK_RolePolicy_Role_RoleID] FOREIGN KEY ([RoleID]) REFERENCES [Permission].[Role]([RoleID]) ON DELETE CASCADE,
	CONSTRAINT [FK_RolePolicy_Policy_PolicyID] FOREIGN KEY ([PolicyID]) REFERENCES [Permission].[Policy]([PolicyID]) ON DELETE CASCADE
)
