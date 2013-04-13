CREATE TRIGGER T4SQL.TRG_WORKSPACE_ENTRY_INS
ON T4SQL.WORKSPACE_ENTRY
AFTER INSERT, UPDATE 
AS 
BEGIN
    SET NOCOUNT ON;
	DECLARE	@tCount INT, @tValidated INT, @tMatched INT;

	SELECT @tCount = COUNT(*) FROM inserted;

	SELECT
		@tValidated	= COUNT(*)
	FROM
		INFORMATION_SCHEMA.TABLES	P,
		INFORMATION_SCHEMA.TABLES	W,
		inserted					I
	WHERE
			P.TABLE_NAME	= I.PROPERTY_TABLE_NAME
		AND	P.TABLE_SCHEMA	= I.WORKSPACE_SCHEMA
		AND W.TABLE_NAME	= I.WORKITEM_TABLE_NAME
		AND	W.TABLE_SCHEMA	= I.WORKSPACE_SCHEMA;

	IF @tCount <> @tValidated
	BEGIN
		RAISERROR(N'The given schema or table does not exist!', 11, 1);
		ROLLBACK TRANSACTION;
		RETURN;
	END;

	SELECT
		@tMatched = COUNT(*)
	FROM
		inserted
	WHERE
			T4SQL.MATCH_TABLE_DEFINITION(N'T4SQL', N'SEED_WORKITEM', WORKSPACE_SCHEMA, WORKITEM_TABLE_NAME)	= 1
		AND	T4SQL.MATCH_TABLE_DEFINITION(N'T4SQL', N'SEED_PROPERTY', WORKSPACE_SCHEMA, PROPERTY_TABLE_NAME)	= 1;

	IF @tCount <> @tMatched
	BEGIN
		RAISERROR(N'Some table definition of add-in workspace is not compatible with built-in workspace T4SQL.SEED_...', 11, 2);
		ROLLBACK TRANSACTION;
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
