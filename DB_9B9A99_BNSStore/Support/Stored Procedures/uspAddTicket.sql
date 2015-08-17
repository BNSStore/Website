CREATE PROCEDURE Support.[uspAddTicket]
	@TicketTitle nvarchar(200) = NULL,
	@TicketCategoryName nvarchar(100) = NULL,
	@TicketCategoryID tinyint = NULL,
	@Rating tinyint = NULL,
	@Comment nvarchar(MAX) = NULL,
	@Email nvarchar(254) = NULL,
	@PhoneNumber varchar(15) = NULL,
	@Name nvarchar(100) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	IF @TicketCategoryID IS NULL
	BEGIN
		SET @TicketCategoryID = (SELECT TicketCategoryID FROM Store.TicketCategoryList WHERE TicketCategoryName = @TicketCategoryName)
	END
	IF @Rating > 10
	BEGIN
		SET @Rating = 10;
	END
	ELSE IF @Rating < 0
	BEGIN
		SET @Rating = 0;
	END
	INSERT INTO Store.Ticket (TicketTitle, TicketCategoryID, Rating, Comment, Email, PhoneNumber, Name) VALUES
	(@TicketTitle, @TicketCategoryID, @Rating, @Comment, @Email, @PhoneNumber, @Name)
END