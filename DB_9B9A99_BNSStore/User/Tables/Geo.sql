CREATE TABLE [User].[Geo] (
    [UserID]    INT            NOT NULL,
    [GeoID]     TINYINT        NOT NULL,
    [CountryID] SMALLINT       NOT NULL,
    [City]      NVARCHAR (100) NULL,
    [Address]   NVARCHAR (100) NULL,
    [ZIPCode]   VARCHAR (16)   NULL,
    [Main]      BIT            CONSTRAINT [DF_Geo_CurrentGeo] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Geo] PRIMARY KEY CLUSTERED ([UserID] ASC, [GeoID] ASC),
    CONSTRAINT [FK_Geo_Account] FOREIGN KEY ([UserID]) REFERENCES [User].[Account] ([UserID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_Geo_CountryList] FOREIGN KEY ([CountryID]) REFERENCES [Geo].[CountryList] ([CountryID])
);

