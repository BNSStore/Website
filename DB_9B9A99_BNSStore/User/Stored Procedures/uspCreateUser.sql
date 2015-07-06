-- =============================================
-- Author:		Cosh_
-- Create date: 2014.9.13
-- Description:	Create simple user
-- =============================================
CREATE PROCEDURE [User].[uspCreateUser]
	@UserID int = NULL OUTPUT, 
	@Username varchar(100) = NULL,
	@DisplayName nvarchar(100) = @Username, 
	@PasswordSalt char(64) = NULL,
	@PasswordHash char(128) = NULL,
	@EmailAddress nvarchar(254) = NULL,
	@LangName varchar(100) = 'English',
	@LangID tinyint = NULL,
	@IP varchar(45) = NULL,
	@IPv4 varchar(15) = NULL,
	@IPv6 varchar(45) = NULL,
	@ProviderID int = NULL,
	@ProviderName varchar(100) = NULL

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRY
		IF @ProviderID IS NULL
		BEGIN
			EXEC Svc.uspGetProviderID @ProviderName = @ProviderName, @ProviderID = @ProviderID OUTPUT
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

		INSERT INTO [User].Account(Username, PasswordSalt, PasswordHash) 
		VALUES (@Username, @PasswordSalt, @PasswordHash)

		EXECUTE [User].uspGetUserID @Username = @Username, @UserID = @UserID OUTPUT

		EXEC [User].uspAddDisplayName @UserID = @UserID, @DisplayName = @DisplayName

		EXEC [User].uspAddEmailAddress @UserID = @UserID, @EmailAddress = @EmailAddress, @Main = 1

		EXEC [User].uspAddLang @UserID = @UserID, @LangName = @LangName, @Main = 1

		INSERT INTO [User].RegisterInfo (UserID, RegisterDateTime, RegisterIPv4, RegisterIPv6, ProviderID)
		VALUES(@UserID, GETDATE(), @IPv4, @IPv6, @ProviderID)
	END TRY
	BEGIN CATCH
		DELETE FROM [User].Account WHERE Username = @Username
		SET @UserID = -1
		 select
        error_message() as errormessage,
        error_number() as erronumber,
        error_state() as errorstate,
        error_procedure() as errorprocedure,
        error_line() as errorline;
	END CATCH
END


GO
GRANT EXECUTE
    ON OBJECT::[User].[uspCreateUser] TO [db_executor]
    AS [dbo];

