USE supermarket;  
GO  

EXEC master.dbo.sp_configure 'show advanced options', 1
RECONFIGURE
EXEC master.dbo.sp_configure 'xp_cmdshell', 0
RECONFIGURE

SELECT productName, productPrice  
FROM tbProduct  
WHERE id = 26  
FOR XML RAW;  
GO  

SELECT DISTINCT C.id, C.clientName, C.clientTelephoneNumber, C.clientEmail
FROM tbClient C JOIN tbBonusCard BC ON C.id = BC.idClient
WHERE C.clientEmail LIKE '%@gmail%'
FOR XML AUTO;
GO

SELECT DISTINCT C.id, C.clientName 
FROM tbClient C JOIN tbBonusCard BC ON C.id = BC.idClient
WHERE C.clientEmail LIKE '%@gmail%'
FOR XML AUTO, ROOT('gmailUsers');
GO

SELECT
    1 AS Tag,
    NULL AS Parent,
	P.id AS [Product!1!id],
	P.productName AS [Product!1!productName!ELEMENT],
    P.productPrice AS [Product!1!productPrice!ELEMENT]
FROM
    tbProduct P	
ORDER BY [Product!1!id]
FOR XML EXPLICIT;
GO

SELECT 2+2  
FOR XML PATH;
GO

 
