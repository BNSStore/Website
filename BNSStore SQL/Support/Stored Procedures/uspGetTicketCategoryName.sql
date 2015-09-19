CREATE PROCEDURE [Support].uspGetTicketCategoryName
    @TicketCategoryID tinyint = NULL,
    @TicketCategoryName varchar(100) = NULL OUTPUT
AS
BEGIN

	SET NOCOUNT ON

	SET @TicketCategoryName = (SELECT TicketCategoryName FROM Support.TicketCategory WHERE TicketCategoryID = @TicketCategoryID)
END