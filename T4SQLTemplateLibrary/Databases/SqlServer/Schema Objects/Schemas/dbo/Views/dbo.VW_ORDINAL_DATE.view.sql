CREATE VIEW dbo.VW_ORDINAL_DATE
AS
SELECT
	DAY_,
	DATEADD(day, DAY_, CONVERT(date, GETDATE()))	AS DATE_
FROM
(
	SELECT
		1 - ORDINAL_NUMBER	AS DAY_
	FROM
		dbo.UTL_ORDINAL_NUMBER
)	D;

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		‎May ‎10, ‎2013, ‏‎9:45:02 PM
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
