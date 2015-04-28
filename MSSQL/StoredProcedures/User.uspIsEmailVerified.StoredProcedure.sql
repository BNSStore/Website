USE [BNSStore]
GO
/****** Object:  StoredProcedure [User].[uspIsEmailVerified]    Script Date: 4/28/2015 2:29:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation
-- =============================================
CREATE PROCEDURE [User].[uspIsEmailVerified]
	@EmailAddress nvarchar(254) = NULL,
	@Verified bit = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    DECLARE @VerifyString varchar(64);
	EXEC [User].uspGetEmailVerifyString @EmailAddress = @EmailAddress, @VerifyString = @VerifyString
	IF @VerifyString IS NULL
	BEGIN
		SET @Verified = 1;
	END
	ELSE
	BEGIN
	   SET @Verified = 0;
	END
END

GO
