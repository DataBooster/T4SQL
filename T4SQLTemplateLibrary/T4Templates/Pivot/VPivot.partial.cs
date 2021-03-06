﻿using System;
using System.Linq;
using System.ComponentModel;

namespace T4SQL.Pivot
{
	[Description("Pivot - transposing rows into columns")]
	public partial class VPivot : ITemplate, ITemplateProperties
	{
		#region Implement ITemplate Properties
		public TemplateContext Context { get; set; }
		#endregion

		#region Implement ITemplateProperties Methods
		public TemplateSpec GetPropertiesSpec()
		{
			TemplateSpec spec = new TemplateSpec();

			spec.AddProperty("ObjectView", "schema.VW_ObjViewName", null, "{+}The full name of object view");
			spec.AddProperty("SourceView", "schema.SomeTableOrView", null, "{+}Source Table Or View");
			spec.AddProperty("SourceFilter", "", null, "[*]Search conditions");
			spec.AddProperty("NonPivotedColumns", "COL1, COL2, COL3", null, "{+}Non-pivoted columns");
			spec.AddProperty("AggregateFunction", "MAX({0})", null, "[*]Aggregation function (pivot is an aggregate operation)");
			spec.AddProperty("ValueColumn", "AGG_VAL_COL", null, "{+}Column being aggregated - the value column of the PIVOT operator");
			spec.AddProperty("PivotColumn", "PIV_COL", null, "{+}Column that contains the values that will become column headers - the pivot column of the PIVOT operator (pivot_for_clause)");
			spec.AddProperty("ValueList", "[Val 1] AS VAL_COL1, [Val 2], Val3", null, "{+}List the values in the PivotColumn that will become the column names of the output table, the aggregations for each value in the pivot_in_clause will be transposed into a separate column");

			return spec;
		}
		#endregion

		#region Members to be called from the code blocks in the T4SQL Template file

		public string TemplateName { get { return this.GetTemplateName(); } }
		public string DbmsPlatform { get { return Context.DbServerEnv.DatabasePlatform; } }
		public Version DbmsVersion { get { return Context.DbServerEnv.ProductVersion; } }
		public string ObjectView { get { return this.GetPropertyValue("ObjectView"); } }
		public string SourceView { get { return this.GetPropertyValue("SourceView"); } }
		public string SourceFilter { get { return this.GetPropertyValue("SourceFilter"); } }
		public string ValueColumn { get { return this.GetPropertyValue("ValueColumn"); } }
		public string PivotColumn { get { return this.GetPropertyValue("PivotColumn"); } }
		public string Value_List { get { return this.GetPropertyValue("ValueList"); } }

		public string ValueList
		{
			get
			{
				return string.Join(", ", Value_List.SplitColumns().Select(v => v.SegmentColumnAlias().Item1));
			}
		}

		public string SelectColumns
		{
			get
			{
				return (DbmsPlatform == "Oracle") ? "*" :
					Context.DbServerEnv.ListTableColumns(SourceView, this.GetPropertyValue("NonPivotedColumns"))
					.ExceptColumns(new string[] { ValueColumn, PivotColumn }).InsertLeft() + Value_List;
			}
		}

		public string SourceColumns
		{
			get
			{
				return string.Join(", ", Context.DbServerEnv.ListTableColumns(SourceView, this.GetPropertyValue("NonPivotedColumns"))
					.UnionColumns(new string[] { ValueColumn, PivotColumn }));
			}
		}

		public string AggregateFunction
		{
			get
			{
				return string.Format(this.GetPropertyValue("AggregateFunction").NormalizeAggFunFmt(), ValueColumn);
			}
		}

		#endregion
	}
}

////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Copyright 2013 Abel Cheng
//	This source code is subject to terms and conditions of the Apache License, Version 2.0.
//	See http://www.apache.org/licenses/LICENSE-2.0.
//	All other rights reserved.
//	You must not remove this notice, or any other, from this software.
//
//	Original Author:	Abel Cheng <abelcys@gmail.com>
//	Created Date:		June 02, 2013, 11:06:10 AM
//	Primary Host:		http://t4sql.codeplex.com
//	Change Log:
//	Author				Date			Comment
//
//
//
//
//	(Keep code clean rather than complicated code plus long comments.)
//
////////////////////////////////////////////////////////////////////////////////////////////////////
