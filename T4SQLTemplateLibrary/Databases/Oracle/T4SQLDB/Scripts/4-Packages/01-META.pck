CREATE OR REPLACE PACKAGE T4SQL.META IS

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		October ‎15, ‎2013, ‏‎11:20:35 PM
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


FUNCTION PARSENAME
(
	inQualified_Name	VARCHAR2,
	inObject_Piece		PLS_INTEGER
)	RETURN VARCHAR2;


FUNCTION EXISTS_TABLE
(
	inSchema_Name	VARCHAR2,
	inTable_Name	VARCHAR2
)	RETURN BOOLEAN;

FUNCTION EXISTS_TABLE
(
	inQualified_Name	VARCHAR2
)	RETURN BOOLEAN;


FUNCTION MATCH_TABLE_DEFINITION
(
	inRef_Schema	VARCHAR2,
	inRef_Table		VARCHAR2,
	inTst_Schema	VARCHAR2,
	inTst_Table		VARCHAR2
)	RETURN BOOLEAN;


PROCEDURE GET_TABLE_IN_TRIGGER
(
	inCall_Stack	VARCHAR2,		-- DBMS_UTILITY.FORMAT_CALL_STACK
	outTab_Schema	OUT VARCHAR2,
	outTab_Name		OUT VARCHAR2
);


PROCEDURE COPY_PROPERTY_DEFAULT
(
	inCall_Stack		VARCHAR2,	-- DBMS_UTILITY.FORMAT_CALL_STACK
	inWorkitem_Name		VARCHAR2,
	inTemplate_Name		VARCHAR2
);


PROCEDURE CREATE_WORKITEM_TRIGGER
(
	inWorkitem_Table	VARCHAR2
);


PROCEDURE CREATE_PROPERTY_VIEW
(
	inWorkitem_Table	VARCHAR2,
	inProperty_Table	VARCHAR2
);


PROCEDURE CREATE_WORKSPACE
(
	inWorkitem_Table			VARCHAR2,
	inProperty_Table			VARCHAR2,
	inWorkspace_Description		NVARCHAR2,
	inAutonomous_Owner			NVARCHAR2
);


END META;
/
CREATE OR REPLACE PACKAGE BODY T4SQL.META IS


FUNCTION PARSENAME
(
	inQualified_Name	VARCHAR2,
	inObject_Piece		PLS_INTEGER
)	RETURN VARCHAR2
IS
	tName				VARCHAR2(64);
BEGIN
	IF inObject_Piece = 1 THEN		-- Table Name
		tName	:= REGEXP_SUBSTR(inQualified_Name, '\.\s*(\w+)\s*$', subexpression => 1);
		IF tName IS NULL THEN
			RETURN REGEXP_SUBSTR(inQualified_Name, '\.\s*"([^"]+)"\s*$', subexpression => 1);
		ELSE
			RETURN UPPER(tName);
		END IF;
	ELSIF inObject_Piece = 2 THEN	-- Schema Name
		tName	:= REGEXP_SUBSTR(inQualified_Name, '^\s*(\w+)\s*\.', subexpression => 1);
		IF tName IS NULL THEN
			RETURN REGEXP_SUBSTR(inQualified_Name, '^\s*"([^"]+)"\s*\.', subexpression => 1);
		ELSE
			RETURN UPPER(tName);
		END IF;
	ELSE
		RETURN NULL;
	END IF;
END PARSENAME;


FUNCTION EXISTS_TABLE
(
	inSchema_Name	VARCHAR2,
	inTable_Name	VARCHAR2
)	RETURN BOOLEAN
IS
	tCount			PLS_INTEGER;
BEGIN
	SELECT
		COUNT(*)	INTO tCount
	FROM
		ALL_TABLES
	WHERE
			TABLE_NAME	= inTable_Name
		AND	OWNER		= inSchema_Name;

	RETURN (tCount = 1);
END EXISTS_TABLE;


