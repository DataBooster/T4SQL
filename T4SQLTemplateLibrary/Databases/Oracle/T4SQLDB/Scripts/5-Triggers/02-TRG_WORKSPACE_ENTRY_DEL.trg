CREATE OR REPLACE TRIGGER T4SQL.TRG_WORKSPACE_ENTRY_DEL
BEFORE DELETE ON T4SQL.WORKSPACE_ENTRY
FOR EACH ROW
BEGIN
	IF :old.WORKITEM_TABLE_NAME = 'T4SQL.SEED_WORKITEM' AND :old.PROPERTY_TABLE_NAME = 'T4SQL.SEED_PROPERTY' THEN
		RAISE_APPLICATION_ERROR(-20101, 'Built-in workspace T4SQL.SEED_... cannot be deleted!');
	END IF;
END;

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		‎November ‎02, 2013, ‏‎11:45:03 PM
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
/
