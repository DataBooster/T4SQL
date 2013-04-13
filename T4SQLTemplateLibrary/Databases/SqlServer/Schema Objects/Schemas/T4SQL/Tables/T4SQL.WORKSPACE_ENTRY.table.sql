CREATE TABLE T4SQL.WORKSPACE_ENTRY
(
	WORKSPACE_ID			INT NOT NULL IDENTITY(1, 1),
	WORKSPACE_SCHEMA		NVARCHAR(30)	NOT NULL,
	WORKITEM_TABLE_NAME		NVARCHAR(30)	NOT NULL,
	PROPERTY_TABLE_NAME		NVARCHAR(30)	NOT NULL,
	AUTONOMOUS_OWNER		NVARCHAR(32),
	WORKSPACE_DESCRIPTION	NVARCHAR(128),

	CONSTRAINT PK_WORKSPACE_ENTRY PRIMARY KEY NONCLUSTERED (WORKSPACE_SCHEMA, WORKITEM_TABLE_NAME),
	CONSTRAINT UK_WORKSPACE_ENTRY_ID UNIQUE CLUSTERED (WORKSPACE_ID),
	CONSTRAINT UK_WORKSPACE_ENTRY_PT UNIQUE NONCLUSTERED (WORKSPACE_SCHEMA,	PROPERTY_TABLE_NAME)
);

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		‎April ‎09, ‎2013, ‏‎12:47:57 AM
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
