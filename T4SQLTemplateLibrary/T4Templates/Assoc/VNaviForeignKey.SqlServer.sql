﻿IF OBJECT_ID(N'<#= ObjectView #>', N'V') IS NULL
	EXECUTE ('CREATE VIEW <#= ObjectView #> AS SELECT NULL AS CREATE_OR_REPLACE');
GO

ALTER VIEW <#= ObjectView #> AS
-- This code was generated by <#= TemplateName #> @ <#= DateTime.Now.ToString() #>
SELECT
<#= ColumnListClause #>
FROM
<#= JoinClause #>
;
GO
