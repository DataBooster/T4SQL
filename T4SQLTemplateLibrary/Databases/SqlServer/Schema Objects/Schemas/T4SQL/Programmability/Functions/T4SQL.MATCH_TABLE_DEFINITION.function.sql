CREATE FUNCTION T4SQL.MATCH_TABLE_DEFINITION
(
	@inRef_Schema	NVARCHAR(64),
	@inRef_Table	NVARCHAR(64),
	@inTst_Schema	NVARCHAR(64),
	@inTst_Table	NVARCHAR(64)
)
RETURNS BIT
AS
BEGIN
	IF EXISTS
	(
		SELECT
			R.COLUMN_NAME,
			R.DATA_TYPE
		FROM
			INFORMATION_SCHEMA.COLUMNS	R
		WHERE
				R.TABLE_NAME	= @inRef_Table
			AND	R.TABLE_SCHEMA	= @inRef_Schema

		EXCEPT

		SELECT
			T.COLUMN_NAME,
			T.DATA_TYPE
		FROM
			INFORMATION_SCHEMA.COLUMNS	T
		WHERE
				T.TABLE_NAME	= @inTst_Table
			AND	T.TABLE_SCHEMA	= @inTst_Schema
	)
		RETURN 0;	-- Mismatched

	RETURN 1;		-- Matched
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
--	Created Date:		‎April ‎11, ‎2013, ‏‎6:00:09 PM
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
