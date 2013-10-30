CREATE OR REPLACE PACKAGE T4SQL.ENGINE IS

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		October ?15, ?2013, ??11:30:05 PM
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


FUNCTION GET_POLL_INTERVAL
RETURN	NUMBER;



END ENGINE;
/
CREATE OR REPLACE PACKAGE BODY T4SQL.ENGINE IS


g_Polling_Interval	NUMBER(3);


FUNCTION GET_POLL_INTERVAL
RETURN	NUMBER
AS
BEGIN
	IF g_Polling_Interval IS NULL THEN
		SELECT	NUMBER_VALUE		INTO g_Polling_Interval
		FROM	T4SQL.ENGINE_CONFIG
		WHERE	ELEMENT_NAME = 'POLL_INTERVAL';
	END IF;
	RETURN g_Polling_Interval;
END GET_POLL_INTERVAL;



END ENGINE;
/
