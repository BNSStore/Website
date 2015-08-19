﻿CREATE PROCEDURE [User].[uspAddEmailSub]
	@EmailAddress nvarchar(254) = NULL,
	@ProviderID int = NULL,
	@ProviderName varchar(100) = NULL,
	@SubTypeID int = NULL,
	@SubTypeName varchar(100) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	IF @SubTypeID IS NULL
	BEGIN
		EXEC [Svc].uspGetSubTypeID @ProviderID = @ProviderID, @ProviderName = @ProviderName, @SubTypeName = @SubTypeName, @SubTypeID = @SubTypeID OUTPUT
	END

	INSERT INTO [User].[EmailSub]([UserID], [EmailAddress], [SubTypeID]) VALUES(@UserID, @EmailAddress, @SubTypeID);
END;
