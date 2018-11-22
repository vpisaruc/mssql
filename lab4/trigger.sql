USE supermarket
GO
EXEC sp_configure 'clr strict security', 0; 
RECONFIGURE;
GO
sp_configure 'show advanced options', 1
GO
RECONFIGURE
GO
sp_configure 'clr enabled', 1
GO
RECONFIGURE
GO
 

IF EXISTS (SELECT name FROM sysobjects WHERE name = 'deleteMe')  
   DROP TRIGGER deleteMe;  
GO  

IF EXISTS (SELECT name FROM sys.assemblies WHERE name = 'MyClrCode')  
   DROP ASSEMBLY MyClrCode;  
GO  

CREATE ASSEMBLY MyClrCode 
AUTHORIZATION dbo
FROM 'D:\university\database\mssql\lab4\CLR\CLR\bin\Debug\CLR.dll' 
WITH PERMISSION_SET = SAFE -- EXTERNAL_ACCESS;  
GO   

CREATE TRIGGER deleteMe
ON tbClient
INSTEAD OF DELETE
AS
EXTERNAL NAME
MyClrCode.UserDefinedFunctions.deleteMe
GO

IF EXISTS (SELECT name FROM sysobjects WHERE name = 'deleteMe')  
   DROP TRIGGER deleteMe;  
GO  

IF EXISTS (SELECT name FROM sys.assemblies WHERE name = 'MyClrCode')  
   DROP ASSEMBLY MyClrCode;  
GO 