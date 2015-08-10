CREATE TABLE [Permission].[Role]
(
	[RoleID] SMALLINT NOT NULL IDENTITY (1, 1) , 
    [RoleName] VARCHAR(100) NOT NULL, 
    CONSTRAINT [PK_Role] PRIMARY KEY ([RoleID]) 
)

GO

CREATE UNIQUE INDEX [IX_Role_RoleName] ON [Permission].[Role] (RoleName)
