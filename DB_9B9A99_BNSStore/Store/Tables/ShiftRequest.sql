CREATE TABLE [Store].[ShiftRequest] (
    [SenderID]   INT  NOT NULL,
    [RecieverID] INT  NOT NULL,
    [Date]       DATE NOT NULL,
    CONSTRAINT [PK_ShiftRequest] PRIMARY KEY CLUSTERED ([SenderID] ASC, [RecieverID] ASC, [Date] ASC)
);

