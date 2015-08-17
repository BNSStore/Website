CREATE TABLE [User].[EmailSub] (
    [UserID]       INT            NOT NULL,
    [EmailAddress] NVARCHAR (254) NOT NULL,
    [SubTypeID] INT NOT NULL, 
    CONSTRAINT [PK_EmailSub] PRIMARY KEY CLUSTERED ([UserID] ASC, [EmailAddress] ASC, [SubTypeID] ASC),
    CONSTRAINT [FK_EmailSub_Email] FOREIGN KEY ([UserID], [EmailAddress]) REFERENCES [User].[Email] ([UserID], [EmailAddress]),
	CONSTRAINT [FK_EmailSub_SubType] FOREIGN KEY ([SubTypeID]) REFERENCES Svc.SubType (SubTypeID)
);

