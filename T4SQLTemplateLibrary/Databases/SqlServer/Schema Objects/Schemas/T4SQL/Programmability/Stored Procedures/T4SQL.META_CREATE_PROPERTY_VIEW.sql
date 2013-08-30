CREATE PROCEDURE T4SQL.META_CREATE_PROPERTY_VIEW
(
	@inWorkitem_Table			NVARCHAR(128),
	@inProperty_Table			NVARCHAR(128)
)
AS
	SET NOCOUNT ON;

	DECLARE @tSchema NVARCHAR(64), @tTab NVARCHAR(64), @tView NVARCHAR(128), @tDdlSql NVARCHAR(2048);

	SET @tSchema = PARSENAME(@inProperty_Table, 2);
	SET @tTab = PARSENAME(@inProperty_Table, 1);

	IF @tSchema IS NULL
		SET @tView = N'VW_' + @tTab
	ELSE
		SET @tView = @tSchema + N'.VW_' + @tTab;

	IF OBJECT_ID(@tView) IS NOT NULL
		RETURN;

	SET @tDdlSql = N'CREATE VIEW ' + @tView + N'
AS
SELECT
	P.WORKITEM_NAME,
	S.PROPERTY_ORDER,
	P.PROPERTY_NAME,
	P.STRING_VALUE,
	P.LINK_STATE,
	CAST(CASE
		WHEN ISNULL(P.STRING_VALUE, NCHAR(13)) = ISNULL(S.DEFAULT_VALUE, NCHAR(13)) AND
			ISNULL(P.LINK_STATE, NCHAR(13)) = ISNULL(S.LINK_STATE, NCHAR(13))
			THEN	0
		ELSE		1
	END	AS BIT)				AS CUSTOM,
	S.DEFAULT_VALUE,
	S.LINK_STATE			AS DEFAULT_LINK_STATE,
	S.PROPERTY_DESCRIPTION,
	S.CLASS_NAME,
	I.BUILD_ORDER
FROM
	' + @inProperty_Table + N'	P
	INNER JOIN
	' + @inWorkitem_Table + N'	I
	ON (P.WORKITEM_NAME = I.WORKITEM_NAME)
	INNER JOIN
	T4SQL.TEMPLATE_SPEC		S
	ON (P.PROPERTY_NAME = S.PROPERTY_NAME AND I.TEMPLATE_NAME = S.CLASS_NAME)
/*
ORDER BY
	I.BUILD_ORDER,
	P.WORKITEM_NAME,
	S.PROPERTY_ORDER,
	S.PROPERTY_NAME
*/
;';

	EXECUTE (@tDdlSql);

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		‎‎August ‎30, ‎2013, 3:39:13 PM
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
