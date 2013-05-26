using System.Collections.Generic;

namespace T4SQL
{
	public class TemplateSpec
	{
		public class TemplatePropertySpec : TemplateContext.TemplateProperty
		{
			public string Description { get; set; }
			public short SortOrder { get; set; }

			public TemplatePropertySpec(string defaultValue, string linkState, string description, short sortOrder)
				: base(defaultValue, linkState)
			{
				Description = description;
				SortOrder = sortOrder;
			}
		}

		private readonly Dictionary<string, TemplatePropertySpec> _Properties;
		public Dictionary<string, TemplatePropertySpec> Properties
		{
			get { return _Properties; }
		}

		private short _Property_Order;

		public TemplateSpec()
		{
			_Properties = new Dictionary<string, TemplatePropertySpec>();
			_Property_Order = 0;
		}

		public void AddProperty(string propertyName, string defaultValue, string linkState, string description)
		{
			_Properties.Add(propertyName, new TemplatePropertySpec(defaultValue, linkState, description, ++_Property_Order));
		}
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
//	Created Date:		‎May ‎01, ‎2013, ‏‎08:56:25 AM
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
