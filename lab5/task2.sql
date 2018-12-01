USE supermarket;  
GO  

DECLARE @iClients int
DECLARE @Clients xml
SELECT @Clients = c FROM OPENROWSET(BULK 'D:\university\database\mssql\lab5\task1.xml', 
                                SINGLE_BLOB) AS TEMP(c)
EXEC sp_xml_preparedocument @iClients OUTPUT, @Clients

SELECT *
FROM OPENXML (@iClients, '/gmailUsers/C')
WITH (id int , clientName varchar(100))
EXEC sp_xml_removedocument @iClients


 
