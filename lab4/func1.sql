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
 

IF EXISTS (SELECT name FROM sysobjects WHERE name = 'ScalarFunc1')  
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

CREATE FUNCTION ScalarFunc1(@beginingDate datetime, @endDate datetime)   
RETURNS INT  
AS EXTERNAL NAME MyClrCode.UserDefinedFunctions.[ScalarFunc1];  
GO  

SELECT dbo.ScalarFunc1('2006-05-02', '2016-05-02') as profitForPeriod;
GO