CREATE TABLE [Svc].[Provider] (
    [ProviderID]   INT            IDENTITY (1, 1) NOT NULL,
    [ProviderName] NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_AccountProvider] PRIMARY KEY CLUSTERED ([ProviderID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_AccountProvider]
    ON [Svc].[Provider]([ProviderName] ASC);

