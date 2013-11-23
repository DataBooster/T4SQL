CREATE FUNCTION T4SQL.META_GET_TABLE_IN_TRIGGER
(
	@inProc_ID	INT
)
RETURNS NVARCHAR(128)
AS
BEGIN
	DECLARE	@tTable_ID INT;

	SELECT
		@tTable_ID	= parent_id
	FROM
		sys.triggers	T
	WHERE
			T.type			= 'TR'
		AND	T.parent_class	= 1
		AND	T.object_id		= @inProc_ID;

	RETURN QUOTENAME(OBJECT_SCHEMA_NAME(@tTable_ID)) + N'.' + QUOTENAME(OBJECT_NAME(@tTable_ID));
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
--	Created Date:		April 19, 2013, 12:41:08 AM
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