FUNCTION EXISTS_TABLE
(
	inQualified_Name	VARCHAR2
)	RETURN BOOLEAN
IS
	tSchema_Name		VARCHAR2(30)	:= PARSENAME(inQualified_Name, 2);
	tTable_Name			VARCHAR2(30)	:= PARSENAME(inQualified_Name, 1);
BEGIN
	RETURN EXISTS_TABLE(tSchema_Name, tTable_Name);
END EXISTS_TABLE;


FUNCTION MATCH_TABLE_DEFINITION
(
	inRef_Schema	VARCHAR2,
	inRef_Table		VARCHAR2,
	inTst_Schema	VARCHAR2,
	inTst_Table		VARCHAR2
)	RETURN BOOLEAN
IS
	tMismatched		PLS_INTEGER;
BEGIN
	SELECT
		COUNT(*)	INTO	tMismatched
	FROM
	(
		SELECT
			COLUMN_NAME,
			DATA_TYPE
		FROM
			ALL_TAB_COLS
		WHERE
				TABLE_NAME	= UPPER(inRef_Table)
			AND	OWNER		= UPPER(inRef_Schema)
	)	R
	LEFT JOIN
	(
		SELECT
			COLUMN_NAME,
			DATA_TYPE
		FROM
			ALL_TAB_COLS
		WHERE
				TABLE_NAME	= UPPER(inTst_Table)
			AND	OWNER		= UPPER(inTst_Schema)
	)	T
	ON	(R.COLUMN_NAME = T.COLUMN_NAME AND R.DATA_TYPE = T.DATA_TYPE)
	WHERE
		T.COLUMN_NAME	IS NULL;

	RETURN (tMismatched = 0);
END MATCH_TABLE_DEFINITION;


PROCEDURE GET_TABLE_IN_TRIGGER
(
	inCall_Stack	VARCHAR2,		-- DBMS_UTILITY.FORMAT_CALL_STACK
	outTab_Schema	OUT VARCHAR2,
	outTab_Name		OUT VARCHAR2
)	AS
	tTrigger		VARCHAR2(64)	:= RTRIM(REGEXP_SUBSTR(inCall_Stack, '[^[:space:]]+[:space:]*$'), CHR(10));
	tTrg_Schema		VARCHAR2(30)	:= REGEXP_SUBSTR(tTrigger, '^[^.]+');
	tTrg_Name		VARCHAR2(30)	:= REGEXP_SUBSTR(tTrigger, '[^.]+$');
BEGIN
	SELECT
		TABLE_OWNER,
		TABLE_NAME
	INTO
		outTab_Schema,
		outTab_Name
	FROM
		ALL_TRIGGERS
	WHERE
			TRIGGER_NAME	= tTrg_Name
		AND	OWNER			= tTrg_Schema;
END GET_TABLE_IN_TRIGGER;


PROCEDURE COPY_PROPERTY_DEFAULT
(
	inCall_Stack		VARCHAR2,	-- DBMS_UTILITY.FORMAT_CALL_STACK
	inWorkitem_Name		VARCHAR2,
	inTemplate_Name		VARCHAR2
)	AS
	tWorkitem_Schema	VARCHAR2(30);
	tWorkitem_Table		VARCHAR2(30);
	tProperty_Table		VARCHAR2(64);
	tSQL				VARCHAR2(512);
BEGIN
	GET_TABLE_IN_TRIGGER(inCall_Stack, tWorkitem_Schema, tWorkitem_Table);

	SELECT
		PROPERTY_TABLE_NAME	INTO	tProperty_Table
	FROM
		T4SQL.WORKSPACE_ENTRY
	WHERE
		WORKITEM_TABLE_NAME	= tWorkitem_Schema || '.' || tWorkitem_Table;

	tSql	:= 'MERGE INTO ' || tProperty_Table || ' P
	USING
	(
		SELECT
			:Workitem_Name	AS	WORKITEM_NAME,
			PROPERTY_NAME,
			DEFAULT_VALUE,
			LINK_STATE
		FROM
			T4SQL.TEMPLATE_SPEC
		WHERE
			CLASS_NAME	= :Template_Class
	) S
	ON (P.PROPERTY_NAME = S.PROPERTY_NAME AND P.WORKITEM_NAME = S.WORKITEM_NAME)
	WHEN NOT MATCHED THEN
		INSERT (WORKITEM_NAME, PROPERTY_NAME, STRING_VALUE, LINK_STATE)
		VALUES (S.WORKITEM_NAME, S.PROPERTY_NAME, S.DEFAULT_VALUE, S.LINK_STATE)';

	EXECUTE IMMEDIATE tSql USING inWorkitem_Name, inTemplate_Name;
