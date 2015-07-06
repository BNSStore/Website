CREATE TABLE [User_Vis].[GeoVis] (
    [UserID] INT      NOT NULL,
    [GeoID]  TINYINT  NOT NULL,
    [ColID]  TINYINT  NOT NULL,
    [IDType] CHAR (1) NOT NULL,
    [ID]     INT      NOT NULL,
    CONSTRAINT [PK_Geo_1] PRIMARY KEY CLUSTERED ([UserID] ASC, [GeoID] ASC, [ColID] ASC, [IDType] ASC, [ID] ASC),
    CONSTRAINT [FK_GeoVis_Geo] FOREIGN KEY ([UserID], [GeoID]) REFERENCES [User].[Geo] ([UserID], [GeoID]),
    CONSTRAINT [FK_GeoVis_GeoCol] FOREIGN KEY ([ColID]) REFERENCES [User_Vis].[GeoCol] ([ColID])
);

