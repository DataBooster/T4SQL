CREATE PROCEDURE T4SQL.ENGINE_STANDBY_PING
(
	@outSwitch_To_Mode	NVARCHAR(8)	OUTPUT
)
AS
	SET NOCOUNT ON;
	DECLARE	@tInterval		TINYINT;
	DECLARE	@tNow			DATETIME;
	DECLARE	@tPrimary_Beat	DATETIME;

	SET	@tInterval	= T4SQL.ENGINE_GET_POLL_INTERVAL();
	SET	@tNow		= GETDATE();

	UPDATE	T4SQL.ENGINE_CONFIG
	SET		DATE_VALUE		= @tNow
	WHERE	DATE_VALUE		<= DATEADD(second, -@tInterval, @tNow)
		AND	ELEMENT_NAME	= N'STANDBY_BEAT';

	IF @@ROWCOUNT > 0
	BEGIN
		SELECT @tPrimary_Beat = DATE_VALUE FROM T4SQL.ENGINE_CONFIG WHERE ELEMENT_NAME = N'PRIMARY_BEAT';
		IF DATEDIFF(second, @tPrimary_Beat, @tNow)	> (@tInterval / 2)
			SET	@outSwitch_To_Mode	= N'Primary';
		ELSE
			SET	@outSwitch_To_Mode	= N'Standby';
	END;

	EXEC T4SQL.ENGINE_SERVICE_PING 0;

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		‎‎March ‎24, ‎2013, ‏‎8:01:15 PM
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
