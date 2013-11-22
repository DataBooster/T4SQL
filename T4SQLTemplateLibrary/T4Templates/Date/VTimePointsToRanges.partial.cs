using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace T4SQL.Date
{
	[Description("Transform time points (turning points) data into time ranges data (start_date - end_date).")]
	public partial class VTimePointsToRanges : ITemplate, ITemplateProperties
	{
		#region Implement ITemplate Properties
		public TemplateContext Context { get; set; }
		#endregion

		#region Implement ITemplateProperties Methods
		public TemplateSpec GetPropertiesSpec()
		{
			TemplateSpec spec = new TemplateSpec();

			spec.AddProperty("ObjectView", "schema.VW_ObjViewName", null, "{+}The full name of object view");
			spec.AddProperty("SourceView", "schema.SourceTableOrView", null, "{+}Source Table Or View");
			spec.AddProperty("KeyColumns", "COL1, COL2", null, "{+}The key column or a comma-separated list of key columns - exclude the date column of time point");
			spec.AddProperty("AttribColumns", "*", null, "[*] * or a comma-separated list of attribute columns");
			spec.AddProperty("DateColumn", "DATE_", null, "{+}Source date column of time point");
			spec.AddProperty("RangeStartDateColumn", "START_DATE", null, "{+}Time range Start Date column");
			spec.AddProperty("RangeEndDateColumn", "END_DATE", null, "{+}Time range End Date column");
			spec.AddProperty("EndDateNext", "0", null, "[*] 0: [START_DATE <= Time Range <= END_DATE]; 1: [START_DATE <= Time Range < END_DATE)");
			spec.AddProperty("DefaultEndDate", "2999-12-31", null, "[*]Ultimate END_DATE as the substitute of IS NULL");

			return spec;
		}
		#endregion

		#region Members to be called from the code blocks in the T4SQL Template file

		public string TemplateName { get { return this.GetTemplateName(); } }
		public string DbmsPlatform { get { return Context.DbServerEnv.DatabasePlatform; } }
		public Version DbmsVersion { get { return Context.DbServerEnv.ProductVersion; } }
		public string ObjectView { get { return this.GetPropertyValue("ObjectView"); } }
		public string SourceView { get { return this.GetPropertyValue("SourceView"); } }

		public string Key_Columns { get { return this.GetPropertyValue("KeyColumns"); } }
		public string DateColumn { get { return this.GetPropertyValue("DateColumn"); } }
		public string RangeStartDateColumn { get { return this.GetPropertyValue("RangeStartDateColumn"); } }
		public string RangeEndDateColumn { get { return this.GetPropertyValue("RangeEndDateColumn"); } }
		public bool IsEndDateNext { get { return this.GetPropertyValue("EndDateNext").IsTrueString(); } }
		public IEnumerable<string> KeyColumns { get { return Key_Columns.SplitColumns(); } }

		public string DefaultEndDate
		{
			get
			{
				string defaultEndDate = this.GetPropertyValue("DefaultEndDate");

				if (string.IsNullOrWhiteSpace(defaultEndDate))
					return string.Empty;
				else
				{
					Regex rgDate = new Regex(@"\s*'?(?<dt>\d{4}-\d{2}-\d{2})'?\s*");
					Match mc = rgDate.Match(defaultEndDate);

					if (mc.Success)
						return string.Format(((DbmsPlatform == "Oracle") ? "TO_DATE('{0}', 'YYYY-MM-DD')" : "CAST('{0}' AS DATE)"), mc.Groups["dt"].Value);
					else
						return defaultEndDate;
				}
			}
		}

		public IEnumerable<string> SelectColumns
		{
			get
			{
				return KeyColumns.UnionColumns(Context.DbServerEnv.ListTableColumns(SourceView, this.GetPropertyValue("AttribColumns")))
					.ExceptColumns(new string[] { DateColumn, RangeStartDateColumn, RangeEndDateColumn });
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
//	Created Date:		‎‎May ‎20, ‎2013, ‏‎12:00:44 AM
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
