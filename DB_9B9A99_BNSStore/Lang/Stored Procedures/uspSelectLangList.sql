﻿CREATE PROCEDURE [Lang].[uspSelectLangList]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT LangName FROM Lang.LangList;
END