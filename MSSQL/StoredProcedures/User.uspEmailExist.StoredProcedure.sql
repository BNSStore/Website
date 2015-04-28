USE [BNSStore]
GO
/****** Object:  StoredProcedure [User].[uspEmailExist]    Script Date: 4/28/2015 2:29:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation
-- =============================================
CREATE PROCEDURE [User].[uspEmailExist]
	@EmailAddress nvarchar(254) = NULL,
	@Exist bit = 0 OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF (SELECT UserID FROM [User].Email WHERE EmailAddress = @EmailAddress) IS NOT NULL
	BEGIN
		SET @Exist = 1
	END
	ELSE
	BEGIN
		SET @Exist = 0
	END
END

GO
