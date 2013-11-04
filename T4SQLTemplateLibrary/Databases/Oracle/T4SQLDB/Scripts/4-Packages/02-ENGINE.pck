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
--	Created Date:		October 15, 2013, 11:30:05 PM
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


PROCEDURE GET_CONFIG
(
	outPoll_Interval	OUT NUMBER
);


PROCEDURE STANDBY_PING
(
	outSwitch_To_Mode	OUT VARCHAR2
);

PROCEDURE PRIMARY_PING
(
	outSwitch_To_Mode	OUT VARCHAR2
);


PROCEDURE GET_DB_SERVER_ENV
(
	outDatabase_Platform	OUT VARCHAR2,
	outDatabase_Product		OUT VARCHAR2,
	outProduct_Version		OUT VARCHAR2,
	outServer_Name			OUT VARCHAR2
);


PROCEDURE REGISTER_TEMPLATE
(
	inFull_Name				VARCHAR2,
	inModule				NVARCHAR2,
	inAssembly_String		VARCHAR2,
	inClass_Description		NVARCHAR2
);


PROCEDURE REGISTER_TEMPLATE_SPEC
(
	inClass_Name			VARCHAR2,
	inProperty_Name			NVARCHAR2,
	inDefault_Value			NVARCHAR2,
	inLink_State			NVARCHAR2,
	inProperty_Description	NVARCHAR2,
	inProperty_Order		NUMBER
);


PROCEDURE LOG_SYS_ERROR
(
	inReference		NVARCHAR2,
	inMessage		VARCHAR
);


PROCEDURE SERVICE_RESTART;


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


PROCEDURE GET_CONFIG
(
	outPoll_Interval	OUT NUMBER
)	AS
BEGIN
	outPoll_Interval	:= GET_POLL_INTERVAL();
END GET_CONFIG;


PROCEDURE SERVICE_PING
(
	inIs_Primary	CHAR
)	AS
BEGIN
	MERGE INTO T4SQL.ENGINE_SERVER		P
	USING
	(
		SELECT
			NVL(SYS_CONTEXT('USERENV', 'TERMINAL'), '?')	AS SERVER_NAME,
			SYSTIMESTAMP									AS SERVICE_BEAT,
			inIs_Primary									AS IS_PRIMARY,
			NVL(SYS_CONTEXT('USERENV', 'OS_USER'), '?')		AS SERVICE_ACCOUNT
		FROM
			DUAL
	)	C
	ON	(P.SERVER_NAME = C.SERVER_NAME)
	WHEN MATCHED THEN
		UPDATE SET
			P.SERVICE_BEAT		= C.SERVICE_BEAT,
			P.IS_PRIMARY		= C.IS_PRIMARY,
			P.SERVICE_ACCOUNT	= C.SERVICE_ACCOUNT
	WHEN NOT MATCHED THEN
		INSERT	(P.SERVER_NAME, P.SERVICE_BEAT, P.IS_PRIMARY, P.SERVICE_ACCOUNT)
		VALUES	(C.SERVER_NAME, C.SERVICE_BEAT, C.IS_PRIMARY, C.SERVICE_ACCOUNT);
END SERVICE_PING;


PROCEDURE STANDBY_PING
(
	outSwitch_To_Mode	OUT VARCHAR2
)	AS
	tInterval		NUMBER	:= GET_POLL_INTERVAL();
	tNow			DATE	:= SYSDATE;
	tPrimary_Beat	DATE;
BEGIN
	UPDATE	T4SQL.ENGINE_CONFIG
	SET		DATE_VALUE		= tNow
	WHERE	DATE_VALUE		<= tNow - tInterval
		AND	ELEMENT_NAME	= 'STANDBY_BEAT';

	IF SQL%ROWCOUNT > 0 THEN
		SELECT DATE_VALUE INTO tPrimary_Beat FROM T4SQL.ENGINE_CONFIG WHERE ELEMENT_NAME = 'PRIMARY_BEAT';
		IF (tNow - tPrimary_Beat) > (tInterval / 2) THEN
			outSwitch_To_Mode	:= 'Primary';
		ELSE
			outSwitch_To_Mode	:= 'Standby';
		END IF;
	END IF;

	SERVICE_PING('N');
	COMMIT;
END STANDBY_PING;


PROCEDURE PRIMARY_PING
(
	outSwitch_To_Mode	OUT VARCHAR2
)	AS
	tNow	DATE	:= SYSDATE;
BEGIN
	UPDATE	T4SQL.ENGINE_CONFIG
	SET		DATE_VALUE		= tNow
	WHERE	ELEMENT_NAME	= 'PRIMARY_BEAT';

	outSwitch_To_Mode		:= 'Primary';

	SERVICE_PING('Y');
	COMMIT;
END PRIMARY_PING;


