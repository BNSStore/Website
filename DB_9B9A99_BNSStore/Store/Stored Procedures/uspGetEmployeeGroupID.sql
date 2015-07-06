-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation
-- =============================================

CREATE PROCEDURE [Store].[uspGetEmployeeGroupID]
	  @Username varchar(100) = NULL,
	  @UserID int = NULL,
	  @GroupID tinyint = NULL OUTPUT
AS
	BEGIN
	    -- SET NOCOUNT ON added to prevent extra result sets from
	    -- interfering with SELECT statements.

	    SET NOCOUNT ON;

	    -- Insert statements for procedure here

	    IF @UserID IS NULL
		   BEGIN EXEC [User].[uspGetUserID]
				    @Username = @Username,
				    @UserID = @UserID OUTPUT;
		   END;

		   SET @GroupID = (SELECT GroupID FROM Store.Employee WHERE UserID = @UserID)
	END;

GO
GRANT EXECUTE
    ON OBJECT::[Store].[uspGetEmployeeGroupID] TO [db_executor];

