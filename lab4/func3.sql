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
 

IF EXISTS (SELECT name FROM sysobjects WHERE name = 'calculateAverageBonusCount')  
   DROP FUNCTION ScalarFunc1;  
GO  

IF EXISTS (SELECT name FROM sys.assemblies WHERE name = 'MyClrCode')  
   DROP ASSEMBLY MyClrCode;  
GO  

CREATE ASSEMBLY MyClrCode 
AUTHORIZATION dbo
FROM 'D:\university\database\mssql\lab4\CLR\CLR\bin\Debug\CLR.dll' 
WITH PERMISSION_SET = SAFE -- EXTERNAL_ACCESS;  
GO   

CREATE FUNCTION calculateAverageBonusCount()   
RETURNS INT  
AS EXTERNAL NAME MyClrCode.UserDefinedFunctions.[calculateAverageBonusCount];  
GO  

SELECT dbo.calculateAverageBonusCount() as AverageBonusCount;
GO

IF EXISTS (SELECT name FROM sysobjects WHERE name = 'calculateAverageBonusCount')  
   DROP FUNCTION ScalarFunc1;  
GO  

IF EXISTS (SELECT name FROM sys.assemblies WHERE name = 'MyClrCode')  
   DROP ASSEMBLY MyClrCode;  
GO 