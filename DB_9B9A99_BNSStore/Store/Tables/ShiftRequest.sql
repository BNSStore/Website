CREATE TABLE [Store].[ShiftRequest] (
    [SenderID]   INT  NOT NULL,
    [RecieverID] INT  NOT NULL,
    [Date]       DATE NOT NULL,
    CONSTRAINT [PK_ShiftRequest] PRIMARY KEY CLUSTERED ([SenderID] ASC, [RecieverID] ASC, [Date] ASC),
	CONSTRAINT [FK_ShiftRequest_Account_SenderID] FOREIGN KEY ([SenderID]) REFERENCES [User].[Account] ([UserID]),
	CONSTRAINT [FK_ShiftRequest_Account_RecieverID] FOREIGN KEY ([RecieverID]) REFERENCES [User].[Account] ([UserID])
);

