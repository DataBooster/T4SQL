CREATE PROCEDURE T4SQL.ENGINE_GET_FOREIGN_KEY
(
	@inTable_Name		NVARCHAR(64),
	@outTable_Schema	NVARCHAR(64)	OUTPUT,
	@outTable_Name		NVARCHAR(64)	OUTPUT,
	@outQualified_Name	NVARCHAR(128)	OUTPUT
)
AS
	SET NOCOUNT ON;
	DECLARE @tTable_Id INT;

	SET @tTable_Id	= OBJECT_ID(@inTable_Name);

	SELECT
		@outTable_Schema	= S.name,
		@outTable_Name		= T.name,
		@outQualified_Name	= QUOTENAME(S.name) + N'.' + QUOTENAME(T.name)
	FROM
		sys.schemas		S,
		sys.objects		T
	WHERE
			S.schema_id	= T.schema_id
		AND T.object_id	= @tTable_Id;

	SELECT
		C.name						AS CONSTRAINT_NAME,
		B.name						AS FOREIGN_KEY_COLUMN,
		B.is_nullable				AS FOREIGN_KEY_NULLABLE,
		QUOTENAME(S.name) + N'.' + QUOTENAME(P.name)
									AS REFERENCED_TABLE,
		A.name						AS REFERENCED_COLUMN,
		A.is_nullable				AS REFERENCED_NULLABLE
	FROM
		sys.schemas					S,		-- Primary/Unique Table/View Schema
		sys.objects					P,		-- Primary/Unique Table/View
		sys.columns					A,		-- Primary/Unique Key Colums
		sys.columns					B,		-- Foreign Key Columns
		sys.objects					C,		-- Constraint Name
		sys.foreign_key_columns		F		-- Driving
	WHERE
			S.schema_id			= P.schema_id
		AND P.object_id			= F.referenced_object_id
		AND A.column_id			= F.referenced_column_id
		AND A.object_id			= F.referenced_object_id
		AND B.column_id			= F.parent_column_id
		AND B.object_id			= F.parent_object_id
		AND C.object_id			= F.constraint_object_id
		AND F.parent_object_id	= @tTable_Id
	ORDER BY
		F.constraint_object_id,
		F.parent_column_id;
		
----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		‎‎‎July ‎04, ‎2013, ‏‎6:44:27 PM
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
