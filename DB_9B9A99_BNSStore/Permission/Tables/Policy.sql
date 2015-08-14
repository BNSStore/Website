CREATE TABLE [Permission].[Policy]
(
	[PolicyID] SMALLINT IDENTITY (1, 1) NOT NULL , 
    [PolicyName] VARCHAR(100) NOT NULL, 
    CONSTRAINT [PK_Policy] PRIMARY KEY ([PolicyID]) 
)

GO

CREATE UNIQUE INDEX [IX_Policy_PolicyName] ON [Permission].[Policy] (PolicyName)
