-- Рекурсивную хранимую процедуру или хранимую процедур с рекурсивным ОТВ
USE supermarket
GO

IF OBJECT_ID ( N'dbo.SelectProductInStockWithPrice', 'P' ) IS NOT NULL
      DROP PROCEDURE dbo.SelectProductInStockWithPrice
GO

CREATE PROCEDURE dbo.SelectProductInStockWithPrice
AS
    WITH ProductInStockWithPrice (productName)
    AS 
    (
        SELECT p.productName
        FROM tbProduct p 
        WHERE p.productInStock = 1

        UNION ALL

        SELECT p.productName
        FROM tbProduct p JOIN ProductInStockWithPrice piswp ON p.productName = piswp.productName
        WHERE p.productPrice > 500 
    )
	
	SELECT TOP 40 * FROM ProductInStockWithPrice
	
GO

EXEC dbo.SelectProductInStockWithPrice;
GO