CREATE PROCEDURE T4SQL.ENGINE_GET_CONFIG
(
	@outPoll_Interval	TINYINT	OUTPUT
)
AS
	SET NOCOUNT ON;

	SET @outPoll_Interval = T4SQL.ENGINE_GET_POLL_INTERVAL();

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		‎‎March ‎25, ‎2013, ‏‎10:44:24 PM
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
