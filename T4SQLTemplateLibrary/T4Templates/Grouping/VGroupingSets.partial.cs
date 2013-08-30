using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace T4SQL.Grouping
{
	[Description("GROUPING SETS")]
	public partial class VGroupingSets : ITemplate, ITemplateProperties
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
			spec.AddProperty("SimpleGroupByColumns", "", null, "[*]Simple group by items, e.g. COL1, COL2, COL3");
			spec.AddProperty("GroupingSetsColumns", "(COL5, COL6, COL7) AS 'AGG_A', (COL4, COL5) AS 'AGG_B', () AS 'AGG_TOTAL'", null, "{+}Grouping Set List");
			spec.AddProperty("GroupingNameColumn", "AGG_TYPE", null, "[*]Return a new column for identifying the level of grouping");
			spec.AddProperty("AggregateExprs", "SUM(COL8) AS SUM_COL8, COUNT(DISTINCT COL9) AS CNT_COL9", null, "{+}Aggregate Function Expressions");

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
		public string SimpleGroupByColumns { get { return this.GetPropertyValue("SimpleGroupByColumns"); } }
		public string GroupingNameColumn { get { return this.GetPropertyValue("GroupingNameColumn"); } }
		public string AggregateExprs { get { return this.GetPropertyValue("AggregateExprs"); } }

		private GroupingColumnSet[] _GroupingSets;
		protected GroupingColumnSet[] GroupingSets
		{
			get
			{
				if (_GroupingSets == null)
				{
					_GroupingSets = this.GetPropertyValue("GroupingSetsColumns").SplitColumns().Select(s => s.SegmentColumnAlias())
						.Where(g => g.Item1.StartsWith("(", StringComparison.Ordinal) && g.Item1.EndsWith(")", StringComparison.Ordinal))
						.Select(t => new GroupingColumnSet(t.Item1, t.Item2)).ToArray();
				}

				return _GroupingSets;
			}
		}

		public string Grouping_Sets
		{
			get
			{
				return string.Join(", ", GroupingSets.Select(g => g.Column_List));
			}
		}

		private string[] _GroupingColumns;
		protected string[] GroupingColumns
		{
			get
			{
				if (_GroupingColumns == null)
				{
					IEnumerable<string> columns = Enumerable.Empty<string>();

					foreach (GroupingColumnSet colSet in GroupingSets)
						if (colSet.Columns.Length > 0)
							columns = columns.UnionColumns(colSet.Columns);

					_GroupingColumns = columns.ToArray();
				}

				return _GroupingColumns;
			}
		}

		public string Grouping_Columns
		{
			get
			{
				return string.Join(", ", GroupingColumns);
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
//	Created Date:		‎‎June ‎14, ‎2013, ‏‎12:24:44 AM
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
