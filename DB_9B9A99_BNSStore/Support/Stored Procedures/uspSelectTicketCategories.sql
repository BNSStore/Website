-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation
-- =============================================
CREATE PROCEDURE Support.[uspSelectTicketCategories]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Store.TicketCategoryList ORDER BY TicketCategoryID;
END

GO
GRANT EXECUTE
    ON OBJECT::[Support].[uspSelectTicketCategories] TO [db_executor];

