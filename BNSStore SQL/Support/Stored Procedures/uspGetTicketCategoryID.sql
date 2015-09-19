CREATE PROCEDURE [Support].uspGetTicketCategoryID
    @TicketCategoryID tinyint = NULL OUTPUT,
    @TicketCategoryName varchar(100) = NULL 
AS
BEGIN

	SET NOCOUNT ON

	SET @TicketCategoryID = (SELECT TicketCategoryID FROM Support.TicketCategory WHERE TicketCategoryName = @TicketCategoryName)
END

