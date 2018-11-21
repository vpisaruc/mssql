-- Триггер AFTER
USE supermarket;
GO

CREATE TRIGGER deleteOrderAfterTransation
	ON tbTransaction
	AFTER DELETE
	AS
BEGIN
	DECLARE @deletedId INT
	SELECT @deletedId = (SELECT id FROM deleted)
	DELETE tbOrder
	WHERE idTransaction = @deletedId
END;
GO

DELETE tbTransaction WHERE id = 1;