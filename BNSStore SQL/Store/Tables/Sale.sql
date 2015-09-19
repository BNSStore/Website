CREATE TABLE [Store].[Sale] (
    [Date]          DATE       NOT NULL,
    [Store]         CHAR (1)   NOT NULL,
    [ProductID]     SMALLINT   NOT NULL,
    [ProductPrice]  SMALLMONEY NOT NULL,
    [Count]         SMALLINT   NOT NULL,
    [EmployeePrice] SMALLMONEY NOT NULL,
    [EmployeeCount] SMALLINT   NOT NULL,
    CONSTRAINT [PK_Sale] PRIMARY KEY CLUSTERED ([Date] ASC, [Store] ASC, [ProductID] ASC),
    CONSTRAINT [FK_Sale_Product] FOREIGN KEY ([ProductID]) REFERENCES [Store].[Product] ([ProductID]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Sale]
    ON [Store].[Sale]([Date] ASC);

