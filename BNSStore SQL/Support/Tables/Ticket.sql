CREATE TABLE [Support].[Ticket] (
    [TicketID]         INT            IDENTITY (1, 1) NOT NULL,
    [TicketTitle]      NVARCHAR (200) NOT NULL,
    [TicketCategoryID] TINYINT        NOT NULL,
    [Rating]           TINYINT        NOT NULL,
    [Comment]          NVARCHAR (MAX) NOT NULL,
    [Email]            NVARCHAR (254) NULL,
    [PhoneNumber]      VARCHAR (50)   NULL,
    [Name]             NVARCHAR (100) NULL,
    CONSTRAINT [PK_Ticket_1] PRIMARY KEY CLUSTERED ([TicketID] ASC),
    CONSTRAINT [FK_Ticket_TicketCategory] FOREIGN KEY ([TicketCategoryID]) REFERENCES [Support].[TicketCategory] ([TicketCategoryID])
);

