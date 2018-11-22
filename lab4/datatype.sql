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
 

IF EXISTS (SELECT name FROM sysobjects WHERE name = 'Point')  
   DROP TYPE Point;  
GO  

IF EXISTS (SELECT name FROM sys.assemblies WHERE name = 'MyClrCode')  
   DROP ASSEMBLY MyClrCode;  
GO  

CREATE ASSEMBLY MyClrCode 
AUTHORIZATION dbo
FROM 'D:\university\database\mssql\lab4\CLR\CLR\bin\Debug\CLR.dll' 
WITH PERMISSION_SET = SAFE -- EXTERNAL_ACCESS;  
GO   


CREATE TYPE dbo.Point  
EXTERNAL NAME MyClrCode.[Point];
GO

CREATE TABLE dbo.Test
( 
  id INT IDENTITY(1,1) NOT NULL, 
  p Point NULL,
);
GO

-- Testing inserts
-- Correct values 
INSERT INTO dbo.Test(p) VALUES('12,15'); 
INSERT INTO dbo.Test(p) VALUES('1,0'); 
INSERT INTO dbo.Test(p) VALUES('21,8');  
GO 
-- An incorrect value 
INSERT INTO dbo.Test(p) VALUES('a,2');
GO

-- Check the data - byte stream
SELECT * FROM dbo.Test;

SELECT id, p.ToString() AS Point 
FROM dbo.Test;

DECLARE @p1 dbo.Point
SET @p1 = CAST('7,5' AS dbo.Point)
SELECT @p1.Distance() AS 'Distance'
GO
 
DECLARE @p1 dbo.Point, @p2 dbo.Point
SET @p1 = CAST('7,5' AS dbo.Point)
SET @p2 = CAST('7,10' AS dbo.Point)
SELECT @p1.DistanceFrom(@p2) AS 'Distance from point'
GO

DROP TABLE dbo.Test
GO

IF EXISTS (SELECT name FROM sysobjects WHERE name = 'Point')  
   DROP TYPE Point;  
GO  

IF EXISTS (SELECT name FROM sys.assemblies WHERE name = 'MyClrCode')  
   DROP ASSEMBLY MyClrCode;  
GO 