CREATE TABLE [Support].[TicketCategory] (
    [TicketCategoryID]   TINYINT       IDENTITY (1, 1) NOT NULL,
    [TicketCategoryName] VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_TicketCategory_1] PRIMARY KEY CLUSTERED ([TicketCategoryID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_TicketCategory]
    ON [Support].[TicketCategory]([TicketCategoryName] ASC);

