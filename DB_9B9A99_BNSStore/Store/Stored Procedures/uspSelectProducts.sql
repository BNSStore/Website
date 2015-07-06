-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation
-- =============================================
CREATE PROCEDURE [Store].[uspSelectProducts]
	@ProductID smallint = NULL,
	@Keyword varchar(100) = NULL,
	@CategoryID tinyint = NULL,
	@MinPrice smallmoney = NULL,
	@MaxPrice smallmoney = NULL,
	@Online bit = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	/*
	DECLARE @ProductTable TABLE(ProductID smallint, ProductName varchar(100), ProductPrice smallmoney, OnSalePrice smallmoney, [Online] bit);

	INSERT INTO @ProductTable (ProductID, ProductName, ProductPrice, OnSalePrice)
	SELECT ProductID, ProductName, ProductPrice, OnSalePrice FROM Store.Product WHERE
	((@ProductID IS NULL) OR (ProductID = @ProductID)) AND
	((@Keyword IS NULL) OR (ProductName LIKE '%' + @Keyword + '%')) AND
	((@MinPrice IS NULL) OR (ProductPrice >= @MinPrice) OR ((OnSalePrice IS NOT NULL)AND (OnSalePrice >= @MinPrice))) AND
	((@MaxPrice IS NULL) OR (ProductPrice <= @MaxPrice) OR ((OnSalePrice IS NOT NULL)AND (OnSalePrice <= @MaxPrice))) AND
	((@Online IS NULL) OR ([Online] = @Online));

	DECLARE cur CURSOR 
	LOCAL SCROLL STATIC
	FOR SELECT ProductID FROM @ProductTable;
	OPEN cur;
	FETCH NEXT FROM cur INTO @ProductID;

	WHILE @@FETCH_STATUS = 0 BEGIN
		IF((SELECT ProductID FROM Store.ProductCategory WHERE ProductID = @ProductID AND ((@CategoryID IS NULL) OR (CategoryID = @CategoryID))) IS NULL)
		BEGIN
			DELETE FROM @ProductTable WHERE ProductID = @ProductID;
		END
		FETCH NEXT FROM cur INTO @ProductID;
	END

	CLOSE cur;
	DEALLOCATE cur;
	SELECT * FROM @ProductTable ORDER BY [ProductPrice], [OnSalePrice];
	*/

	SELECT * FROM Store.Product WHERE
	((@ProductID IS NULL) OR (ProductID = @ProductID)) AND
	((@Keyword IS NULL) OR (ProductName LIKE '%' + @Keyword + '%')) AND
	((@MinPrice IS NULL) OR (ProductPrice >= @MinPrice) OR ((OnSalePrice IS NOT NULL)AND (OnSalePrice >= @MinPrice))) AND
	((@MaxPrice IS NULL) OR (ProductPrice <= @MaxPrice) OR ((OnSalePrice IS NOT NULL)AND (OnSalePrice <= @MaxPrice))) AND
	((@Online IS NULL) OR ([Online] = @Online)) AND
	((@CategoryID IS NULL) OR ([CategoryID] = @CategoryID)) ORDER BY CategoryID, ProductName;
	
END

GO
GRANT EXECUTE
    ON OBJECT::[Store].[uspSelectProducts] TO [db_executor];

