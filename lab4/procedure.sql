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
 

IF EXISTS (SELECT name FROM sysobjects WHERE name = 'procedureCLR')  
   DROP PROCEDURE procedureCLR;  
GO  

IF EXISTS (SELECT name FROM sys.assemblies WHERE name = 'MyClrCode')  
   DROP ASSEMBLY MyClrCode;  
GO  

CREATE ASSEMBLY MyClrCode 
FROM 'D:\university\database\mssql\lab4\CLR\CLR\bin\Debug\CLR.dll' 
WITH PERMISSION_SET = SAFE  
GO   

CREATE PROCEDURE procedureCLR ( @cashboxNumber INT )
AS
EXTERNAL NAME
MyClrCode.UserDefinedFunctions.procedureCLR
GO


EXEC procedureCLR 2
GO

IF EXISTS (SELECT name FROM sysobjects WHERE name = 'procedureCLR')  
   DROP PROCEDURE procedureCLR;  
GO  

IF EXISTS (SELECT name FROM sys.assemblies WHERE name = 'MyClrCode')  
   DROP ASSEMBLY MyClrCode;  
GO 