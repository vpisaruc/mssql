use supermarket
go

-- Подставляемая табличная функция
IF OBJECT_ID (N'dbo.orderProductInStock', N'FN') IS NOT NULL
    DROP FUNCTION dbo.orderProductInStock
GO

CREATE FUNCTION dbo.orderProductInStock	(@transactionId int)
RETURNS TABLE
AS
RETURN (
    SELECT o.idTransaction, p.id, p.productName,
	CAST(
		CASE
			WHEN p.productInStock = 0 
				THEN 'Out of stock'
				ELSE 'In stock'
		END AS varchar(20)
		) as inStock
    FROM tbProduct p JOIN tbOrder o ON p.id = o.idProduct
    WHERE o.idTransaction = @transactionId
    )
GO

SELECT * FROM dbo.orderProductInStock(5)
GO