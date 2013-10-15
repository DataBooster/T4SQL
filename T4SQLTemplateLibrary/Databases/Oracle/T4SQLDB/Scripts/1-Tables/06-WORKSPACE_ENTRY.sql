CREATE TABLE T4SQL.WORKSPACE_ENTRY
(
	WORKSPACE_ID			NUMBER(9)		NOT NULL,
	WORKITEM_TABLE_NAME		VARCHAR2(128)	NOT NULL,
	PROPERTY_TABLE_NAME		VARCHAR2(128)	NOT NULL,
	WORKSPACE_DESCRIPTION	NVARCHAR2(128),
	AUTONOMOUS_OWNER		NVARCHAR2(64),

	CONSTRAINT PK_WORKSPACE_ENTRY PRIMARY KEY (WORKITEM_TABLE_NAME),
	CONSTRAINT UK_WORKSPACE_ENTRY_ID UNIQUE (WORKSPACE_ID),
	CONSTRAINT UK_WORKSPACE_ENTRY_PT UNIQUE (PROPERTY_TABLE_NAME)
)
ORGANIZATION INDEX
STORAGE (INITIAL 16K NEXT 16K);

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		‎October ‎14, ‎2013, ‏‎12:45:04 AM
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
