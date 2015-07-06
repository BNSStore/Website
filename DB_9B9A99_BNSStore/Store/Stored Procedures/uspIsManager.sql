-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation

-- =============================================
CREATE PROCEDURE Store.uspIsManager
	@UserID int = NULL,
	@Username varchar(100) = NULL,
	@IsManager bit = 0 OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF @UserID IS NULL
	BEGIN
		EXEC [User].uspGetUserID @Username = @Username, @UserID = @UserID OUTPUT
	END

	DECLARE @IsEmployee bit = 0;
	EXEC Store.uspIsEmployee @UserID = @UserID, @IsEmployee  = @IsEmployee OUTPUT;
	IF @IsEmployee = 0
	BEGIN
		SET @IsManager = 0;
	END
	ELSE
	BEGIN
		SET @IsManager = (SELECT Manager FROM Store.Employee WHERE UserID = @UserID)
	END

END

GO
GRANT EXECUTE
    ON OBJECT::[Store].[uspIsManager] TO [db_executor];

