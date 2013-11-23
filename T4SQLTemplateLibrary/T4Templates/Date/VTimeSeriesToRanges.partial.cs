using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace T4SQL.Date
{
	[Description("Transform time series (daily) data into time ranges data (start_date - end_date).")]
	public partial class VTimeSeriesToRanges : ITemplate, ITemplateProperties
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
			spec.AddProperty("DateColumn", "DATE_", null, "{+}The date column of daily source table or view");
			spec.AddProperty("SelectColumns", "*", null, "[*] * or a comma-separated list of select columns - exclude the date column");
			spec.AddProperty("RangeStartDateColumn", "START_DATE", null, "{+}Time range Start Date column");
			spec.AddProperty("RangeEndDateColumn", "END_DATE", null, "{+}Time range End Date column");
			spec.AddProperty("EndDateNext", "0", null, "[*] 0: [START_DATE <= Time Range <= END_DATE]; 1: [START_DATE <= Time Range < END_DATE)");

			return spec;
		}
		#endregion

		#region Members to be called from the code blocks in the T4SQL Template file

		public string TemplateName { get { return this.GetTemplateName(); } }
		public string DbmsPlatform { get { return Context.DbServerEnv.DatabasePlatform; } }
		public Version DbmsVersion { get { return Context.DbServerEnv.ProductVersion; } }
		public string ObjectView { get { return this.GetPropertyValue("ObjectView"); } }
		public string SourceView { get { return this.GetPropertyValue("SourceView"); } }

		public string DateColumn { get { return this.GetPropertyValue("DateColumn"); } }
		public string RangeStartDateColumn { get { return this.GetPropertyValue("RangeStartDateColumn"); } }
		public string RangeEndDateColumn { get { return this.GetPropertyValue("RangeEndDateColumn"); } }
		public bool IsEndDateNext { get { return this.GetPropertyValue("EndDateNext").IsTrueString(); } }

		public IEnumerable<string> SelectColumns
		{
			get
			{
				return Context.DbServerEnv.ListTableColumns(SourceView, this.GetPropertyValue("SelectColumns"))
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
//	Created Date:		May 19, 2013, 11:59:24 PM
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
