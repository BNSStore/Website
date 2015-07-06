CREATE TABLE [Store].[Employee] (
    [UserID]  INT     NOT NULL,
    [GroupID] TINYINT NOT NULL,
    [Manager] BIT     CONSTRAINT [DF_Employee_Manager] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED ([UserID] ASC),
    CONSTRAINT [FK_Employee_Account] FOREIGN KEY ([UserID]) REFERENCES [User].[Account] ([UserID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Employee_Group] FOREIGN KEY ([GroupID]) REFERENCES [Store].[Group] ([GroupID]) ON DELETE CASCADE
);

