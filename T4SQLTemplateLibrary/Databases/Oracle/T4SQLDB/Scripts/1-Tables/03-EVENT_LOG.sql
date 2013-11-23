CREATE TABLE T4SQL.EVENT_LOG
(
	LOG_TIME	TIMESTAMP(3) DEFAULT SYSTIMESTAMP	NOT NULL,
	LOG_TYPE	NVARCHAR2(16),
	REFERENCE_	NVARCHAR2(256),
	MESSAGE_	VARCHAR2(4000)
)
STORAGE (INITIAL 1M NEXT 1M);

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		October 12, 2013, 11:30:46 PM
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
