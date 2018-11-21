use supermarket;
go

-- Скалярная функция
IF OBJECT_ID (N'dbo.averageValueForPeriod', N'FN') IS NOT NULL  
    DROP FUNCTION averageValueForPeriod;  
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

-- Подставляемая табличная функция


-- Многооператорная табличная функция


-- Рекурсивная функция или функция с табличной ОТВ
