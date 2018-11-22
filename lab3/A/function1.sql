use supermarket;
go

-- Скалярная функция
IF OBJECT_ID (N'dbo.profitForPeriod', N'FN') IS NOT NULL  
    DROP FUNCTION profitForPeriod;  
GO 
CREATE FUNCTION dbo.profitForPeriod(@beginingDate date, @endDate date)
RETURNS int
AS 
BEGIN
	DECLARE @ret int;
	SELECT @ret = SUM(paymentAmount)
	FROM tbTransaction
	WHERE "date" BETWEEN @beginingDate and @endDate;
		IF (@ret IS NULL)
			SET @ret = 0;
	RETURN @ret;
END;
GO

SELECT dbo.profitForPeriod('2006-05-02', '2016-05-02') AS profitForPeriod;
GO