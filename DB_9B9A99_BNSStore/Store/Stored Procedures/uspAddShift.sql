-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation

-- =============================================
CREATE PROCEDURE [Store].[uspAddShift] 

	@Username varchar(100) = NULL,
	@UserID int = NULL,
	@FirstName nvarchar(100) = NULL,
	@MiddleName nvarchar(100) = NULL,
	@LastName nvarchar(100) = NULL,
	@Date date = NULL,
	@Day tinyint = NULL,
	@Month tinyint = NULL,
	@Year smallint = NULL,
	@Store char(1) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

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

	INSERT INTO Store.Schedule ([Date], UserID, Store) VALUES (@Date, @UserID, @Store)



END

GO
GRANT EXECUTE
    ON OBJECT::[Store].[uspAddShift] TO [db_executor];

