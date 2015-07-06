CREATE TABLE [Lang].[Translation] (
    [LangID]     TINYINT        NOT NULL,
    [ProviderID] INT            NOT NULL,
    [Keyword]    VARCHAR (100)  NOT NULL,
    [Context]    NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Translation] PRIMARY KEY CLUSTERED ([LangID] ASC, [ProviderID] ASC, [Keyword] ASC),
    CONSTRAINT [FK_Translation_LangList] FOREIGN KEY ([LangID]) REFERENCES [Lang].[LangList] ([LangID]),
    CONSTRAINT [FK_Translation_Provider] FOREIGN KEY ([ProviderID]) REFERENCES [Svc].[Provider] ([ProviderID])
);

