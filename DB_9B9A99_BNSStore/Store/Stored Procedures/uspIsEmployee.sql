-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation

-- =============================================
CREATE PROCEDURE Store.uspIsEmployee
	@UserID int = NULL,
	@Username varchar(100) = NULL,
	@IsEmployee bit = 0 OUTPUT
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

	DECLARE @EmployeeUserID int = NULL;
	SET @EmployeeUserID = (SELECT UserID FROM Store.Employee WHERE UserID = @UserID)
	IF @EmployeeUserID IS NOT NULL
	BEGIN
		SET @IsEmployee = 1
	END
	ELSE
	BEGIN
		SET @IsEmployee = 0
	END 

END

GO
GRANT EXECUTE
    ON OBJECT::[Store].[uspIsEmployee] TO [db_executor];

