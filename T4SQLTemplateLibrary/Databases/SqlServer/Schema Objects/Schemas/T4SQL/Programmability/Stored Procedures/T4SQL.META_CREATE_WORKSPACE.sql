CREATE PROCEDURE T4SQL.META_CREATE_WORKSPACE
(
	@inWorkitem_Table			NVARCHAR(128),
	@inProperty_Table			NVARCHAR(128),
	@inWorkspace_Description	NVARCHAR(128),
	@inAutonomous_Owner			NVARCHAR(64)
)
AS
    SET NOCOUNT ON;

	SET @inWorkitem_Table = LTRIM(RTRIM(@inWorkitem_Table));
	SET @inProperty_Table = LTRIM(RTRIM(@inProperty_Table));

	IF OBJECT_ID(@inWorkitem_Table) IS NOT NULL OR OBJECT_ID(@inProperty_Table) IS NOT NULL
	BEGIN
		RAISERROR (N'%s, %s already exists in the database.', 16, 1, @inWorkitem_Table, @inProperty_Table);
		RETURN;
	END;

	DECLARE @tTab NVARCHAR(64), @tDdlSql NVARCHAR(1024);

	SET @tTab = PARSENAME(@inWorkitem_Table, 1);
	SET @tDdlSql = N'CREATE TABLE ' + @inWorkitem_Table + N'
(
	WORKITEM_NAME			NVARCHAR(32)	NOT NULL,
	TEMPLATE_NAME			VARCHAR(128)	NOT NULL,
	WORKITEM_DESCRIPTION	NVARCHAR(256)	NOT NULL,
	WORKITEM_USER			NVARCHAR(32),
	MODIFIED_TIME			DATETIME		NOT NULL	DEFAULT GETDATE(),
	COMPILED_TIME			DATETIME,
	COMPILED_ERROR			NVARCHAR(2000),
	OBJECT_CODE				NVARCHAR(MAX),
	BUILD_ORDER				INT,
	START_BUILD				BIT				NOT NULL	DEFAULT 0,

	CONSTRAINT PK_' + @tTab + N' PRIMARY KEY (WORKITEM_NAME),
	CONSTRAINT FK_' + @tTab + N'_TEMPLATE FOREIGN KEY (TEMPLATE_NAME)
		REFERENCES T4SQL.TEMPLATE_CLASS(FULL_NAME)
		ON UPDATE CASCADE
		ON DELETE CASCADE
);';

	EXECUTE (@tDdlSql);

	SET @tTab = PARSENAME(@inProperty_Table, 1);
	SET @tDdlSql = N'CREATE TABLE ' + @inProperty_Table + N'
(
	WORKITEM_NAME	NVARCHAR(32)	NOT NULL,
	PROPERTY_NAME	NVARCHAR(64)	NOT NULL,
	STRING_VALUE	NVARCHAR(4000)	NOT NULL,
	LINK_STATE		NVARCHAR(256),

	CONSTRAINT PK_' + @tTab + N' PRIMARY KEY (WORKITEM_NAME, PROPERTY_NAME),
	CONSTRAINT FK_' + @tTab + N'_WORKITEM FOREIGN KEY (WORKITEM_NAME)
		REFERENCES ' + @inWorkitem_Table + N'(WORKITEM_NAME)
		ON UPDATE  CASCADE 
		ON DELETE  CASCADE
);';

	EXECUTE (@tDdlSql);

	INSERT INTO T4SQL.WORKSPACE_ENTRY
	(
		WORKITEM_TABLE_NAME,
		PROPERTY_TABLE_NAME,
		WORKSPACE_DESCRIPTION,
		AUTONOMOUS_OWNER
	)
	VALUES
	(
		@inWorkitem_Table,
		@inProperty_Table,
		@inWorkspace_Description,
		@inAutonomous_Owner
	);

	EXEC T4SQL.META_CREATE_PROPERTY_VIEW @inWorkitem_Table, @inProperty_Table;

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		‎‎June ‎26, ‎2013, ‏‎11:30:13 PM
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
