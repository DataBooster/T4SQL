CREATE PROCEDURE T4SQL.META_CREATE_WORKITEM_TRIGGER
(
	@inWorkitem_Table	NVARCHAR(128)
)
AS
    SET NOCOUNT ON;
	DECLARE @tDdlSql NVARCHAR(1024);

	IF NOT EXISTS
	(
		SELECT
			1
		FROM
			sys.triggers	T
		WHERE
				T.parent_id		= OBJECT_ID(@inWorkitem_Table, N'U')
			AND	T.type			= 'TR'
			AND	T.parent_class	= 1
	)
	BEGIN
		SET @tDdlSql = N'CREATE TRIGGER TRG_' + PARSENAME(@inWorkitem_Table, 1) + N'_INS
ON ' + @inWorkitem_Table + N'
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
	DECLARE	@tWorkitems AS T4SQL.TT_WORKITEMS;

	INSERT INTO @tWorkitems (WORKITEM_NAME, TEMPLATE_CLASS)
	SELECT I.WORKITEM_NAME, I.TEMPLATE_NAME FROM inserted I;

	EXEC T4SQL.META_COPY_PROPERTY_DEFAULT @@PROCID, @tWorkitems;
END;';

		EXECUTE (@tDdlSql);
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
--	Created Date:		‎April ‎20, ‎2013, ‏‎11:44:31 PM
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
