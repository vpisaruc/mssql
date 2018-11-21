-- Многооператорная табличная функция
USE supermarket
GO
IF OBJECT_ID (N'dbo.productUnderInputPrice', N'FN') IS NOT NULL
    DROP FUNCTION dbo.productUnderInputPrice
GO

CREATE FUNCTION dbo.productUnderInputPrice(@price INT)
RETURNS @tbProductsForInputPrice TABLE 
(
	productId INT NOT NULL,
    productName VARCHAR(200) NOT NULL, 
    productPrice INT, 
	productInStock TINYINT
)
AS
BEGIN
    INSERT INTO @tbProductsForInputPrice
    SELECT p.id, p.productName, p.productPrice, p.productInStock
    FROM tbProduct p
    WHERE p.productPrice < @price 
RETURN
END
GO


SELECT *
FROM dbo.productUnderInputPrice(500)