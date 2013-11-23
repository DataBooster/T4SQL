CREATE PROCEDURE T4SQL.CMD_BUILD_SCRIPTS
(
	@inWorkitem_Table		NVARCHAR(128),
	@inSearch_Conditions	NVARCHAR(4000),
	@outGenerated_Scripts	NVARCHAR(MAX)	OUTPUT
)
AS
	SET NOCOUNT ON

	IF OBJECT_ID(@inWorkitem_Table, N'U') IS NULL
	BEGIN
		SET @outGenerated_Scripts = N'------ Invalid table name ''' + @inWorkitem_Table + N''' (@inWorkitem_Table) ------';
		PRINT @outGenerated_Scripts;
		RETURN -1;
	END;

	CREATE TABLE #WORKITEMS (ROW_INDEX INT PRIMARY KEY, WORKITEM_NAME NVARCHAR(32));

	DECLARE @tCnt INT, @tSQL NVARCHAR(2000);

	SET	@tSQL = N'SELECT ROW_NUMBER() OVER (ORDER BY ISNULL(BUILD_ORDER, 999999999), WORKITEM_NAME) AS ROW_INDEX, WORKITEM_NAME FROM ' + @inWorkitem_Table;

	IF LEN(@inSearch_Conditions) > 1
		SET @tSQL += N' WHERE ' + @inSearch_Conditions;

	SET @tSQL += N' ORDER BY ISNULL(BUILD_ORDER, 999999999), WORKITEM_NAME';

	INSERT INTO #WORKITEMS (ROW_INDEX, WORKITEM_NAME)
	EXECUTE sp_executesql @tSQL;

	SET @tCnt = @@ROWCOUNT;

	IF @tCnt = 0
	BEGIN
		SET @outGenerated_Scripts = N'------------------ None workitem to build ------------------';
		PRINT @outGenerated_Scripts;
		RETURN 0;
	END;

	SET @tSQL = N'UPDATE ' + @inWorkitem_Table + N' SET START_BUILD = 1 WHERE START_BUILD = 0';

	IF LEN(@inSearch_Conditions) > 1
		SET @tSQL += N' AND (' + @inSearch_Conditions + N')';

	EXECUTE sp_executesql @tSQL;	-- Start building

	WAITFOR DELAY '00:00:06';

	DECLARE @tI INT, @tAttempts INT, @tWorkitem_Name NVARCHAR(32), @tParmDef NVARCHAR(512);
	DECLARE @tStart_Build BIT, @tObject_Code NVARCHAR(MAX), @tCompiled_Error NVARCHAR(4000);

	SET @tSQL = N'SELECT @outStart_Build = START_BUILD, @outObject_Code = OBJECT_CODE, @outCompiled_Error = COMPILED_ERROR FROM '
				+ @inWorkitem_Table + N' WHERE WORKITEM_NAME = @inWorkitem_Name';
	SET @tParmDef = N'@inWorkitem_Name NVARCHAR(32), @outStart_Build BIT OUTPUT, @outObject_Code NVARCHAR(MAX) OUTPUT, @outCompiled_Error NVARCHAR(4000) OUTPUT';
	SET	@outGenerated_Scripts	= N'';
	SET @tI = 1;

	WHILE @tI <= @tCnt
	BEGIN
		SELECT @tWorkitem_Name = WORKITEM_NAME FROM #WORKITEMS WHERE ROW_INDEX = @tI;

		SET	@outGenerated_Scripts += N'
------------------ Build started workitem: ' + @tWorkitem_Name + N' ------------------
';
		SET @tAttempts = 60;

		WHILE 1 = 1
		BEGIN
			SET @tStart_Build = NULL;
			SET @tObject_Code = NULL;
			SET @tCompiled_Error = NULL;

			EXECUTE sp_executesql @tSQL, @tParmDef,
									@tWorkitem_Name,
									@tStart_Build OUTPUT,
									@tObject_Code OUTPUT,
									@tCompiled_Error OUTPUT;
			IF @tStart_Build = 1
			BEGIN
				IF @tAttempts > 0	-- Retry
				BEGIN
					WAITFOR DELAY '00:00:03';
					SET @tAttempts -= 1;
				END
				ELSE
					BREAK;
			END
			ELSE
				BREAK;
		END;

		IF @tStart_Build IS NULL
		BEGIN
			SET	@outGenerated_Scripts += N'
------------------ Error: The workitem has been deleted by others ------------------
';
			CONTINUE;
		END
		ELSE IF @tStart_Build = 1
		BEGIN
			SET	@outGenerated_Scripts += N'
------------------ Error: Template Engine is down ------------------
';
			BREAK;
		END;

		IF @tCompiled_Error IS NULL OR @tCompiled_Error = N''
			SET	@outGenerated_Scripts += @tObject_Code;
		ELSE
			SET	@outGenerated_Scripts += N'/*
' + @tCompiled_Error + N'
*/';

		SET	@outGenerated_Scripts += N'
------------------ Build ended workitem: ' + @tWorkitem_Name + N' ------------------
';
		SET @tI += 1;
	END;

	SET @outGenerated_Scripts += N'
------------------ Build Total: ' + CAST(@tCnt AS NVARCHAR(8)) + N' Workitems ------------------';

	EXEC T4SQL.CMD_PRINT_ALL_LINES @outGenerated_Scripts;
	
----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		June 20, 2013, 11:41:44 PM
--	Primary Host:		http://t4sql.codeplex.com
--	Change Log:
--	Author				Date			Comment
--
--
--
--
--	(Keep code clean)
--
----------------------------------------------------------------------------------------------------
