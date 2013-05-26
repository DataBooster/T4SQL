using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel;
using T4SQL;

namespace T4SQL.SqlServer.Date
{
	[Description("Transform time points (turning points) data into time ranges data.")]
	public partial class TimePointsToRanges : ITemplate, ITemplateProperties
	{
		#region Implement ITemplate Properties
		public TemplateContext Context { get; set; }
		#endregion

		#region Implement ITemplateProperties Methods
		public TemplateSpec GetPropertiesSpec()
		{
			TemplateSpec spec = new TemplateSpec();

			spec.AddProperty("ObjectView", "dbo.VW_ViewName_ToDo", null, "The full name of object view");
			spec.AddProperty("SourceView", "schema.SourceTableOrView", null, "Source Table Or View");
			spec.AddProperty("KeyColumns", "COL1, COL2", null, "The key column or a comma-separated list of key columns - exclude the date column of time point");
			spec.AddProperty("DateColumn", "DATE_", null, "Source date column of time point");
			spec.AddProperty("RangeStartDateColumn", "START_DATE", null, "Time range Start Date column");
			spec.AddProperty("RangeEndDateColumn", "END_DATE", null, "Time range End Date column");
			spec.AddProperty("EndDateNext", "0", null, "0: [START_DATE <= Time Range <= END_DATE]; 1: [START_DATE <= Time Range < END_DATE)");
			spec.AddProperty("DefaultEndDate", "CAST('9999-12-31' AS DATE)", null, "Ultimate END_DATE as the substitute of IS NULL");

			return spec;
		}
		#endregion

		#region Members to be called from the code blocks in the tt file

		public string TemplateName { get { return this.GetTemplateName(); } }
		public Version DbmsVersion { get { return Context.DbServerEnv.ProductVersion; } }
		public string ObjectView { get { return this.GetPropertyValue("ObjectView"); } }
		public string SourceView { get { return this.GetPropertyValue("SourceView"); } }

		public IEnumerable<string> GetKeyColumns()
		{
			return this.GetPropertyValue("KeyColumns").SplitToCollection();
		}

		public IEnumerable<string> GetRemainColumns()
		{
			return Context.DbServerEnv.ListTableColumns(this.GetPropertyValue("SourceView")).Except
				(new string[] { this.GetPropertyValue("DateColumn") }, StringComparer.OrdinalIgnoreCase);
		}

		public string KeyColumns { get { return this.GetPropertyValue("KeyColumns"); } }
		public string DateColumn { get { return this.GetPropertyValue("DateColumn"); } }
		public string RangeStartDateColumn { get { return this.GetPropertyValue("RangeStartDateColumn"); } }
		public string RangeEndDateColumn { get { return this.GetPropertyValue("RangeEndDateColumn"); } }
		public string DefaultEndDate { get { return this.GetPropertyValue("DefaultEndDate"); } }

		public int EndDateNext
		{
			get
			{
				int nextDay;

				if (int.TryParse(this.GetPropertyValue("EndDateNext"), out nextDay))
					return nextDay - 1;
				else
					return -1;
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
