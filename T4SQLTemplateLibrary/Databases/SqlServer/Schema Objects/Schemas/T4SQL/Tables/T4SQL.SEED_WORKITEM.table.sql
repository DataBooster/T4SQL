CREATE TABLE T4SQL.SEED_WORKITEM
(
	WORKITEM_NAME			NVARCHAR(32)	NOT NULL,
	TEMPLATE_NAME			VARCHAR(128)	NOT NULL,
	WORKITEM_DESCRIPTION	NVARCHAR(256)	NOT NULL,
	WORKITEM_USER			NVARCHAR(32),
	MODIFIED_TIME			DATETIME		NOT NULL	DEFAULT GETDATE(),
	COMPILED_TIME			DATETIME,
	COMPILED_ERROR			NVARCHAR(2000),
	OBJECT_CODE				NVARCHAR(MAX),
	BUILD_ORDER				INT,
	START_BUILD				BIT				NOT NULL	DEFAULT 0,

	CONSTRAINT PK_SEED_WORKITEM PRIMARY KEY (WORKITEM_NAME),
	CONSTRAINT FK_SEED_WORKITEM_TEMPLATE_CLASS FOREIGN KEY (TEMPLATE_NAME)
		REFERENCES T4SQL.TEMPLATE_CLASS(FULL_NAME)
		ON UPDATE CASCADE
		ON DELETE CASCADE
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
--	Created Date:		April 09, 2013, 1:19:59 AM
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
