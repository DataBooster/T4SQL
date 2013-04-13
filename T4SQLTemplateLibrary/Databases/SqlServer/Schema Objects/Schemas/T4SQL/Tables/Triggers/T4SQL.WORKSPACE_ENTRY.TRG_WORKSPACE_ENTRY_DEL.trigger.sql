CREATE TRIGGER T4SQL.TRG_WORKSPACE_ENTRY_DEL
ON T4SQL.WORKSPACE_ENTRY
AFTER DELETE
AS 
BEGIN
    SET NOCOUNT ON;
	IF EXISTS
	(
		SELECT	1
		FROM	deleted 
		WHERE	WORKSPACE_SCHEMA	= N'T4SQL'
			AND	WORKITEM_TABLE_NAME	= N'SEED_WORKITEM'
			AND PROPERTY_TABLE_NAME	= N'SEED_PROPERTY'
	)
	BEGIN
		RAISERROR(N'Built-in workspace T4SQL.SEED_... cannot be deleted!', 16, 3);
		ROLLBACK TRANSACTION;
	END;
END

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		‎‎April ‎10, ‎2013, ‏‎6:50:26 PM
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
