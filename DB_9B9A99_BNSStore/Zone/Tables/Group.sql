CREATE TABLE [Zone].[Group] (
    [ZoneID]   INT            IDENTITY (1, 1) NOT NULL,
    [Owner]    INT            NOT NULL,
    [ZoneName] NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_Group_1] PRIMARY KEY CLUSTERED ([ZoneID] ASC),
    CONSTRAINT [FK_Group_Account] FOREIGN KEY ([Owner]) REFERENCES [User].[Account] ([UserID]) ON DELETE CASCADE ON UPDATE CASCADE
);

