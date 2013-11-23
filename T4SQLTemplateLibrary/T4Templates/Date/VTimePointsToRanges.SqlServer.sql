﻿IF OBJECT_ID(N'<#= ObjectView #>', N'V') IS NULL
	EXECUTE ('CREATE VIEW <#= ObjectView #> AS SELECT NULL AS CREATE_OR_REPLACE');
GO

ALTER VIEW <#= ObjectView #> AS
-- This code was generated by <#= TemplateName #> @ <#= DateTime.Now.ToString() #>
<#
	string tEndDateCol = IsEndDateNext ? "t2." + DateColumn : string.Format("DATEADD(day, -1, t2.{0})", DateColumn);

	if (DbmsVersion > new Version(11, 0))	// SQL Server 2012
	{
#>
SELECT
	<#= SelectColumns.InsertLeft() #>
	<#= DateColumn #>		AS <#= RangeStartDateColumn #>,
	LEAD(<#= tEndDateCol #>, 1<#= DefaultEndDate.IsNullString() ? "" : ", " + DefaultEndDate #>) OVER (PARTITION BY <#= Key_Columns #> ORDER BY <#= DateColumn #>)
								AS <#= RangeEndDateColumn #>
FROM
	<#= SourceView #>	t2
<#
	}
	else									// SQL Server 2008, 2005
	{
		if (!DefaultEndDate.IsNullString())
			tEndDateCol = string.Format("ISNULL({0}, {1})", tEndDateCol, DefaultEndDate);
#>
WITH TR AS
(
	SELECT
		*,
		ROW_NUMBER() OVER (PARTITION BY <#= Key_Columns #> ORDER BY <#= DateColumn #>) AS ROW$NUMBER
	FROM
		<#= SourceView #>
)
SELECT
	<#= SelectColumns.Select(c => "t1." + c).InsertLeft() #>
	t1.<#= DateColumn #>		AS <#= RangeStartDateColumn #>,
	<#= tEndDateCol #>			AS <#= RangeEndDateColumn #>
FROM
	TR t1 LEFT OUTER JOIN
	TR t2 ON (<#= String.Join(" AND ", KeyColumns.Select(k => string.Format(@"t1.{0} = t2.{0}", k))) #>
		AND t1.ROW$NUMBER + 1 = t2.ROW$NUMBER)
<#
	}
#>
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
--	Created Date:		May 20, 2013, 12:00:44 AM
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
