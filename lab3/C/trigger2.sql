-- Триггер INSTEAD OF
USE supermarket;
GO

CREATE TRIGGER productsDelete
ON tbProduct
INSTEAD OF DELETE
AS
UPDATE tbProduct
	SET productInStock = 0
WHERE ID =(SELECT id FROM deleted)
