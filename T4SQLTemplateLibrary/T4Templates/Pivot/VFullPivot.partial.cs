using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel;
using T4SQL;

namespace T4SQL.Pivot
{
	[Description("Similar to Pivot, but this transposing from rows into columns is a full-join-based operation instead of an aggregate operation")]
	public partial class VFullPivot : ITemplate, ITemplateProperties
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
			spec.AddProperty("JunctionColumns", "COL1, COL2, COL3", null, "{+}Join on columns");
			spec.AddProperty("PivotColumn", "PIV_COL", null, "{+}Column that contains the values that will become column headers");
			spec.AddProperty("MeasureColumn", "VALUE_COL", null, "{+}the value column of the PIVOT operator");
			spec.AddProperty("ValueAliasList", "'Val 1' AS VAL_1, 'Val 2' AS VAL_2, 'Val 3' AS VAL_3", null, "{+}List the values that will become the column names of the output table, each value in the list will be transposed into a separate column");

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
		public string Junction_Columns { get { return this.GetPropertyValue("JunctionColumns"); } }
		public string PivotColumn { get { return this.GetPropertyValue("PivotColumn"); } }
		public string MeasureColumn { get { return this.GetPropertyValue("MeasureColumn"); } }

		private Tuple<string, string>[] _Value_Aliases;
		public Tuple<string, string>[] Value_Aliases
		{
			get
			{
				if (_Value_Aliases == null)
					_Value_Aliases = this.GetPropertyValue("ValueAliasList").SplitColumns().Select(v => v.SegmentColumnAlias()).ToArray();

				return _Value_Aliases;
			}
		}

		public string Value_List { get { return string.Join(", ", Value_Aliases.Select(v => v.Item1)); } }

		private string[] _JunctionColumns;
		public string[] JunctionColumns
		{
			get
			{
				if (_JunctionColumns == null)
					_JunctionColumns = Junction_Columns.SplitColumns().ToArray();

				return _JunctionColumns;
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
//	Created Date:		November 26, 2013, 10:06:18 PM
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
