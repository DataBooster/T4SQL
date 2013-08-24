using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel;
using T4SQL;

namespace $rootnamespace$
{
	[Description("<ToDo>Template Description ... (Max 1024 Chars)</ToDo>")]
	public partial class $safeitemname$ : ITemplate, ITemplateProperties
	{
		#region Implement ITemplate Properties
		public TemplateContext Context { get; set; }
		#endregion

		#region Implement ITemplateProperties Methods
		public TemplateSpec GetPropertiesSpec()
		{
			TemplateSpec spec = new TemplateSpec();

			// <ToDo> Specify all properties to be used in the template:
			spec.AddProperty("ObjectView", "schema.ObjectView", null, "{+}The full name of object view");
			spec.AddProperty("SourceView", "schema.SomeTableOrView", null, "{+}Source Table Or View");
//	...	...	spec.AddProperty("CustomPropertyName", "ExampleValue", null, "{+}Customization is necessary property description ... (Max 1024 Chars)");
//	...	...	spec.AddProperty("OptionPropertyName", "DefaultValue", null, "[*]Default can be acceptable property description ... (Max 1024 Chars)");
			// </ToDo>

			return spec;
		}
		#endregion

		#region Members to be called from the code blocks in the T4SQL Template file

		public string TemplateName { get { return this.GetTemplateName(); } }
		public string DbmsPlatform { get { return Context.DbServerEnv.DatabasePlatform; } }
		public Version DbmsVersion { get { return Context.DbServerEnv.ProductVersion; } }
		public string ObjectView { get { return this.GetPropertyValue("ObjectView"); } }
		public string SourceView { get { return this.GetPropertyValue("SourceView"); } }

		#endregion
	}
}
