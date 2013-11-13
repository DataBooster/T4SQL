CREATE OR REPLACE PACKAGE T4SQL.PUB_REF IS

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		‎November ‎12, 2013, 9:39:35 PM
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

PROCEDURE COPY_PROPERTY_DEFAULT
(
	inCall_Stack		VARCHAR2,	-- DBMS_UTILITY.FORMAT_CALL_STACK
	inWorkitem_Name		VARCHAR2,
	inTemplate_Name		VARCHAR2
);

END PUB_REF;
/
CREATE OR REPLACE PACKAGE BODY T4SQL.PUB_REF IS


PROCEDURE COPY_PROPERTY_DEFAULT
(
	inCall_Stack		VARCHAR2,	-- DBMS_UTILITY.FORMAT_CALL_STACK
	inWorkitem_Name		VARCHAR2,
	inTemplate_Name		VARCHAR2
)	AS
BEGIN
	T4SQL.META.COPY_PROPERTY_DEFAULT(inCall_Stack, inWorkitem_Name, inTemplate_Name);
END COPY_PROPERTY_DEFAULT;


END PUB_REF;
/
