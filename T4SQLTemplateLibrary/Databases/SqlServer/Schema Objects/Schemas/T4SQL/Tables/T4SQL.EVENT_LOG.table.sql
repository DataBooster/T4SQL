CREATE TABLE T4SQL.EVENT_LOG
(
	LOG_TIME	DATETIME		NOT NULL,
	LOG_TYPE	NVARCHAR(16),
	REFERENCE_	NVARCHAR(256),
	MESSAGE_	NVARCHAR(4000)
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
--	Created Date:		March ‎23, ‎2013, ‏‎11:54:02 PM
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
