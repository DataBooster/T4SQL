CREATE TABLE T4SQL.SEED_PROPERTY
(
	WORKITEM_NAME	NVARCHAR2(32)	NOT NULL,
	PROPERTY_NAME	NVARCHAR2(64)	NOT NULL,
	STRING_VALUE	VARCHAR2(4000),
	LINK_STATE		NVARCHAR2(256),

	CONSTRAINT PK_SEED_PROPERTY PRIMARY KEY (WORKITEM_NAME, PROPERTY_NAME),
	CONSTRAINT FK_SEED_PROPERTY_WORKITEM FOREIGN KEY (WORKITEM_NAME)
		REFERENCES T4SQL.SEED_WORKITEM(WORKITEM_NAME)
		ON DELETE  CASCADE
)
STORAGE (INITIAL 16K NEXT 64K);

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		October 14, 2013, 10:18:56 PM
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
