using System;
using System.Collections.Generic;
using System.Dynamic;

namespace T4SQL
{
	public partial class TemplateContext : DynamicObject
	{
		public class TemplateProperty
		{
			public string StringValue { get; set; }
			public string LinkState { get; set; }

			public TemplateProperty(string stringValue, string linkState)
			{
				StringValue = stringValue;
				LinkState = linkState;
			}

			public override string ToString()
			{
				return StringValue;
			}
		}

		private readonly Dictionary<string, TemplateProperty> _Properties;
		public Dictionary<string, TemplateProperty> Properties
		{
			get { return _Properties; }
		}

		private readonly DbmsEnvironment _DbServerEnv;
		public DbmsEnvironment DbServerEnv
		{
			get { return _DbServerEnv; }
		}

		private readonly dynamic _PropertyBag;
		public dynamic PropertyBag
		{
			get { return _PropertyBag; }
		}

		#region Error Message Formats
		public static string _PropertyNotFoundErrorFormat = "Property \"{0}\" is not found in the workitem.";
		#endregion

		public TemplateContext(DbmsEnvironment dbServerEnv)
		{
			_Properties = new Dictionary<string, TemplateProperty>();
			_DbServerEnv = dbServerEnv;
			_PropertyBag = this as dynamic;
		}

		private bool TryGetProperty(string propertyName, out object result)
		{
			TemplateProperty templateProperty;
			bool bFound = _Properties.TryGetValue(propertyName, out templateProperty);

			result = templateProperty;	// .StringValue;
			return bFound;
		}

		private bool TrySetProperty(string propertyName, object value)
		{
			if (value is TemplateProperty)
			{
				_Properties[propertyName] = value as TemplateProperty;
				return true;
			}
			else if (value is string)
			{
				_Properties[propertyName] = new TemplateProperty(value as string, null);
				return true;
			}
			else
				return false;
		}

		#region override methods
		public override bool TryGetMember(System.Dynamic.GetMemberBinder binder, out object result)
		{
			return TryGetProperty(binder.Name, out result);
		}

		public override bool TryGetIndex(System.Dynamic.GetIndexBinder binder, object[] indexes, out object result)
		{
			return TryGetProperty(indexes[0] as string, out result);
		}

		public override bool TrySetIndex(System.Dynamic.SetIndexBinder binder, object[] indexes, object value)
		{
			return TrySetProperty(indexes[0] as string, value);
		}

		public override bool TrySetMember(System.Dynamic.SetMemberBinder binder, object value)
		{
			return TrySetProperty(binder.Name, value);
		}
		#endregion

		public void Clear()
		{
			_Properties.Clear();
		}

		public TemplateContext Copy()
		{
			TemplateContext newInstance = new TemplateContext(_DbServerEnv);

			foreach (KeyValuePair<string, TemplateProperty> kvp in _Properties)
				newInstance.Properties.Add(kvp.Key, new TemplateProperty(kvp.Value.StringValue, kvp.Value.LinkState));

			return newInstance;
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
//	Created Date:		March 07, 2013, 11:23:23 PM
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
