CREATE TABLE T4SQL.ENGINE_SERVER
(
	SERVER_NAME			NVARCHAR2(32) 							NOT NULL,
	SERVICE_BEAT		TIMESTAMP(3)	DEFAULT SYSTIMESTAMP	NOT NULL,
	IS_PRIMARY			CHAR(1)			DEFAULT 'Y'				NOT NULL,
	SERVICE_ACCOUNT		NVARCHAR2(32),
	CONSTRAINT PK_ENGINE_SERVER PRIMARY KEY (SERVER_NAME)
)
ORGANIZATION INDEX
STORAGE (INITIAL 16K NEXT 8K);

----------------------------------------------------------------------------------------------------
--
--	Copyright 2012 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		‎October ‎12, ‎2013, ‏‎12:42:26 AM
--	Primary Host:		http://t4sql.codeplex.com
--	Change Log:
--	Author				Date			Comment
--
--
--
--
--	(Keep clean code rather than complicated code plus long comments.)
--
----------------------------------------------------------------------------------------------------
