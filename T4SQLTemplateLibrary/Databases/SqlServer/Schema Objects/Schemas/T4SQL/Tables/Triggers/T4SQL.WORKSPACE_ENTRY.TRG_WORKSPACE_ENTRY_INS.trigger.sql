CREATE TRIGGER T4SQL.TRG_WORKSPACE_ENTRY_INS
ON T4SQL.WORKSPACE_ENTRY
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
	DECLARE	@tCount INT, @tValidated INT, @tMatched INT, @tSeed_Workitem INT, @tSeed_Property INT;

	SET @tSeed_Workitem	= OBJECT_ID(N'T4SQL.SEED_WORKITEM');
	SET @tSeed_Property	= OBJECT_ID(N'T4SQL.SEED_PROPERTY');

	IF (UPDATE(WORKITEM_TABLE_NAME) OR UPDATE(PROPERTY_TABLE_NAME)) AND EXISTS
	(
		SELECT	1
		FROM
				deleted
		WHERE	OBJECT_ID(WORKITEM_TABLE_NAME)	= @tSeed_Workitem
			AND	OBJECT_ID(PROPERTY_TABLE_NAME)	= @tSeed_Property
	) AND NOT EXISTS
	(
		SELECT	1
		FROM	inserted
		WHERE	OBJECT_ID(WORKITEM_TABLE_NAME)	= @tSeed_Workitem
			AND	OBJECT_ID(PROPERTY_TABLE_NAME)	= @tSeed_Property
	)
	BEGIN
		RAISERROR(N'Built-in workspace T4SQL.SEED_... cannot be removed!', 16, 3);
		ROLLBACK TRANSACTION;
		RETURN;
	END;

	-- Check the existence of every workspace (WORKITEM_TABLE_NAME, PROPERTY_TABLE_NAME).

	SELECT @tCount = COUNT(*) FROM inserted;

	SELECT
		@tValidated	= COUNT(*)
	FROM
		inserted
	WHERE
		(OBJECT_ID(PROPERTY_TABLE_NAME, N'U') IS NOT NULL OR OBJECT_ID(PROPERTY_TABLE_NAME, N'V') IS NOT NULL)
		AND	OBJECT_ID(WORKITEM_TABLE_NAME, N'U') IS NOT NULL

	IF @tCount <> @tValidated
	BEGIN
		RAISERROR(N'The given schema or table does not exist!', 11, 1);
		ROLLBACK TRANSACTION;
		RETURN;
	END;

	-- Check the uniqueness of every workspace

	IF UPDATE(WORKITEM_TABLE_NAME)
		IF EXISTS
		(
			SELECT		1
			FROM		T4SQL.WORKSPACE_ENTRY
			GROUP BY	OBJECT_ID(WORKITEM_TABLE_NAME, N'U')
			HAVING		COUNT(*) > 1
		)
		BEGIN
			RAISERROR ('Workspace WORKITEM_TABLE_NAME object must be unique!', 11, 4);
			ROLLBACK TRANSACTION;
			RETURN;
		END;

	IF UPDATE(PROPERTY_TABLE_NAME)
		IF EXISTS
		(
			SELECT		1
			FROM		T4SQL.WORKSPACE_ENTRY
			GROUP BY	OBJECT_ID(PROPERTY_TABLE_NAME)
			HAVING		COUNT(*) > 1
		)
		BEGIN
			RAISERROR ('Workspace PROPERTY_TABLE_NAME object must be unique!', 11, 5);
			ROLLBACK TRANSACTION;
			RETURN;
		END;

	-- Compare new workspace with SEED workspace to check compatibility of table definitions.

	SELECT
		@tMatched = COUNT(*)
	FROM
		inserted
	WHERE
			T4SQL.META_MATCH_TABLE_DEFINITION(N'T4SQL.SEED_WORKITEM', WORKITEM_TABLE_NAME)	= 1
		AND	T4SQL.META_MATCH_TABLE_DEFINITION(N'T4SQL.SEED_PROPERTY', PROPERTY_TABLE_NAME)	= 1;

	IF @tCount <> @tMatched
	BEGIN
		RAISERROR(N'Some table definition of add-in workspace is not compatible with built-in workspace T4SQL.SEED_...', 11, 2);
		ROLLBACK TRANSACTION;
		RETURN;
	END;

	-- Create trigger for each new workspace to auto-copy properties' default values while inserting a new workitem.

	DECLARE @tTable_Name NVARCHAR(128), @tWorkitem_Tables AS T4SQL.TT_STRING_ARRAY;

	INSERT INTO @tWorkitem_Tables (STRING_ID, STRING_VALUE)
	SELECT
		ROW_NUMBER() OVER (ORDER BY I.WORKSPACE_ID)		ID,
		I.WORKITEM_TABLE_NAME
	FROM
		inserted	I;

	WHILE @tValidated > 0
	BEGIN
		SELECT	@tTable_Name = A.STRING_VALUE
		FROM	@tWorkitem_Tables		A
		WHERE	A.STRING_ID	= @tValidated;

		EXEC T4SQL.META_CREATE_WORKITEM_TRIGGER @tTable_Name;
		SET @tValidated = @tValidated - 1;
	END;
END;

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		‎April ‎10, ‎2013, ‏‎6:48:17 PM
--	Primary Host:		http://t4sql.codeplex.com
--	Change Log:
--	Author				Date			Comment
--
--
--
--
--	(Keep code clean rather than complicated code plus long comments.)
--
----------------------------------------------------------------------------------------------------
