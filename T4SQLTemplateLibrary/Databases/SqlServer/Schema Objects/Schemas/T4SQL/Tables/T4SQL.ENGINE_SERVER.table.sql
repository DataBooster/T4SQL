CREATE TABLE T4SQL.ENGINE_SERVER
(
	SERVER_NAME			NVARCHAR(32) 						NOT NULL,
	SERVICE_BEAT		DATETIME		DEFAULT GETDATE()	NOT NULL,
	IS_PRIMARY			BIT				DEFAULT 1			NOT NULL,
	SERVICE_ACCOUNT		NVARCHAR(32),
	CONSTRAINT PK_ENGINE_SERVER PRIMARY KEY (SERVER_NAME)
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
--	Created Date:		March 18, 2013, 11:19:53 PM
--	Primary Host:		http://t4sql.codeplex.com
--	Change Log:
--	Author				Date			Comment
--
--
--
--
--	(Keep code clean rather than complicated code plus long comments.)
--
----------------------------------------------------------------------------------------------------
