-- Хранимую процедуру с курсором
USE supermarket
GO
-- Объявляем переменную
DECLARE @TableName varchar(255)
-- Объявляем курсор 
DECLARE TableCursor CURSOR FOR
SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE'
-- Открываем курсор и извлекаем первую запись
OPEN TableCursor
FETCH NEXT FROM TableCursor INTO @TableName
-- Проходим в цикле все записи
WHILE @@FETCH_STATUS = 0
BEGIN
   PRINT @TableName
FETCH NEXT FROM TableCursor INTO @TableName END
-- Чистим
CLOSE TableCursor
DEALLOCATE TableCursor