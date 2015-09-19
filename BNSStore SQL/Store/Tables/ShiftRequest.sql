CREATE TABLE [Store].[ShiftRequest] (
	[Date]       DATE NOT NULL,
    [SenderID]   INT  NOT NULL,
    [RecieverID] INT  NOT NULL,
    CONSTRAINT [PK_ShiftRequest] PRIMARY KEY CLUSTERED ([SenderID] ASC, [RecieverID] ASC, [Date] ASC)
);

