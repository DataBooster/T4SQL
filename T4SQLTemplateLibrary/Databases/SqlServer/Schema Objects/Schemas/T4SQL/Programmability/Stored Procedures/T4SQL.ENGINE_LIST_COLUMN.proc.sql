CREATE PROCEDURE T4SQL.ENGINE_LIST_COLUMN
(
	@inTable_Name	NVARCHAR(64)
)
AS
    SET NOCOUNT ON;

	/*
	SELECT
		COLUMN_NAME
	FROM
		INFORMATION_SCHEMA.COLUMNS
	WHERE
		TABLE_SCHEMA + '.' + TABLE_NAME	= @inTable_Name
	ORDER BY
		ORDINAL_POSITION;
	*/

	SELECT
		name	AS COLUMN_NAME,
		IS_NULLABLE
	FROM
		sys.columns
	WHERE
		object_id	= OBJECT_ID(@inTable_Name)
	ORDER BY
		column_id;

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		‎‎April ‎01, ‎2013, ‏‎1:15:59 AM
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
