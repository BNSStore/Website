CREATE TABLE [User].[Account] (
    [UserID]       INT           IDENTITY (1, 1) NOT NULL,
    [Username]     VARCHAR (100) NOT NULL,
    [PasswordSalt] CHAR (64)     NOT NULL,
    [PasswordHash] CHAR (128)    NOT NULL,
    CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED ([UserID] ASC)
);