END COPY_PROPERTY_DEFAULT;


PROCEDURE CREATE_WORKITEM_TRIGGER
(
	inWorkitem_Table	VARCHAR2
)	AS
--	tSchema_Name		VARCHAR2(22) := PARSENAME(inWorkitem_Table, 2);
	tTable_Name			VARCHAR2(22) := PARSENAME(inWorkitem_Table, 1);
BEGIN
	EXECUTE IMMEDIATE 'CREATE OR REPLACE TRIGGER TRG_' || tTable_Name || '_IU
AFTER INSERT OR UPDATE ON ' || inWorkitem_Table || '
FOR EACH ROW
BEGIN
	T4SQL.META.COPY_PROPERTY_DEFAULT(DBMS_UTILITY.FORMAT_CALL_STACK, :new.WORKITEM_NAME, :new.TEMPLATE_NAME);
END;';
END CREATE_WORKITEM_TRIGGER;


PROCEDURE CREATE_PROPERTY_VIEW
(
	inWorkitem_Table	VARCHAR2,
	inProperty_Table	VARCHAR2
)	AS
	tSchema				VARCHAR2(22)	:= PARSENAME(inProperty_Table, 2);
	tTab				VARCHAR2(22)	:= PARSENAME(inProperty_Table, 1);
BEGIN
	EXECUTE IMMEDIATE 'CREATE OR REPLACE VIEW ' || tSchema || '.VW_' || tTab || '
AS
SELECT
	P.WORKITEM_NAME,
	S.PROPERTY_ORDER,
	P.PROPERTY_NAME,
	P.STRING_VALUE,
	P.LINK_STATE,
	DECODE(DECODE(P.STRING_VALUE, S.DEFAULT_VALUE, 0, 1) + DECODE(P.LINK_STATE, S.LINK_STATE, 0, 1),
		0, ''NO'', ''YES'')		AS CUSTOM,
	S.DEFAULT_VALUE,
	S.LINK_STATE				AS DEFAULT_LINK_STATE,
	S.PROPERTY_DESCRIPTION,
	S.CLASS_NAME,
	I.BUILD_ORDER
FROM
	' || inProperty_Table || '	P
	INNER JOIN
	' || inWorkitem_Table || '	I
	ON (P.WORKITEM_NAME = I.WORKITEM_NAME)
	INNER JOIN
	T4SQL.TEMPLATE_SPEC		S
	ON (P.PROPERTY_NAME = S.PROPERTY_NAME AND I.TEMPLATE_NAME = S.CLASS_NAME)

ORDER BY
	I.BUILD_ORDER,
	P.WORKITEM_NAME,
	S.PROPERTY_ORDER,
	S.PROPERTY_NAME

;';
END CREATE_PROPERTY_VIEW;


PROCEDURE CREATE_WORKSPACE
(
	inWorkitem_Table			VARCHAR2,
	inProperty_Table			VARCHAR2,
	inWorkspace_Description		NVARCHAR2,
	inAutonomous_Owner			NVARCHAR2
)	AS
	tWorkitem_Schema		VARCHAR2(30)	:= PARSENAME(inWorkitem_Table, 2);
	tWorkitem_Table			VARCHAR2(30)	:= PARSENAME(inWorkitem_Table, 1);
	tProperty_Schema		VARCHAR2(30)	:= PARSENAME(inProperty_Table, 2);
	tProperty_Table			VARCHAR2(30)	:= PARSENAME(inProperty_Table, 1);
	tQualified_Workitem		VARCHAR2(45);
	tQualified_Property		VARCHAR2(45);
