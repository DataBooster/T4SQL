CREATE TABLE T4SQL.SEED_PROPERTY
(
	WORKITEM_NAME	NVARCHAR(32)	NOT NULL,
	PROPERTY_NAME	NVARCHAR(64)	NOT NULL,
	STRING_VALUE	NVARCHAR(4000),
	LINK_STATE		NVARCHAR(256),

	CONSTRAINT PK_SEED_PROPERTY PRIMARY KEY (WORKITEM_NAME, PROPERTY_NAME),
	CONSTRAINT FK_SEED_PROPERTY_WORKITEM FOREIGN KEY (WORKITEM_NAME)
		REFERENCES T4SQL.SEED_WORKITEM(WORKITEM_NAME)
		ON UPDATE  CASCADE 
		ON DELETE  CASCADE
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
--	Created Date:		‎‎April ‎09, ‎2013, ‏‎1:20:54 AM
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
