﻿IF OBJECT_ID(N'<#= ObjectView #>', N'V') IS NULL
	EXECUTE ('CREATE VIEW <#= ObjectView #> AS SELECT NULL AS CREATE_OR_REPLACE');
GO

ALTER VIEW <#= ObjectView #> AS
-- This code was generated by <#= TemplateName #> @ <#= DateTime.Now.ToString() #>
SELECT
	D.<#= DateColumn #><#= SelectColumns.Select(c => "S." + c).InsertRight() #>
FROM
	<#= SourceView #>	S,
	<#= DailyView #>	D
WHERE
	(
		S.<#= RangeEndDateColumn #> <#= IsEndDateNext ? ">" : ">=" #> D.<#= DateColumn #>
<# if (IsEndDateNullable) { #>
		OR S.<#= RangeEndDateColumn #> IS NULL
<# } #>
	)
	AND S.<#= RangeStartDateColumn #> <= D.<#= DateColumn #>
;
GO

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		May 15, 2013, 11:27:30 PM
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
