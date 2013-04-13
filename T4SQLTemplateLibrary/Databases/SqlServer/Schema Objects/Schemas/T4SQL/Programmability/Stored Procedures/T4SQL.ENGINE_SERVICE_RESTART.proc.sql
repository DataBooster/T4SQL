CREATE PROCEDURE T4SQL.ENGINE_SERVICE_RESTART
AS
	SET NOCOUNT ON;

	UPDATE	T4SQL.TEMPLATE_CLASS
	SET		IS_ACTIVE = 0
	WHERE	IS_ACTIVE = 1;

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		‎‎March ‎24, ‎2013, ‏‎9:19:05 PM
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
