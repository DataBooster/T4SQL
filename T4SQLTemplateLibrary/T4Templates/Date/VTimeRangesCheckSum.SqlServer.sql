﻿IF OBJECT_ID(N'<#= ObjectView #>', N'V') IS NULL
	EXECUTE ('CREATE VIEW <#= ObjectView #> AS SELECT NULL AS CREATE_OR_REPLACE');
GO

ALTER VIEW <#= ObjectView #> AS
-- This code was generated by <#= TemplateName #> @ <#= DateTime.Now.ToString() #>
SELECT
	<#= KeyColumns #>,
	MIN(<#= RangeStartDateColumn #>)									AS <#= RangeStartDateColumn #>,
	MAX(ISNULL(<#= RangeEndDateColumn #>, <#= DefaultEndDate #>))	AS <#= RangeEndDateColumn #>,
	DATEDIFF(day, MIN(<#= RangeStartDateColumn #>), MAX(ISNULL(<#= RangeEndDateColumn #>, <#= DefaultEndDate #>)))<#= IsEndDateNext ? "" : " + 1" #>	AS <#= InscopeDaysColumn #>,
	SUM(DATEDIFF(day, <#= RangeStartDateColumn #>, ISNULL(<#= RangeEndDateColumn #>, <#= DefaultEndDate #>))<#= IsEndDateNext ? "" : " + 1" #>)		AS <#= CheckSumColumn #>
FROM
	<#= SourceView #>
GROUP BY
	<#= KeyColumns #>
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
--	Created Date:		‎‎June ‎02, ‎2013, ‏‎11:00:30 AM
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
