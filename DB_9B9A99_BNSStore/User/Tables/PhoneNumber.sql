CREATE TABLE [User].[PhoneNumber] (
    [UserID]      INT           NOT NULL,
    [PhoneID]     TINYINT       NOT NULL,
    [CountryID]   SMALLINT      NOT NULL,
    [AreaCode]    NVARCHAR (10) NULL,
    [PhoneNumber] NVARCHAR (15) NOT NULL,
    [Main]        BIT           CONSTRAINT [DF_PhoneNumber_CurrentPhoneNumber] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PhoneNumber] PRIMARY KEY CLUSTERED ([UserID] ASC, [PhoneID] ASC),
    CONSTRAINT [FK_PhoneNumber_Account] FOREIGN KEY ([UserID]) REFERENCES [User].[Account] ([UserID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_PhoneNumber_CountryList] FOREIGN KEY ([CountryID]) REFERENCES [Geo].[CountryList] ([CountryID])
);

