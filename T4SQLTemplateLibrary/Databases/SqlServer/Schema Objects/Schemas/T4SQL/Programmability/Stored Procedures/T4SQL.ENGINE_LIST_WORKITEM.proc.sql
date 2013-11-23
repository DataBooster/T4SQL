CREATE PROCEDURE T4SQL.ENGINE_LIST_WORKITEM
(
	@inWorkitem_Table	NVARCHAR(128)
)
AS
	EXECUTE (N'SELECT
	I.WORKITEM_NAME,
	C.FULL_NAME		AS TEMPLATE_NAME
FROM
	T4SQL.TEMPLATE_CLASS	C,
	' + @inWorkitem_Table + N'	I
WHERE
		C.FULL_NAME		= I.TEMPLATE_NAME
	AND	C.IS_ACTIVE		= 1
	AND	I.START_BUILD	= 1
ORDER BY
	I.BUILD_ORDER
');

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		April 24, 2013, 12:45:01 PM
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
