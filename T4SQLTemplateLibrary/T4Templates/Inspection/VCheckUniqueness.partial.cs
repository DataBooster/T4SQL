using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel;
using T4SQL;

namespace T4SQL.Inspection
{
	[Description(" Checks the uniqueness by a supposed business key, ranks every row within their partition of the supposed key, and assigns a sequential number of every row.")]
	public partial class VCheckUniqueness : ITemplate, ITemplateProperties
	{
		#region Implement ITemplate Properties
		public TemplateContext Context { get; set; }
		#endregion

		#region Implement ITemplateProperties Methods
		public TemplateSpec GetPropertiesSpec()
		{
			TemplateSpec spec = new TemplateSpec();

			spec.AddProperty("ObjectView", "schema.ObjectView", null, "{+}The full name of object view");
			spec.AddProperty("SourceTable", "schema.SomeTable", null, "{+}Source Table");
			spec.AddProperty("SourceFilter", "", null, "[*]Search conditions");
			spec.AddProperty("KeyColumns", "Col1, Col2", null, "{+}Specifies a column or a list of columns which is supposed to be a unique key. A composite key (includes two or more columns) must be delimited by commas.");
			spec.AddProperty("OrderBy", "Col7 DESC, Col8", null, "{+}The ORDER_BY clause determines the sequence in which the rows are assigned their unique ROW_NUMBER within a specified partition.");
			spec.AddProperty("RowNumberAlias", "ROW$NUMBER", null, "{+}Specifies an alias name for returning the ROW_NUMBER");
			spec.AddProperty("CountAlias", "CNT$DUP", null, "[*]Specifies an alias name for returning the COUNT(*) within each partition");
			spec.AddProperty("RowidAlias", "ROW$ID", null, "[*]Specifies an alias name for returning the Oracle special pseudo column ROWID (Applies to Oracle only)");

			return spec;
		}
		#endregion

		#region Members to be called from the code blocks in the T4SQL Template file

		public string TemplateName { get { return this.GetTemplateName(); } }
		public string DbmsPlatform { get { return Context.DbServerEnv.DatabasePlatform; } }
		public Version DbmsVersion { get { return Context.DbServerEnv.ProductVersion; } }
		public string ObjectView { get { return this.GetPropertyValue("ObjectView"); } }
		public string SourceTable { get { return this.GetPropertyValue("SourceTable"); } }
		public string SourceFilter { get { return this.GetPropertyValue("SourceFilter"); } }
		public string KeyColumns { get { return this.GetPropertyValue("KeyColumns"); } }
		public string OrderBy { get { return this.GetPropertyValue("OrderBy"); } }
		public string RowNumberAlias { get { return this.GetPropertyValue("RowNumberAlias"); } }
		public string CountAlias { get { return this.GetPropertyValue("CountAlias"); } }
		public string RowidAlias { get { return this.GetPropertyValue("RowidAlias"); } }

		#endregion
	}
}
