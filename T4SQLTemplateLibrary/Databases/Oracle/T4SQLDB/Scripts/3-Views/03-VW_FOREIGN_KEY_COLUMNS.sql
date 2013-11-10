CREATE OR REPLACE VIEW T4SQL.VW_FOREIGN_KEY_COLUMNS AS
SELECT
	CR.OWNER,
	CR.TABLE_NAME,
	CR.CONSTRAINT_NAME,
	FC.COLUMN_NAME		AS FOREIGN_KEY_COLUMN,
	CF.NULLABLE			AS FOREIGN_KEY_NULLABLE,
	FC.POSITION,
	PC.OWNER			AS REFERENCED_SCHEMA,
	PC.TABLE_NAME		AS REFERENCED_TABLE,
	PC.COLUMN_NAME		AS REFERENCED_COLUMN,
	CP.NULLABLE			AS REFERENCED_NULLABLE
FROM
	ALL_TAB_COLUMNS		CP,
	ALL_CONS_COLUMNS	PC,
	ALL_TAB_COLUMNS		CF,
	ALL_CONS_COLUMNS	FC,
	ALL_CONSTRAINTS		CR
WHERE
		CP.COLUMN_NAME		= PC.COLUMN_NAME
	AND	CP.TABLE_NAME		= PC.TABLE_NAME
	AND	CP.OWNER			= PC.OWNER
	AND	PC.POSITION			= FC.POSITION
	AND	PC.CONSTRAINT_NAME	= CR.R_CONSTRAINT_NAME
	AND	PC.OWNER			= CR.R_OWNER
	AND	CF.COLUMN_NAME		= FC.COLUMN_NAME
	AND	CF.TABLE_NAME		= FC.TABLE_NAME
	AND	CF.OWNER			= FC.OWNER
	AND	FC.CONSTRAINT_NAME	= CR.CONSTRAINT_NAME
	AND	FC.OWNER			= CR.OWNER
	AND	CR.CONSTRAINT_TYPE	= 'R'
ORDER BY
	CR.OWNER,
	CR.TABLE_NAME,
	CR.CONSTRAINT_NAME,
	FC.POSITION

WITH READ ONLY;

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		??November ?04, ?2013, 11:05:07 PM
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
