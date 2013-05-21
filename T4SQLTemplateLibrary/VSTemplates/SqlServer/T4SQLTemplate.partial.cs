using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using T4SQL;

namespace $rootnamespace$
{
	[Description("<ToDo>Template Description ... (Max 1024 Chars)</ToDo>")]
	public partial class $safeitemname$ : ITemplate, ITemplateProperties
	{
		public TemplateContext Context { get; set; }

		public TemplateSpec GetPropertiesSpec()
		{
			TemplateSpec spec = new TemplateSpec();

			// <ToDo>
			spec.AddProperty("ObjectView", "dbo.VW_TODO", null, "The full name of object view.");
			spec.AddProperty("PropertyName", "DefaultValue", null, "Property description ... (Max 1024 Chars)");
			// ...
			// </ToDo>

			return spec;
		}
	}
}
