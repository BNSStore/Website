CREATE PROCEDURE [Store].[uspAddShift] 
	@Username varchar(100) = NULL,
	@UserID int = NULL,
	@Date date = NULL,
	@Day tinyint = NULL,
	@Month tinyint = NULL,
	@Year smallint = NULL,
	@Store char(1) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	IF @UserID IS NULL AND @Username IS NOT NULL
	BEGIN
		EXEC [User].uspGetUserID @Username = @Username, @UserID = @UserID OUTPUT
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
