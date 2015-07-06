-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation
-- =============================================

CREATE PROCEDURE [Store].[uspHasMissedShift]
	  @UserID int = NULL,
	  @HasMissedShift bit = NULL OUTPUT
AS
	BEGIN
	    -- SET NOCOUNT ON added to prevent extra result sets from
	    -- interfering with SELECT statements.

	    SET NOCOUNT ON;

	    -- Insert statements for procedure here

	    IF(SELECT [UserID] FROM [Store].[Schedule]
		  WHERE [UserID] = @UserID AND
			   [Mark] = 0) IS NULL
		   BEGIN
			  SET @HasMissedShift = 0;
		   END;
	    ELSE
		   BEGIN
			  SET @HasMissedShift = 1;
		   END;
	END;
