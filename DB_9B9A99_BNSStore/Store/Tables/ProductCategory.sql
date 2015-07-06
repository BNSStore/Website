CREATE TABLE [Store].[ProductCategory] (
    [CategoryID]   TINYINT       IDENTITY (1, 1) NOT NULL,
    [CategoryName] VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_ProductCategory_1] PRIMARY KEY CLUSTERED ([CategoryID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ProductCategory]
    ON [Store].[ProductCategory]([CategoryName] ASC);

