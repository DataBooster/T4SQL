CREATE PROCEDURE T4SQL.ENGINE_LIST_DEFAULT_PROPERTY
AS
	SELECT
		D.CLASS_NAME,
		D.PROPERTY_NAME,
		D.DEFAULT_VALUE,
		D.LINK_STATE
	FROM
		T4SQL.TEMPLATE_SPEC		D,
		T4SQL.TEMPLATE_CLASS	C
	WHERE
			D.CLASS_NAME	= C.FULL_NAME
		AND	C.IS_ACTIVE		= 1
	ORDER BY
		D.PROPERTY_ORDER
;

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		April 23, 2013, 11:59:02 PM
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