BEGIN
	IF tWorkitem_Schema IS NULL OR tProperty_Schema IS NULL THEN
		RAISE_APPLICATION_ERROR(-21741, 'Schema must be specified in qualified table name');
	END IF;

	IF LENGTH(tWorkitem_Table) > 22 THEN
		RAISE_APPLICATION_ERROR(-20972, 'inWorkitem_Table table name is too long (maximum length 22 bytes)');
	END IF;

	IF LENGTH(tProperty_Table) > 22 THEN
		RAISE_APPLICATION_ERROR(-20972, 'inProperty_Table table name is too long (maximum length 22 bytes)');
	END IF;

	IF EXISTS_TABLE(tWorkitem_Schema, tWorkitem_Table) THEN
		RAISE_APPLICATION_ERROR(-20942, 'inWorkitem_Table table already exists in the database');
	END IF;

	IF EXISTS_TABLE(tProperty_Schema, tProperty_Table) THEN
		RAISE_APPLICATION_ERROR(-20942, 'inProperty_Table table already exists in the database');
	END IF;

	tQualified_Workitem	:= tWorkitem_Schema || '.' || tWorkitem_Table;
	tQualified_Property	:= tProperty_Schema || '.' || tProperty_Table;

	EXECUTE IMMEDIATE 'CREATE TABLE ' || tQualified_Workitem || '
(
	WORKITEM_NAME			NVARCHAR2(32)	NOT NULL,
	TEMPLATE_NAME			VARCHAR2(128)	NOT NULL,
	WORKITEM_DESCRIPTION	NVARCHAR2(256)	NOT NULL,
	WORKITEM_USER			NVARCHAR2(32),
	MODIFIED_TIME			TIMESTAMP(3)	DEFAULT SYSTIMESTAMP	NOT NULL,
	COMPILED_TIME			TIMESTAMP(3),
	COMPILED_ERROR			NVARCHAR2(2000),
	OBJECT_CODE				CLOB,
	BUILD_ORDER				NUMBER(4),
	START_BUILD				CHAR(1)			DEFAULT ''N''				NOT NULL,

	CONSTRAINT PK_' || tWorkitem_Table || ' PRIMARY KEY (WORKITEM_NAME),
	CONSTRAINT FK_' || tWorkitem_Table || '_CLS FOREIGN KEY (TEMPLATE_NAME)
		REFERENCES T4SQL.TEMPLATE_CLASS(FULL_NAME)
		ON DELETE CASCADE
);';

	EXECUTE IMMEDIATE 'CREATE TABLE ' || tQualified_Property || '
(
	WORKITEM_NAME	NVARCHAR2(32)	NOT NULL,
	PROPERTY_NAME	NVARCHAR2(64)	NOT NULL,
	STRING_VALUE	NVARCHAR2(2000)	NOT NULL,
	LINK_STATE		NVARCHAR2(256),

	CONSTRAINT PK_' || tProperty_Table || ' PRIMARY KEY (WORKITEM_NAME, PROPERTY_NAME),
	CONSTRAINT FK_' || tProperty_Table || '_WI FOREIGN KEY (WORKITEM_NAME)
		REFERENCES ' || tQualified_Workitem || '(WORKITEM_NAME)
		ON DELETE  CASCADE
);';

	INSERT INTO T4SQL.WORKSPACE_ENTRY
	(
		WORKITEM_TABLE_NAME,
		PROPERTY_TABLE_NAME,
		WORKSPACE_DESCRIPTION,
		AUTONOMOUS_OWNER
	)
	VALUES
	(
		tQualified_Workitem,
		tQualified_Property,
		inWorkspace_Description,
		inAutonomous_Owner
	);

	CREATE_PROPERTY_VIEW(tQualified_Workitem, tQualified_Property);

END CREATE_WORKSPACE;


END META;
/
