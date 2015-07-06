CREATE TABLE [Store].[TicketCategoryList] (
    [TicketCategoryID]   TINYINT       IDENTITY (1, 1) NOT NULL,
    [TicketCategoryName] VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_TicketCategory_1] PRIMARY KEY CLUSTERED ([TicketCategoryID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_TicketCategory]
    ON [Store].[TicketCategoryList]([TicketCategoryName] ASC);

