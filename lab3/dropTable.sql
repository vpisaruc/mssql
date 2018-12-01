SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID ('dropTablesByFilter', 'P') IS NOT NULL
   DROP PROCEDURE dropTablesByFilter;
GO

CREATE PROCEDURE dropTablesByFilter
	@inTableNameFilter    VARCHAR(50)
AS
BEGIN
	DECLARE @ExecStr     VARCHAR(250);
	DECLARE @TableName   VARCHAR(100);

	DECLARE cursorTableNames CURSOR FOR   
	        SELECT   '[' + ss.name + '].[' + st.name + ']'       table_name
				from sys.tables               st
					 inner join sys.schemas   ss    on ss.schema_id = st.schema_id
				where st.name LIKE @inTableNameFilter
				order by 1

	OPEN cursorTableNames;
    FETCH NEXT FROM cursorTableNames INTO @TableName;
	 
	WHILE @@FETCH_STATUS = 0
	BEGIN
	    SET @ExecStr = 'DROP TABLE ' + @TableName + ';';
		Exec (@ExecStr);

		FETCH NEXT FROM cursorTableNames INTO @TableName;
	END;

	CLOSE cursorTableNames;
	DEALLOCATE  cursorTableNames;

END
GO
