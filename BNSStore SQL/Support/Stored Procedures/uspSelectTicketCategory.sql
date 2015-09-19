CREATE PROCEDURE Support.[uspSelectTicketCategories]

AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM Support.TicketCategory ORDER BY TicketCategoryID;
END