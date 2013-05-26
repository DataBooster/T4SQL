CREATE PROCEDURE T4SQL.ENGINE_REGISTER_TEMPLATE_SPEC
(
	@inClass_Name			VARCHAR(128),
	@inProperty_Name		NVARCHAR(64),
	@inDefault_Value		NVARCHAR(4000),
	@inLink_State			NVARCHAR(256),
	@inProperty_Description	NVARCHAR(1024),
	@inProperty_Order		SMALLINT
)
AS
	SET NOCOUNT ON;

	MERGE INTO T4SQL.TEMPLATE_SPEC	T
	USING
	(
		SELECT
			@inClass_Name			AS CLASS_NAME,
			@inProperty_Name		AS PROPERTY_NAME,
			@inDefault_Value		AS DEFAULT_VALUE,
			@inLink_State			AS LINK_STATE,
			@inProperty_Description	AS PROPERTY_DESCRIPTION,
			@inProperty_Order		AS PROPERTY_ORDER
	)	R
	ON	(T.PROPERTY_NAME = R.PROPERTY_NAME AND T.CLASS_NAME = R.CLASS_NAME)
	WHEN MATCHED THEN
		UPDATE SET
			T.DEFAULT_VALUE			= R.DEFAULT_VALUE,
			T.LINK_STATE			= R.LINK_STATE,
			T.PROPERTY_DESCRIPTION	= R.PROPERTY_DESCRIPTION,
			T.PROPERTY_ORDER		= R.PROPERTY_ORDER
	WHEN NOT MATCHED THEN
		INSERT	(CLASS_NAME, PROPERTY_NAME, DEFAULT_VALUE, LINK_STATE, PROPERTY_DESCRIPTION, PROPERTY_ORDER)
		VALUES	(R.CLASS_NAME, R.PROPERTY_NAME, R.DEFAULT_VALUE, R.LINK_STATE, R.PROPERTY_DESCRIPTION, R.PROPERTY_ORDER);

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		May ‎01, ‎2013, ‏‎10:24:40 PM
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
