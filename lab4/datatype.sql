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
 

IF EXISTS (SELECT name FROM sysobjects WHERE name = 'Telephone')  
   DROP TYPE Telephone;  
GO  

IF EXISTS (SELECT name FROM sys.assemblies WHERE name = 'MyClrCode')  
   DROP ASSEMBLY MyClrCode;  
GO  

CREATE ASSEMBLY MyClrCode 
AUTHORIZATION dbo
FROM 'D:\university\database\mssql\lab4\CLR\CLR\bin\Debug\CLR.dll' 
WITH PERMISSION_SET = SAFE -- EXTERNAL_ACCESS;  
GO   


CREATE TYPE dbo.Telephone  
EXTERNAL NAME MyClrCode.[Telephone];
GO

CREATE TABLE dbo.Test
( 
  id INT IDENTITY(1,1) NOT NULL, 
  t Telephone NULL,
);
GO

-- Testing inserts
-- Correct values 
INSERT INTO dbo.Test(t) VALUES('7-9690524677'); 
INSERT INTO dbo.Test(t) VALUES('373-79018454'); 
INSERT INTO dbo.Test(t) VALUES('1-8326752342');  
GO 

-- Check the data - byte stream
SELECT * FROM dbo.Test;

SELECT id, t.ToString() AS Telephone 
FROM dbo.Test;

DROP TABLE dbo.Test
GO

IF EXISTS (SELECT name FROM sysobjects WHERE name = 'Telephone')  
   DROP TYPE Telephone;  
GO  

IF EXISTS (SELECT name FROM sys.assemblies WHERE name = 'MyClrCode')  
   DROP ASSEMBLY MyClrCode;  
GO 