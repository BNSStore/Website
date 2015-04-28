USE [BNSStore]
GO
/****** Object:  StoredProcedure [Lang].[uspGetLangCode]    Script Date: 4/28/2015 2:29:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation
-- =============================================
CREATE PROCEDURE [Lang].[uspGetLangCode]
	@LangID tinyint = NULL,
	@LangName varchar(100) = NULL,
	@LangCode varchar(7) = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	
    -- Insert statements for procedure here

	SET NOCOUNT ON;

	IF @LangID IS NULL
	BEGIN
		EXEC Lang.uspGetLangID @LangName = @LangName, @LangID = @LangID OUTPUT
	END

	SET @LangCode = (SELECT LangCode FROM Lang.LangList WHERE LangID = @LangID)
END

GO
