CREATE TABLE T4SQL.TEMPLATE_SPEC
(
	CLASS_NAME				VARCHAR(128)	NOT NULL,
	PROPERTY_NAME			NVARCHAR(64)	NOT NULL,
	DEFAULT_VALUE			NVARCHAR(4000),
	LINK_STATE				NVARCHAR(256),
	PROPERTY_DESCRIPTION	NVARCHAR(1024),
	PROPERTY_ORDER			SMALLINT,

	CONSTRAINT PK_TEMPLATE_SPEC PRIMARY KEY (CLASS_NAME, PROPERTY_NAME),
	CONSTRAINT FK_TEMPLATE_SPEC_CLASS FOREIGN KEY (CLASS_NAME)
		REFERENCES T4SQL.TEMPLATE_CLASS(FULL_NAME)
		ON UPDATE  CASCADE 
		ON DELETE  CASCADE
);
GO

CREATE INDEX IX_TEMPLATE_SPEC_ORDER ON T4SQL.TEMPLATE_SPEC (CLASS_NAME, PROPERTY_ORDER);

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		‎March ‎19, ‎2013, ‏‎8:53:42 PM
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
