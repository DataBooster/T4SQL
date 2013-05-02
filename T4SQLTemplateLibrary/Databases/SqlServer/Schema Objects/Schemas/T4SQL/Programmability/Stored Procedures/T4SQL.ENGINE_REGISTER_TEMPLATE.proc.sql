CREATE PROCEDURE T4SQL.ENGINE_REGISTER_TEMPLATE
(
	@inFull_Name			VARCHAR(128),
	@inModule				NVARCHAR(128),
	@inAssembly_String		VARCHAR(256),
	@inClass_Description	NVARCHAR(1024)
)
AS
	SET NOCOUNT ON;

	MERGE INTO T4SQL.TEMPLATE_CLASS		T
	USING
	(
		SELECT
			@inFull_Name 			AS FULL_NAME,
			@inModule				AS MODULE,
			@inAssembly_String		AS ASSEMBLY_STRING,
			@inClass_Description	AS CLASS_DESCRIPTION,
			GETDATE()				AS REGISTER_TIME
	)	R
	ON	(T.FULL_NAME = R.FULL_NAME)
	WHEN MATCHED THEN
		UPDATE SET
			T.MODULE			= R.MODULE,
			T.ASSEMBLY_STRING	= R.ASSEMBLY_STRING,
			T.START_TIME		= R.REGISTER_TIME,
			T.IS_ACTIVE			= 1,
			T.CLASS_DESCRIPTION	= R.CLASS_DESCRIPTION
	WHEN NOT MATCHED THEN
		INSERT	(FULL_NAME, MODULE, ASSEMBLY_STRING, CREATED_TIME, START_TIME, IS_ACTIVE, CLASS_DESCRIPTION)
		VALUES	(R.FULL_NAME, R.MODULE, R.ASSEMBLY_STRING, R.REGISTER_TIME, R.REGISTER_TIME, 1, R.CLASS_DESCRIPTION);

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		‎March ‎24, ‎2013, ‏‎11:49:40 PM
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
