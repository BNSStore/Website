CREATE TABLE [Lang].[Translation] (
    [LangID]     TINYINT        NOT NULL,
    [Keyword]    VARCHAR (100)  NOT NULL,
    [Context]    NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Translation] PRIMARY KEY CLUSTERED ([LangID] ASC, [Keyword] ASC),
    CONSTRAINT [FK_Translation_LangList] FOREIGN KEY ([LangID]) REFERENCES [Lang].[LangList] ([LangID]) ON DELETE CASCADE ON UPDATE CASCADE,
);

