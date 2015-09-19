CREATE TABLE [Store].[Product] (
    [ProductID]     SMALLINT      IDENTITY (1, 1) NOT NULL,
    [ProductName]   VARCHAR (100) NOT NULL,
    [ProductPrice]  SMALLMONEY    NOT NULL,
    [EmployeePrice] SMALLMONEY    NOT NULL,
    [OnSalePrice]   SMALLMONEY    CONSTRAINT [DF_Product_OnSale] DEFAULT (NULL) NULL,
    [CategoryID]    TINYINT       NULL,
    [Online]        BIT           CONSTRAINT [DF_Product_Online] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([ProductID] ASC),
    CONSTRAINT [FK_Product_ProductCategory] FOREIGN KEY ([CategoryID]) REFERENCES [Store].[ProductCategory] ([CategoryID]) ON DELETE SET NULL ON UPDATE SET NULL
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Product]
    ON [Store].[Product]([ProductName] ASC);

