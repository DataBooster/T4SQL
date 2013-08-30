using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace T4SQL.Pivot
{
	[Description("Unpivot")]
	public partial class VUnpivot : ITemplate, ITemplateProperties
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
			spec.AddProperty("NonPivotedColumns", "COL1, COL2, COL3", null, "{+}Non-pivoted columns");
			spec.AddProperty("ValueColumn", "MEASURE_COL", null, "{+}Specify a name for each output column that will hold measure values");
			spec.AddProperty("PivotColumn", "TYPE_COL", null, "{+}Specify a name for each output column that will hold descriptor values");
			spec.AddProperty("UnpivotColumns", "*", null, "[*]Specify the input data columns whose names will become values in the output columns");

			return spec;
		}
		#endregion

		#region Members to be called from the code blocks in the T4SQL Template file

		public string TemplateName { get { return this.GetTemplateName(); } }
		public string DbmsPlatform { get { return Context.DbServerEnv.DatabasePlatform; } }
		public Version DbmsVersion { get { return Context.DbServerEnv.ProductVersion; } }
		public string ObjectView { get { return this.GetPropertyValue("ObjectView"); } }
		public string SourceView { get { return this.GetPropertyValue("SourceView"); } }
		public string ValueColumn { get { return this.GetPropertyValue("ValueColumn"); } }
		public string PivotColumn { get { return this.GetPropertyValue("PivotColumn"); } }

		private IEnumerable<string> NonPivotedColumns
		{
			get
			{
				return this.GetPropertyValue("NonPivotedColumns").SplitColumns();
			}
		}

		public string UnpivotInColumns
		{
			get
			{
				return string.Join(", ",
					Context.DbServerEnv.ListTableColumns(SourceView, this.GetPropertyValue("UnpivotColumns"))
					.ExceptColumns(NonPivotedColumns));
			}
		}

		public string SelectColumns
		{
			get
			{
				return string.Join(", ", NonPivotedColumns.UnionColumns(new string[] { PivotColumn, ValueColumn }));
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
//	Created Date:		‎‎June ‎02, ‎2013, ‏‎11:06:38 AM
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
