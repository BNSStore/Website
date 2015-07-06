-- =============================================
-- Author:		Cosh_
-- Create date: 2014.9.13
-- Description:	Create simple user
-- =============================================
CREATE PROCEDURE [User].[uspLogin]
	@UserID int = NULL,
	@Username varchar(100) = NULL,
	@IP varchar(45) = NULL,
	@IPv4 varchar(15) = NULL,
	@IPv6 varchar(45) = NULL,
	@PasswordHash char(128) = NULL,
	@SessionToken char(32) = NULL,
	@OutputSessionToken char(32) = NULL OUTPUT
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

	IF @IP IS NOT NULL
	BEGIN
		IF LEN(@IP) > 15 AND @IPv6 IS NULL
		BEGIN
			SET @IPv6 = @IP
		END
		ELSE IF LEN(@IP) <= 15 AND @IPv4 IS NULL
		BEGIN
			SET @IPv4 = @IP
		END
	END

	--SessionToken Login
	IF @SessionToken IS NOT NULL
	BEGIN
		DECLARE @LoginDateTime datetime2(0) = NULL
		EXEC [User].uspGetLoginDateTime @UserID = @UserID, @SessionToken = @SessionToken, @LoginDateTime = @LoginDateTime OUTPUT
		--Success
		IF @LoginDateTime IS NOT NULL
		BEGIN
			--Check if passed max session time
			--Passed. Return new SessionToken
			/*IF DATEDIFF(hour,@LoginDateTime,GETDATE()) > 24
			BEGIN
				EXEC dbo.uspGenerateRandomString @Length = 32, @RandomString = @OutputSessionToken OUTPUT
				EXEC [User].uspUpdateSession @UserID = @UserID, @SessionToken = @SessionToken, @NewSessionToken = @OutputSessionToken, @IPv4 = @IPv4, @IPv6 = @IPv6
			END
			
			--Not Passed. Return original SessionToken
			ELSE
			*/
			BEGIN
				SET @OutputSessionToken = @SessionToken
			END
		END
		--Failed. Return NULL
		ELSE
		BEGIN
			SET @OutputSessionToken = NULL
		END
	END
	--PasswordHash Login
	ELSE
	BEGIN
		DECLARE @DBPasswordHash char(128) = NULL
		EXEC [User].uspGetPasswordHash @UserID = @UserID, @PasswordHash = @DBPasswordHash OUTPUT
		--Success Return new SessionToken
		IF @DBPasswordHash = @PasswordHash
		BEGIN
			EXEC [User].uspAddSession @UserID = @UserID, @IPv4 = @IPv4, @IPv6 = @IPv6, @SessionToken = @OutputSessionToken OUTPUT
		END
		--Failed Return NULL
		ELSE
		BEGIN
			EXEC [User].uspAddFailedLogin @UserID = @UserID, @IPv4 = @IPv4, @IPv6 = @IPv6
			SET @OutputSessionToken = NULL
		END
	END
END


GO
GRANT EXECUTE
    ON OBJECT::[User].[uspLogin] TO [db_executor]
    AS [dbo];

