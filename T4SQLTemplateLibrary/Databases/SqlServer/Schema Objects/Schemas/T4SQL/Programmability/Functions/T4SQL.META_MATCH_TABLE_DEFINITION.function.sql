CREATE FUNCTION T4SQL.META_MATCH_TABLE_DEFINITION
(
	@inRef_Table	NVARCHAR(128),
	@inTst_Table	NVARCHAR(128)
)
RETURNS BIT
AS
BEGIN
	DECLARE	@tRef_Catalog NVARCHAR(64), @tRef_Schema NVARCHAR(64), @tRef_Tab_Name NVARCHAR(64);
	DECLARE	@tTst_Catalog NVARCHAR(64), @tTst_Schema NVARCHAR(64), @tTst_Tab_Name NVARCHAR(64);

	SET	@tRef_Tab_Name	= PARSENAME(@inRef_Table, 1);
	SET	@tRef_Schema	= PARSENAME(@inRef_Table, 2);
	SET	@tRef_Catalog	= PARSENAME(@inRef_Table, 3);

	SET	@tTst_Tab_Name	= PARSENAME(@inTst_Table, 1);
	SET	@tTst_Schema	= PARSENAME(@inTst_Table, 2);
	SET	@tTst_Catalog	= PARSENAME(@inTst_Table, 3);

	IF EXISTS
	(
		SELECT
			R.COLUMN_NAME,
			R.DATA_TYPE
		FROM
			INFORMATION_SCHEMA.COLUMNS	R
		WHERE
				R.TABLE_NAME		= @tRef_Tab_Name
			AND	(R.TABLE_SCHEMA		= @tRef_Schema		OR	@tRef_Schema	IS NULL)
			AND	(R.TABLE_CATALOG	= @tRef_Catalog		OR	@tRef_Catalog	IS NULL)

		EXCEPT

		SELECT
			T.COLUMN_NAME,
			T.DATA_TYPE
		FROM
			INFORMATION_SCHEMA.COLUMNS	T
		WHERE
				T.TABLE_NAME		= @tTst_Tab_Name
			AND	(T.TABLE_SCHEMA		= @tTst_Schema		OR	@tTst_Schema	IS NULL)
			AND	(T.TABLE_CATALOG	= @tTst_Catalog		OR	@tTst_Catalog	IS NULL)
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
--	Created Date:		April 11, 2013, 6:00:09 PM
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
