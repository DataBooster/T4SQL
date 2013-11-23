CREATE PROCEDURE T4SQL.ENGINE_PRIMARY_PING
(
	@outSwitch_To_Mode	NVARCHAR(8)	OUTPUT
)
AS
	SET NOCOUNT ON
	DECLARE	@tReturn	INT;
	DECLARE	@tNow		DATETIME;
	
	SET	@tNow	= GETDATE();

	UPDATE	T4SQL.ENGINE_CONFIG
	SET		DATE_VALUE		= @tNow
	WHERE	ELEMENT_NAME	= N'PRIMARY_BEAT';

	SET	@outSwitch_To_Mode	= N'Primary';

	EXEC T4SQL.ENGINE_SERVICE_PING 1;

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		March 27, 2013, 10:37:33 PM
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
