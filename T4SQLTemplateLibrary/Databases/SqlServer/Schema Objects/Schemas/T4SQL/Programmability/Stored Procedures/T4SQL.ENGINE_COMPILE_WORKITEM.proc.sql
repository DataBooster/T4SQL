CREATE PROCEDURE T4SQL.ENGINE_COMPILE_WORKITEM
(
	@inWorkitem_Table	NVARCHAR(128),
	@inWorkitem_Name	NVARCHAR(32),
	@inCompiled_Error	NVARCHAR(4000),
	@inObject_Code		NVARCHAR(MAX)
)
AS
	SET NOCOUNT ON
	DECLARE	@tSQL NVARCHAR(512), @ParmDefinition NVARCHAR(128);

	SET	@tSQL = N'UPDATE	' + @inWorkitem_Table + N'
	SET
		COMPILED_TIME	= GETDATE(),
		COMPILED_ERROR	= @Compiled_Error,
		OBJECT_CODE		= @Object_Code,
		START_BUILD		= 0
	WHERE
		WORKITEM_NAME	= @Workitem_Name
';

	SET	@ParmDefinition = N'@Compiled_Error NVARCHAR(4000), @Object_Code NVARCHAR(MAX), @Workitem_Name NVARCHAR(32)';

	EXECUTE sp_executesql @tSQL, @ParmDefinition, @Compiled_Error = @inCompiled_Error, @Object_Code = @inObject_Code, @Workitem_Name = @inWorkitem_Name;
	
----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		April 25, 2013, 10:59:31 PM
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
