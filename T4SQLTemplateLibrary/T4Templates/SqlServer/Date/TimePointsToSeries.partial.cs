using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel;
using T4SQL;

namespace T4SQL.SqlServer.Date
{
	[Description("Transform time points (turning points) data into time series (daily) data.")]
	public partial class TimePointsToSeries : ITemplate, ITemplateProperties
	{
		#region Implement ITemplate Properties
		public TemplateContext Context { get; set; }
		#endregion

		#region Implement ITemplateProperties Methods
		public TemplateSpec GetPropertiesSpec()
		{
			TemplateSpec spec = new TemplateSpec();

			spec.AddProperty("ObjectView", "dbo.VW_ViewName_ToDo", null, "The full name of object view");
			spec.AddProperty("SourceView", "[SomeTableOrView]", null, "Source Table Or View");
			spec.AddProperty("KeyColumns", "COL1, COL2", null, "The key column or a comma-separated list of key columns - exclude the date column of time point");
			spec.AddProperty("SourceDateColumn", "DATE_", null, "Source date column of time point");
			spec.AddProperty("AttribColumns", "*", null, "* or a comma-separated list of attribute columns");
			spec.AddProperty("DailyView", "dbo.VW_ORDINAL_DATE", null, "Time Series base daily source table or view");
			spec.AddProperty("DailyDateColumn", "DATE_", null, "The date column of daily source table or view");

			return spec;
		}
		#endregion

		#region Members to be called from the code blocks in the tt file

		public string TemplateName { get { return this.GetTemplateName(); } }
		public Version DbmsVersion { get { return Context.DbServerEnv.ProductVersion; } }
		public string ObjectView { get { return this.GetPropertyValue("ObjectView"); } }
		public string SourceView { get { return this.GetPropertyValue("SourceView"); } }

		public string Key_Columns { get { return this.GetPropertyValue("KeyColumns"); } }
		public string SourceDateColumn { get { return this.GetPropertyValue("SourceDateColumn"); } }
		public string DailyView { get { return this.GetPropertyValue("DailyView"); } }
		public string DailyDateColumn { get { return this.GetPropertyValue("DailyDateColumn"); } }
		public IEnumerable<string> KeyColumns { get { return Key_Columns.SplitToCollection(); } }

		public IEnumerable<string> SelectColumns
		{
			get
			{
				return KeyColumns.Union(Context.DbServerEnv.ListTableColumns(SourceView, this.GetPropertyValue("AttribColumns")),
					StringComparer.OrdinalIgnoreCase).Except(new string[] { SourceDateColumn }, StringComparer.OrdinalIgnoreCase);
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
//	Created Date:		‎May ‎15, ‎2013, ‏‎11:30:25 PM
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
