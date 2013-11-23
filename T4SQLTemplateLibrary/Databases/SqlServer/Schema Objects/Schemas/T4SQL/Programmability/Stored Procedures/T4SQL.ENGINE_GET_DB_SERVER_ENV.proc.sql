CREATE PROCEDURE T4SQL.ENGINE_GET_DB_SERVER_ENV
(
	@outDatabase_Platform	NVARCHAR(32)	OUTPUT,
	@outDatabase_Product	NVARCHAR(256)	OUTPUT,
	@outProduct_Version		NVARCHAR(64)	OUTPUT,
	@outServer_Name			NVARCHAR(64)	OUTPUT
)
AS
	SET NOCOUNT ON;

	SELECT
		@outDatabase_Platform	= N'SQL Server',
		@outDatabase_Product	= @@version,
		@outProduct_Version		= CONVERT(NVARCHAR, SERVERPROPERTY('ProductVersion')),
		@outServer_Name			= CONVERT(NVARCHAR, SERVERPROPERTY('ServerName'))
;

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		May 14, 2013, 1:24:24 AM
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
