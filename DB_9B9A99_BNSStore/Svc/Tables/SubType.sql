CREATE TABLE [Svc].[SubType]
(
	[SubTypeID] INT NOT NULL IDENTITY (1, 1), 
	[ProviderID] INT NOT NULL , 
    [SubTypeName] VARCHAR(100) NOT NULL, 
    CONSTRAINT [PK_EmailType] PRIMARY KEY ([SubTypeID])
)
