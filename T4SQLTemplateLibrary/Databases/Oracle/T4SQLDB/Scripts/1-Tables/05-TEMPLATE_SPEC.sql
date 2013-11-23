CREATE TABLE T4SQL.TEMPLATE_SPEC
(
	CLASS_NAME				VARCHAR2(128)	NOT NULL,
	PROPERTY_NAME			NVARCHAR2(64)	NOT NULL,
	DEFAULT_VALUE			VARCHAR2(4000),
	LINK_STATE				NVARCHAR2(256),
	PROPERTY_DESCRIPTION	NVARCHAR2(1024),
	PROPERTY_ORDER			NUMBER(5),

	CONSTRAINT PK_TEMPLATE_SPEC PRIMARY KEY (CLASS_NAME, PROPERTY_NAME),
	CONSTRAINT FK_TEMPLATE_SPEC_CLASS FOREIGN KEY (CLASS_NAME)
		REFERENCES	T4SQL.TEMPLATE_CLASS(FULL_NAME)
		ON DELETE	CASCADE
)
STORAGE (INITIAL 512K NEXT 1M);

CREATE INDEX T4SQL.IX_TEMPLATE_SPEC_ORDER ON T4SQL.TEMPLATE_SPEC (CLASS_NAME, PROPERTY_ORDER);

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		October 13, 2013, 10:38:05 PM
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
