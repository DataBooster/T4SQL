CREATE TABLE T4SQL.SEED_WORKITEM
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
	START_BUILD				CHAR(1)			DEFAULT 'N'				NOT NULL,

	CONSTRAINT PK_SEED_WORKITEM PRIMARY KEY (WORKITEM_NAME),
	CONSTRAINT FK_SEED_WORKITEM_TEMPLATE_CLS FOREIGN KEY (TEMPLATE_NAME)
		REFERENCES T4SQL.TEMPLATE_CLASS(FULL_NAME)
		ON DELETE CASCADE
)
STORAGE (INITIAL 16K NEXT 8K);

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		‎October ‎14, ‎2013, ‏‎1:19:39 AM
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
