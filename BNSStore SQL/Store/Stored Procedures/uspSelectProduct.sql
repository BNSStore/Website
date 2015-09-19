CREATE PROCEDURE [Store].[uspSelectProduct]
	@ProductID smallint = NULL,
	@Keyword varchar(100) = NULL,
	@CategoryID tinyint = NULL,
	@MinPrice smallmoney = NULL,
	@MaxPrice smallmoney = NULL,
	@Online bit = NULL
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM Store.Product WHERE
	((@ProductID IS NULL) OR (ProductID = @ProductID)) AND
	((@Keyword IS NULL) OR (ProductName LIKE '%' + @Keyword + '%')) AND
	((@MinPrice IS NULL) OR (ProductPrice >= @MinPrice) OR ((OnSalePrice IS NOT NULL)AND (OnSalePrice >= @MinPrice))) AND
	((@MaxPrice IS NULL) OR (ProductPrice <= @MaxPrice) OR ((OnSalePrice IS NOT NULL)AND (OnSalePrice <= @MaxPrice))) AND
	((@Online IS NULL) OR ([Online] = @Online)) AND
	((@CategoryID IS NULL) OR ([CategoryID] = @CategoryID)) ORDER BY CategoryID, ProductName;
	
END