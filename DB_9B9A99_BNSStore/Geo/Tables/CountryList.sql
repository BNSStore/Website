CREATE TABLE [Geo].[CountryList] (
    [CountryID]   SMALLINT      NOT NULL,
    [CountryName] VARCHAR (100) NOT NULL,
    [PhonePrefix] CHAR (4)      NULL,
    CONSTRAINT [PK_CountryList] PRIMARY KEY CLUSTERED ([CountryID] ASC)
);

