CREATE OR REPLACE PACKAGE T4SQL.CMD IS

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		October 15, 2013, 11:30:21 PM
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


PROCEDURE BUILD_SCRIPTS
(
	inWorkitem_Table		VARCHAR2,
	inSearch_Conditions		VARCHAR2,
	outGenerated_Scripts	OUT	CLOB
);


END CMD;
/
CREATE OR REPLACE PACKAGE BODY T4SQL.CMD IS


PROCEDURE BUILD_SCRIPTS
(
	inWorkitem_Table		VARCHAR2,
	inSearch_Conditions		VARCHAR2,
	outGenerated_Scripts	OUT	CLOB
)	AS
BEGIN
	IF NOT T4SQL.META.EXISTS_TABLE(inWorkitem_Table) THEN
		RAISE_APPLICATION_ERROR(-20942, 'inWorkitem_Table table does not exist in the database');
	END IF;


END BUILD_SCRIPTS;


END CMD;
/
