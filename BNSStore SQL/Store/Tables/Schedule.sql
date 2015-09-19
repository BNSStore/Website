CREATE TABLE [Store].[Schedule] (
    [Date]    DATE           NOT NULL,
    [UserID]  INT            NOT NULL,
    [Store]   CHAR (1)       NOT NULL,
    [Mark]    TINYINT        NULL,
    [Comment] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Schedule] PRIMARY KEY CLUSTERED ([Date] ASC, [UserID] ASC)
);

