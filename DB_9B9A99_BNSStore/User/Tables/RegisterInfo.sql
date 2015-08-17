CREATE TABLE [User].[RegisterInfo] (
    [UserID]           INT          NOT NULL,
    [RegisterDateTime] DATETIME     CONSTRAINT [DF_RegisterInfo_RegisterDate] DEFAULT (getdate()) NOT NULL,
    [RegisterIPv4]     VARCHAR (15) NULL,
    [RegisterIPv6]     VARCHAR (45) NULL,
    [ProviderID]       INT          NOT NULL,
    CONSTRAINT [PK_RegisterInfo] PRIMARY KEY CLUSTERED ([UserID] ASC),
    CONSTRAINT [FK_RegisterInfo_Account] FOREIGN KEY ([UserID]) REFERENCES [User].[Account] ([UserID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_RegisterInfo_Provider] FOREIGN KEY ([ProviderID]) REFERENCES [Svc].[Provider] ([ProviderID])
);

