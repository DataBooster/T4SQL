CREATE TABLE T4SQL.TEMPLATE_CLASS
(
	FULL_NAME			VARCHAR2(128)	NOT NULL,
	MODULE				NVARCHAR2(128)	NOT NULL,
	ASSEMBLY_STRING		VARCHAR2(256)	NOT NULL,
	CREATED_TIME		DATE			NOT NULL,
	START_TIME			DATE			NOT NULL,
	IS_ACTIVE			CHAR(1)			NOT NULL,
	CLASS_DESCRIPTION	NVARCHAR2(1024),

	CONSTRAINT PK_TEMPLATE_CLASS PRIMARY KEY (FULL_NAME)
)
ORGANIZATION INDEX
STORAGE (INITIAL 128K NEXT 128K);

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		‎October ‎13, ‎2013, ‏‎12:05:04 AM
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
