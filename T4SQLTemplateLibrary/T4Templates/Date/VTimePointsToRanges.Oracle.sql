﻿CREATE OR REPLACE VIEW <#= ObjectView #> AS
SELECT
	-- This code was generated by <#= TemplateName #> @ <#= DateTime.Now.ToString() #>
<#
	string tEndDateCol = IsEndDateNext ? DateColumn : string.Format("({0} - 1)", DateColumn);
#>
	<#= SelectColumns.InsertLeft() #>
	<#= DateColumn #>		AS <#= RangeStartDateColumn #>,
	LEAD(<#= tEndDateCol #>, 1<#= DefaultEndDate.IsNullString() ? "" : ", " + DefaultEndDate #>) OVER (PARTITION BY <#= Key_Columns #> ORDER BY <#= DateColumn #>)
								AS <#= RangeEndDateColumn #>
FROM
	<#= SourceView #>

WITH READ ONLY;

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
