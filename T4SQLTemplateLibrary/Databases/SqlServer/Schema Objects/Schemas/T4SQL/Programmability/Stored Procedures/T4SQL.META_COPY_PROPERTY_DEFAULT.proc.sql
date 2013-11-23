CREATE PROCEDURE T4SQL.META_COPY_PROPERTY_DEFAULT
(
	@inProc_ID		INT,	-- Trigger Id
	@inTemplates	T4SQL.TT_WORKITEMS	READONLY
)
AS
    SET NOCOUNT ON;
	DECLARE	@tProperty_Table NVARCHAR(128), @tSql NVARCHAR(1024);

	SELECT
		@tProperty_Table = E.PROPERTY_TABLE_NAME
	FROM
		T4SQL.WORKSPACE_ENTRY	E,
		sys.triggers			T
	WHERE
		OBJECT_ID(E.WORKITEM_TABLE_NAME, N'U')	= T.parent_id
		AND	T.type				= 'TR'
		AND	T.parent_class		= 1
		AND	T.object_id			= @inProc_ID;

	SET @tSql = N'MERGE INTO ' + @tProperty_Table + N' P
	USING
	(
		SELECT
			T.WORKITEM_NAME,
			D.PROPERTY_NAME,
			D.DEFAULT_VALUE,
			D.LINK_STATE
		FROM
			T4SQL.TEMPLATE_SPEC	D,
			@tTemplates			T
		WHERE
			D.CLASS_NAME	= T.TEMPLATE_CLASS
	) S
	ON (P.PROPERTY_NAME = S.PROPERTY_NAME AND P.WORKITEM_NAME = S.WORKITEM_NAME)
	WHEN NOT MATCHED THEN
		INSERT (WORKITEM_NAME, PROPERTY_NAME, STRING_VALUE, LINK_STATE)
		VALUES (S.WORKITEM_NAME, S.PROPERTY_NAME, S.DEFAULT_VALUE, S.LINK_STATE)';

	EXECUTE sp_executesql @tSql, N'@tTemplates T4SQL.TT_WORKITEMS READONLY', @tTemplates = @inTemplates;

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		April 19, 2013, 12:36:40 AM
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
