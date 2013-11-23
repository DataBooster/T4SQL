CREATE PROCEDURE T4SQL.ENGINE_LIST_WORKING_PROPERTY
(
	@inProperty_Table	NVARCHAR(128)
)
AS
	EXECUTE (N'SELECT
	WORKITEM_NAME,
	PROPERTY_NAME,
	STRING_VALUE,
	LINK_STATE
FROM
	' + @inProperty_Table + N'
ORDER BY
	WORKITEM_NAME,
	PROPERTY_NAME
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
--	Created Date:		April 24, 2013, 4:39:58 PM
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
