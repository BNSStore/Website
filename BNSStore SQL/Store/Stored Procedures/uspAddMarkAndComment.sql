CREATE PROCEDURE Store.uspAddMarkAndComment
	@Date date = NULL,
	@UserID int = NULL,
	@Store char = NULL,
	@Mark tinyint = NULL,
	@Comment nvarchar(MAX) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE Store.Schedule SET Mark = @Mark, Comment = @Comment WHERE [Date] = @Date AND UserID = @UserID AND Store = @Store;
END
