CREATE PROCEDURE Support.[uspSelectTicketCategories]

AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM Store.TicketCategoryList ORDER BY TicketCategoryID;
END