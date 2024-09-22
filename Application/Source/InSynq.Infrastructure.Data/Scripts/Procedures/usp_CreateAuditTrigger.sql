ALTER PROCEDURE [dbo].[usp_CreateAuditTrigger] (@tableName VARCHAR(100), @excludedColumns VARCHAR(100) = '[]')
AS
BEGIN
	DECLARE @sql VARCHAR(max)
	DECLARE @insert_columns VARCHAR(max)
	DECLARE @object_id INT
	DECLARE @object_name VARCHAR(255)
	DECLARE @triggerName VARCHAR(100)

	SET @sql = ''
	SET @insert_columns = ''

	SELECT @object_id = OBJECT_ID(@tableName);
	SELECT @object_name = OBJECT_NAME(@object_id);

	DECLARE @exclude TABLE (value VARCHAR(100))

	INSERT INTO @exclude SELECT value from openjson(CASE WHEN @excludedColumns IS NULL OR @excludedColumns = '' THEN '[]' ELSE @excludedColumns END)

	SELECT @insert_columns = @insert_columns + '[' + name + '],' + CHAR(13) + CHAR(10) + CHAR(9) + CHAR(9) + CHAR(9) + CHAR(9)
	FROM sys.columns
	WHERE object_id = @object_id
	AND name NOT IN (SELECT value FROM @exclude)
	AND is_computed = 0
	ORDER BY column_id;

	SELECT @insert_columns = LEFT(@insert_columns, LEN(@insert_columns) - 7);

	SET @triggerName = 'trg_Update_' + @object_name + 'Audit';

	IF EXISTS(
		SELECT 1 FROM sys.objects
		WHERE type = 'TR'
		AND name = @triggerName
		AND parent_object_id = @object_id
	)
		SET @sql = 'ALTER'
	ELSE
		SET @sql = 'CREATE'

	SET @sql = @sql + N' TRIGGER [' + OBJECT_SCHEMA_NAME(@object_id) + '].[' + @triggerName + '] ON [' + OBJECT_SCHEMA_NAME(@object_id) + '].[' + OBJECT_NAME(@object_id) + '] AFTER UPDATE AS ' + CHAR(13) + CHAR(10) + 'BEGIN' + CHAR(13) + CHAR(10);

	SET @sql = @sql + N'
	IF (SELECT COUNT(0)
		FROM
		(
			SELECT ' + @insert_columns + '
			FROM deleted d
			EXCEPT
			SELECT ' + @insert_columns + '
			FROM inserted i
		)x) > 0
	BEGIN
		INSERT INTO ' + OBJECT_SCHEMA_NAME(@object_id) + '.' + @object_name + '_aud (AuditTimeStamp, LogType, ' + @insert_columns + ')
		SELECT SYSUTCDATETIME(), ''UPD'', ' + @insert_columns + '
		FROM deleted
	END
END;';

	--PRINT @sql
	EXEC(@sql);

END
GO