PROCEDURE GET_DB_SERVER_ENV
(
	outDatabase_Platform	OUT VARCHAR2,
	outDatabase_Product		OUT VARCHAR2,
	outProduct_Version		OUT VARCHAR2,
	outServer_Name			OUT VARCHAR2
)	AS
BEGIN
	outDatabase_Platform	:= 'Oracle';
	outServer_Name			:= SYS_CONTEXT('USERENV', 'DB_NAME');

	SELECT
		banner,
		REGEXP_SUBSTR(banner, '\d+\.\d+(\.\d+)?(\.\d+)?')
	INTO
		outDatabase_Product,
		outProduct_Version
	FROM
		v$version
	WHERE
		banner LIKE 'Oracle Database%';
END GET_DB_SERVER_ENV;


PROCEDURE REGISTER_TEMPLATE
(
	inFull_Name				VARCHAR2,
	inModule				NVARCHAR2,
	inAssembly_String		VARCHAR2,
	inClass_Description		NVARCHAR2
)	AS
BEGIN
	MERGE INTO T4SQL.TEMPLATE_CLASS		T
	USING
	(
		SELECT
			inFull_Name 			AS FULL_NAME,
			inModule				AS MODULE,
			inAssembly_String		AS ASSEMBLY_STRING,
			inClass_Description		AS CLASS_DESCRIPTION,
			SYSDATE					AS REGISTER_TIME
		FROM
			DUAL
	)	R
	ON	(T.FULL_NAME = R.FULL_NAME)
	WHEN MATCHED THEN
		UPDATE SET
			T.MODULE			= R.MODULE,
			T.ASSEMBLY_STRING	= R.ASSEMBLY_STRING,
			T.START_TIME		= R.REGISTER_TIME,
			T.IS_ACTIVE			= 'Y',
			T.CLASS_DESCRIPTION	= R.CLASS_DESCRIPTION
	WHEN NOT MATCHED THEN
		INSERT	(FULL_NAME, MODULE, ASSEMBLY_STRING, CREATED_TIME, START_TIME, IS_ACTIVE, CLASS_DESCRIPTION)
		VALUES	(R.FULL_NAME, R.MODULE, R.ASSEMBLY_STRING, R.REGISTER_TIME, R.REGISTER_TIME, 'Y', R.CLASS_DESCRIPTION);
	COMMIT;
END REGISTER_TEMPLATE;


PROCEDURE REGISTER_TEMPLATE_SPEC
(
	inClass_Name			VARCHAR2,
	inProperty_Name			NVARCHAR2,
	inDefault_Value			NVARCHAR2,
	inLink_State			NVARCHAR2,
	inProperty_Description	NVARCHAR2,
	inProperty_Order		NUMBER
)	AS
BEGIN
	MERGE INTO T4SQL.TEMPLATE_SPEC	T
	USING
	(
		SELECT
			inClass_Name			AS CLASS_NAME,
			inProperty_Name			AS PROPERTY_NAME,
			inDefault_Value			AS DEFAULT_VALUE,
			inLink_State			AS LINK_STATE,
			inProperty_Description	AS PROPERTY_DESCRIPTION,
			inProperty_Order		AS PROPERTY_ORDER
		FROM
			DUAL
	)	R
	ON	(T.PROPERTY_NAME = R.PROPERTY_NAME AND T.CLASS_NAME = R.CLASS_NAME)
	WHEN MATCHED THEN
		UPDATE SET
			T.DEFAULT_VALUE			= R.DEFAULT_VALUE,
			T.LINK_STATE			= R.LINK_STATE,
			T.PROPERTY_DESCRIPTION	= R.PROPERTY_DESCRIPTION,
			T.PROPERTY_ORDER		= R.PROPERTY_ORDER
	WHEN NOT MATCHED THEN
		INSERT	(CLASS_NAME, PROPERTY_NAME, DEFAULT_VALUE, LINK_STATE, PROPERTY_DESCRIPTION, PROPERTY_ORDER)
		VALUES	(R.CLASS_NAME, R.PROPERTY_NAME, R.DEFAULT_VALUE, R.LINK_STATE, R.PROPERTY_DESCRIPTION, R.PROPERTY_ORDER);
	COMMIT;
END REGISTER_TEMPLATE_SPEC;


PROCEDURE LOG_SYS_ERROR
(
	inReference		NVARCHAR2,
	inMessage		VARCHAR
)	AS
BEGIN
	INSERT INTO T4SQL.EVENT_LOG (LOG_TIME, LOG_TYPE, REFERENCE_, MESSAGE_)
	VALUES (SYSTIMESTAMP, 'Error', inReference, inMessage);
	COMMIT;
END LOG_SYS_ERROR;


PROCEDURE SERVICE_RESTART
AS
BEGIN
	UPDATE	T4SQL.TEMPLATE_CLASS
	SET		IS_ACTIVE = 'N'
	WHERE	IS_ACTIVE = 'Y';
	COMMIT;
END SERVICE_RESTART;


END ENGINE;
/
