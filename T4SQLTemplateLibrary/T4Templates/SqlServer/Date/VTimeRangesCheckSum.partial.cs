using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel;
using T4SQL;

namespace T4SQL.SqlServer.Date
{
	[Description("Check Time Ranges Continuity - no overlap, no interruption")]
	public partial class VTimeRangesCheckSum : ITemplate, ITemplateProperties
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
			spec.AddProperty("KeyColumns", "COL1, COL2", null, "The key column or a comma-separated list of key columns - exclude date-range columns");
			spec.AddProperty("RangeStartDateColumn", "START_DATE", null, "Time range Start Date column");
			spec.AddProperty("RangeEndDateColumn", "END_DATE", null, "Time range End Date column");
			spec.AddProperty("EndDateNext", "0", null, "0: [START_DATE <= Time Range <= END_DATE]; 1: [START_DATE <= Time Range < END_DATE)");
			spec.AddProperty("DefaultEndDate", "CONVERT(date, GETDATE())", null, "Ultimate END_DATE as the substitute of IS NULL");
			spec.AddProperty("InscopeDaysColumn", "INSCOPE_DAYS", null, "The total number of days between the first START_DATE and the last END_DATE");
			spec.AddProperty("CheckSumColumn", "CHECK_SUM", null, "SUM total days of every Time Ranges");

			return spec;
		}
		#endregion

		#region Members to be called from the code blocks in the tt file

		public string TemplateName { get { return this.GetTemplateName(); } }
		public Version DbmsVersion { get { return Context.DbServerEnv.ProductVersion; } }
		public string ObjectView { get { return this.GetPropertyValue("ObjectView"); } }
		public string SourceView { get { return this.GetPropertyValue("SourceView"); } }

		public string KeyColumns { get { return this.GetPropertyValue("KeyColumns"); } }
		public string RangeStartDateColumn { get { return this.GetPropertyValue("RangeStartDateColumn"); } }
		public string RangeEndDateColumn { get { return this.GetPropertyValue("RangeEndDateColumn"); } }
		public bool IsEndDateNext { get { return this.GetPropertyValue("EndDateNext").IsTrueString(); } }
		public string InscopeDaysColumn { get { return this.GetPropertyValue("InscopeDaysColumn"); } }
		public string DefaultEndDate { get { return this.GetPropertyValue("DefaultEndDate"); } }
		public string CheckSumColumn { get { return this.GetPropertyValue("CheckSumColumn"); } }

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
//	Created Date:		‎June ‎02, ‎2013, ‏‎11:00:39 AM
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
