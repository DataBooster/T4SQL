CREATE OR REPLACE VIEW T4SQL.VW_ORDINAL_DATE
AS
SELECT
	DAY_,
	TRUNC(SYSDATE) + DAY_	AS DATE_
FROM
(
	SELECT
		1 - ORDINAL_NUMBER	AS DAY_
	FROM
		T4SQL.UTL_ORDINAL_NUMBER
)	D

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
--	Created Date:		October 14, 2013, 10:48:10 PM
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
