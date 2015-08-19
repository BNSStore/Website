-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation

-- =============================================
CREATE PROCEDURE [Store].[uspDelShift]
	@Username varchar(100) = NULL,
	@UserID int = NULL,
	@FirstName nvarchar(100) = NULL,
	@MiddleName nvarchar(100) = NULL,
	@LastName nvarchar(100) = NULL,
	@Date date = NULL,
	@Day tinyint = NULL,
	@Month tinyint = NULL,
	@Year smallint = NULL
AS
BEGIN
	SET NOCOUNT ON;

	IF @UserID IS NULL AND @Username IS NOT NULL
	BEGIN
		EXEC [User].uspGetUserID @Username = @Username, @UserID = @UserID OUTPUT
	END
	ELSE IF @UserID IS NULL AND @Username IS NULL
	BEGIN
		SET @UserID = (SELECT se.UserID FROM Store.Employee se WHERE 
		se.UserID = (SELECT ui.UserID FROM [User].Info ui WHERE 
		((@FirstName IS NULL) OR (FirstName = @FirstName)) AND
		((@MiddleName IS NULL) OR (MiddleName = @MiddleName)) AND
		((@LastName IS NULL) OR (LastName = @LastName))
		 ))
	END

	IF @Date IS NULL
	BEGIN
		IF @Year IS NULL
		BEGIN
			SET @Year = YEAR(GETDATE())
		END

		IF @Month IS NULL
		BEGIN
			SET @Month = MONTH(GETDATE())
		END

		IF @Day IS NULL
		BEGIN
			SET @Day = DAY(GETDATE())
		END
		
		SET @Date = DATEADD(mm, (@Year - 1900) * 12 + @Month - 1 , @Day - 1);

	END

	DELETE FROM Store.Schedule WHERE UserID = @UserID AND [Date] = @Date



END

GO
GRANT EXECUTE
    ON OBJECT::[Store].[uspDelShift] TO [db_executor];

