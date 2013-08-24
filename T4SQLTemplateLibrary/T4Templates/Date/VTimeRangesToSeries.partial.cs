using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace T4SQL.Date
{
	[Description("Transform time ranges data (start_date - end_date) into time series data (daily).")]
	public partial class VTimeRangesToSeries : ITemplate, ITemplateProperties
	{
		#region Implement ITemplate Properties
		public TemplateContext Context { get; set; }
		#endregion

		#region Implement ITemplateProperties Methods
		public TemplateSpec GetPropertiesSpec()
		{
			TemplateSpec spec = new TemplateSpec();

			spec.AddProperty("ObjectView", "dbo.VW_ViewName_ToDo", null, "{+}The full name of object view");
			spec.AddProperty("SourceView", "schema.SomeTableOrView", null, "{+}Source Table Or View");
			spec.AddProperty("SelectColumns", "*", null, "[*] * or a comma-separated list of select columns - exclude the date column");
			spec.AddProperty("RangeStartDateColumn", "START_DATE", null, "{+}Time range Start Date column");
			spec.AddProperty("RangeEndDateColumn", "END_DATE", null, "{+}Time range End Date column");
			spec.AddProperty("EndDateNext", "0", null, "[*] 0: [START_DATE <= Time Range <= END_DATE]; 1: [START_DATE <= Time Range < END_DATE)");
			spec.AddProperty("EndDateNull", "NULL", null, "[*]EndDate IS NULL means CURRENT");
			spec.AddProperty("DailyView", "dbo.VW_ORDINAL_DATE", null, "[*]Time Series base daily source table or view");
			spec.AddProperty("DateColumn", "DATE_", null, "[*]The date column of daily source table or view");

			return spec;
		}
		#endregion

		#region Members to be called from the code blocks in the T4SQL Template file

		public string TemplateName { get { return this.GetTemplateName(); } }
		public string DbmsPlatform { get { return Context.DbServerEnv.DatabasePlatform; } }
		public Version DbmsVersion { get { return Context.DbServerEnv.ProductVersion; } }
		public string ObjectView { get { return this.GetPropertyValue("ObjectView"); } }
		public string SourceView { get { return this.GetPropertyValue("SourceView"); } }

		public string RangeStartDateColumn { get { return this.GetPropertyValue("RangeStartDateColumn"); } }
		public string RangeEndDateColumn { get { return this.GetPropertyValue("RangeEndDateColumn"); } }
		public string DailyView { get { return this.GetPropertyValue("DailyView"); } }
		public string DateColumn { get { return this.GetPropertyValue("DateColumn"); } }
		public bool IsEndDateNext { get { return this.GetPropertyValue("EndDateNext").IsTrueString(); } }

		public bool IsEndDateNullable
		{
			get
			{
				string strEndDateNull = this.GetPropertyValue("EndDateNull");

				if (strEndDateNull.IsNullString())
					return true;
				else
					return strEndDateNull.IsTrueString();
			}
		}

		public IEnumerable<string> SelectColumns
		{
			get
			{
				return Context.DbServerEnv.ListTableColumns(SourceView, this.GetPropertyValue("SelectColumns"))
					.ExceptColumns(new string[] { RangeStartDateColumn, RangeEndDateColumn, DateColumn });
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
//	Created Date:		‎May ‎15, ‎2013, ‏‎11:30:39 PM
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
