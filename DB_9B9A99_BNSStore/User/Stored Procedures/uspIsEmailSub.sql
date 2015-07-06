-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation
-- =============================================

CREATE PROCEDURE [User].[uspIsEmailSub]
	 @UserID int = NULL,
	 @Username varchar(100) = NULL,
	 @EmailAddress nvarchar(254) = NULL,
	 @IsEmailSub bit = NULL OUTPUT
AS
	BEGIN
	    -- SET NOCOUNT ON added to prevent extra result sets from
	    -- interfering with SELECT statements.

	    SET NOCOUNT ON;

	    -- Insert statements for procedure here

	    IF @UserID IS NULL
		   BEGIN EXEC [User].[uspGetUserID]
				    @Username = @Username,
				    @EmailAddress = @EmailAddress,
				    @UserID = @UserID OUTPUT;
		   END;

	    IF @EmailAddress IS NULL
		   BEGIN EXEC [User].[uspGetMainEmailAddress]
				    @UserID = @UserID,
				    @EmailAddress = @EmailAddress OUTPUT;
		   END;


	    IF(SELECT [UserID] FROM [User].[EmailSub]
		  WHERE [EmailAddress] = @EmailAddress) IS NULL
		   BEGIN
			  SET @IsEmailSub = 0;
		   END;
	    ELSE
		   BEGIN
			  SET @IsEmailSub = 1;
		   END;
	END;
