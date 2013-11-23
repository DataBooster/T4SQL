CREATE PROCEDURE T4SQL.ENGINE_LOG_SYS_ERROR
(
	@inReference	NVARCHAR(256),
	@inMessage		NVARCHAR(4000)
)
AS
	SET NOCOUNT ON;

	INSERT INTO T4SQL.EVENT_LOG (LOG_TIME, LOG_TYPE, REFERENCE_, MESSAGE_)
	VALUES (GETDATE(), N'Error', @inReference, @inMessage);

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		March 24, 2013, 12:02:04 AM
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
