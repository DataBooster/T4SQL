CREATE TRIGGER T4SQL.TRG_SEED_WORKITEM_INS
ON T4SQL.SEED_WORKITEM
AFTER INSERT, UPDATE 
AS 
BEGIN
    SET NOCOUNT ON;
	DECLARE	@tWorkitems AS T4SQL.TT_WORKITEMS;

	INSERT INTO @tWorkitems (WORKITEM_NAME, TEMPLATE_CLASS)
	SELECT I.WORKITEM_NAME, I.TEMPLATE_NAME FROM inserted I;

	EXEC T4SQL.META_COPY_PROPERTY_DEFAULT @@PROCID, @tWorkitems;
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
--	Created Date:		‎‎April ‎17, ‎2013, ‏‎11:28:39 PM
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
