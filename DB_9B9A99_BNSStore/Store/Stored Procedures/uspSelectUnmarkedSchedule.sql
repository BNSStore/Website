-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation
-- =============================================

CREATE PROCEDURE [Store].[uspSelectUnmarkedSchedule]
	  @Username varchar(100) = NULL,
	  @UserID int = NULL
AS
	BEGIN
	    -- SET NOCOUNT ON added to prevent extra result sets from
	    -- interfering with SELECT statements.

	    SET NOCOUNT ON;

	    -- Insert statements for procedure here

	    IF @UserID IS NULL
		   BEGIN EXEC [User].[uspGetUserID]
				    @Username = @Username,
				    @UserID = @UserID OUTPUT;
		   END;
	    DECLARE @WorkingSchedule TABLE([Date] date,[UserID] int, [Store] char(1));
	    DECLARE @StartDate date,
			  @EndDate date;
	    SET @EndDate = CONVERT(date, GETDATE());
	    SET @StartDate = DATEADD(month, -1, @EndDate);

	    INSERT INTO @WorkingSchedule
	    EXEC [Store].[uspSelectSchedule]
		    @UserID = @UserID,
		    @StartDate = @StartDate,
		    @EndDate = @EndDate;

	    DECLARE @MarkingSchedule TABLE([Date] date, [UserID] int, [Store] char(1));
	    DECLARE @Date date,
			  @Store char(1);

	    DECLARE cur CURSOR LOCAL SCROLL STATIC
		   FOR SELECT [Date], [Store] FROM @WorkingSchedule;

	    OPEN cur;
	    FETCH NEXT FROM cur INTO @Date,
						    @Store;

	    WHILE @@FETCH_STATUS = 0
		   BEGIN

			  INSERT INTO @MarkingSchedule
			  SELECT [Date], [UserID], [Store] FROM [Store].[Schedule] s
			  WHERE [UserID] != @UserID AND
				   [Date] = @Date AND
				   [Store] = @Store AND
				   [Mark] IS NULL AND
				   (SELECT Manager FROM Store.Employee e WHERE e.UserID = s.UserID) = 0
			  FETCH NEXT FROM cur INTO @Date,
								  @Store;
		   END;

	    CLOSE cur;
	    DEALLOCATE cur;

	    SELECT * FROM @MarkingSchedule;
	END;

GO
GRANT EXECUTE
    ON OBJECT::[Store].[uspSelectUnmarkedSchedule] TO [db_executor];

