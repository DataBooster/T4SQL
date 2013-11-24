﻿IF OBJECT_ID(N'<#= ObjectView #>', N'V') IS NULL
	EXECUTE ('CREATE VIEW <#= ObjectView #> AS SELECT NULL AS CREATE_OR_REPLACE');
GO

ALTER VIEW <#= ObjectView #> AS
-- This code was generated by <#= TemplateName #> @ <#= DateTime.Now.ToString() #>
SELECT
	<#= SimpleGroupByColumns.InsertLeft() #>
	CASE GROUPING_ID(<#= Grouping_Columns #>)
<#	for (int i = 0; i < GroupingSets.Length; i++) { #>
		WHEN <#= GroupingSets[i].GetGroupingId(GroupingColumns) #>	THEN <#= GroupingSets[i].ToString() #>
<#	} #>
	END		AS <#= GroupingNameColumn #>,
	<#= Grouping_Columns #>,
	<#= AggregateExprs #>
FROM
	<#= SourceView #>
<#= SourceFilter.InsertRight("WHERE\r\n	") #>
GROUP BY
	<#= SimpleGroupByColumns.InsertLeft() #>
	GROUPING SETS
	(
		<#= Grouping_Sets #>
	)
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
--	Created Date:		June 14, 2013, 12:24:41 AM
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
