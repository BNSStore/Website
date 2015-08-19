CREATE TABLE [Svc].[SubType]
(
	[SubTypeID] INT NOT NULL IDENTITY (1, 1), 
	[ProviderID] INT NOT NULL , 
    [SubTypeName] VARCHAR(100) NOT NULL, 
    CONSTRAINT [PK_SubType] PRIMARY KEY ([SubTypeID], [ProviderID]), 
    CONSTRAINT [FK_SubType_Provider] FOREIGN KEY ([ProviderID]) REFERENCES [Svc].[Provider]([ProviderID]) 
)

GO

CREATE UNIQUE INDEX [IX_SubType_SubTypeID] ON [Svc].[SubType] ([SubTypeID])
