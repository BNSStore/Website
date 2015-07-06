CREATE TABLE [User_Vis].[PhoneNumberVis] (
    [UserID]  INT      NOT NULL,
    [PhoneID] TINYINT  NOT NULL,
    [ColID]   TINYINT  NOT NULL,
    [IDType]  CHAR (1) NOT NULL,
    [ID]      INT      NOT NULL,
    CONSTRAINT [PK_PhoneNumberVis] PRIMARY KEY CLUSTERED ([UserID] ASC, [PhoneID] ASC, [ColID] ASC, [IDType] ASC, [ID] ASC),
    CONSTRAINT [FK_PhoneNumberVis_PhoneNumber] FOREIGN KEY ([UserID], [PhoneID]) REFERENCES [User].[PhoneNumber] ([UserID], [PhoneID]),
    CONSTRAINT [FK_PhoneNumberVis_PhoneNumberCol] FOREIGN KEY ([ColID]) REFERENCES [User_Vis].[PhoneNumberCol] ([ColID])
);

