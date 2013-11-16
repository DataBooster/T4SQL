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
TYPE NAME_ARRAY IS TABLE OF NVARCHAR2(32) INDEX BY PLS_INTEGER;
	tWorkitem_Names			NAME_ARRAY;
	tSQL					VARCHAR2(2000);
	tHas_Condition			BOOLEAN;
	tStart_Build			CHAR(1);
	tObject_Code			CLOB;
	tCompiled_Error			VARCHAR2(4000);
BEGIN
	tSQL	:= 'SELECT WORKITEM_NAME FROM ' || inWorkitem_Table;

	tHas_Condition	:= RTRIM(inSearch_Conditions) IS NOT NULL;
	IF tHas_Condition THEN
		tSQL	:= tSQL || ' WHERE ' || inSearch_Conditions;
	END IF;

	tSQL	:= tSQL || ' ORDER BY BUILD_ORDER NULLS LAST, WORKITEM_NAME';

	EXECUTE IMMEDIATE tSQL BULK COLLECT INTO tWorkitem_Names;

	IF tWorkitem_Names.COUNT > 0 THEN
		outGenerated_Scripts	:= '------------------ Started at: ' || TO_CHAR(SYSDATE, 'YYYY-MM-DD HH24:MI:SS') || ' ------------------
';
	ELSE
		outGenerated_Scripts	:= '------------------ None workitem to build ------------------';
		RETURN;
	END IF;

	tSQL	:= 'UPDATE ' || inWorkitem_Table || ' SET START_BUILD = ''Y'' WHERE START_BUILD = ''N''';

	IF tHas_Condition THEN
		tSQL	:= tSQL || ' AND (' || inSearch_Conditions || ')';
	END IF;

	EXECUTE IMMEDIATE tSQL;

	tSQL	:= 'SELECT START_BUILD, OBJECT_CODE, COMPILED_ERROR FROM ' || inWorkitem_Table || ' WHERE WORKITEM_NAME = :workitem_name';

	DBMS_LOCK.SLEEP(6);

	FOR i IN tWorkitem_Names.FIRST .. tWorkitem_Names.LAST
	LOOP
		DBMS_LOB.APPEND(outGenerated_Scripts, '
------------------ Build started workitem: ' || tWorkitem_Names(i) || ' ------------------
');

		FOR attempt IN 1..60
		LOOP
			tStart_Build	:= NULL;
			tObject_Code	:= NULL;
			tCompiled_Error	:= NULL;

			EXECUTE IMMEDIATE tSQL INTO tStart_Build, tObject_Code, tCompiled_Error USING tWorkitem_Names(i);

			EXIT WHEN tStart_Build = 'N';

			DBMS_LOCK.SLEEP(3);
		END LOOP;

		IF tStart_Build IS NULL THEN
			DBMS_LOB.APPEND(outGenerated_Scripts, '
------------------ Error: The workitem has been deleted by others ------------------
');
			CONTINUE;
		ELSIF tStart_Build = 'Y' THEN
			DBMS_LOB.APPEND(outGenerated_Scripts, '
------------------ Error: Template Engine is down ------------------
');
			EXIT;
		END IF;

		IF tCompiled_Error IS NULL THEN
			DBMS_LOB.APPEND(outGenerated_Scripts, tObject_Code);
		ELSE
			DBMS_LOB.APPEND(outGenerated_Scripts, '/*
' || tCompiled_Error || '
*/');
		END IF;

		DBMS_LOB.APPEND(outGenerated_Scripts, '
------------------ Build ended - workitem: ' || tWorkitem_Names(i) || ' ------------------
');

	END LOOP;

	DBMS_LOB.APPEND(outGenerated_Scripts, '
------------------ Build Total: ' || TO_CHAR(tWorkitem_Names.COUNT) || ' Workitems. Ended at: '|| TO_CHAR(SYSDATE, 'YYYY-MM-DD HH24:MI:SS') || ' ------------------');

END BUILD_SCRIPTS;


END CMD;
/
