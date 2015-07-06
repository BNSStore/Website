CREATE TABLE [Lang].[LangList] (
    [LangID]   TINYINT       IDENTITY (1, 1) NOT NULL,
    [LangName] VARCHAR (100) NOT NULL,
    [LangCode] VARCHAR (7)   NULL,
    CONSTRAINT [PK_LangList] PRIMARY KEY CLUSTERED ([LangID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_LangList]
    ON [Lang].[LangList]([LangName] ASC);

