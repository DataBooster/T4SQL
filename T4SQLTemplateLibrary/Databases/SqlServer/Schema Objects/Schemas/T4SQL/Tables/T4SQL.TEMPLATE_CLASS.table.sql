CREATE TABLE T4SQL.TEMPLATE_CLASS
(
	FULL_NAME			VARCHAR(128)	NOT NULL, 
	MODULE				NVARCHAR(128)	NOT NULL,
	ASSEMBLY_STRING		VARCHAR(256)	NOT NULL,
	CREATED_TIME		DATETIME		NOT NULL,
	START_TIME			DATETIME		NOT NULL,
	IS_ACTIVE			BIT				NOT NULL,
	CLASS_DESCRIPTION	NVARCHAR(512),

	CONSTRAINT PK_TEMPLATE_CLASS PRIMARY KEY (FULL_NAME)
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
--	Created Date:		‎March ‎19, ‎2013, ‏‎8:53:27 PM
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
