CREATE TABLE [User_Vis].[InfoVis] (
    [UserID] INT      NOT NULL,
    [ColID]  TINYINT  NOT NULL,
    [IDType] CHAR (1) NOT NULL,
    [ID]     INT      NOT NULL,
    CONSTRAINT [PK_Info_1] PRIMARY KEY CLUSTERED ([UserID] ASC, [ColID] ASC, [IDType] ASC, [ID] ASC),
    CONSTRAINT [FK_InfoVis_Info] FOREIGN KEY ([UserID]) REFERENCES [User].[Info] ([UserID]),
    CONSTRAINT [FK_InfoVis_InfoCol] FOREIGN KEY ([ColID]) REFERENCES [User_Vis].[InfoCol] ([ColID])
);

