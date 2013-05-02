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
		}

		private readonly Dictionary<string, TemplateProperty> _Properties;
		public Dictionary<string, TemplateProperty> Properties
		{
			get { return _Properties; }
		}

		private readonly Func<string, List<string>> _fListTableColumns;

		public TemplateContext(Func<string, List<string>> fListTableColumns)
		{
			_Properties = new Dictionary<string, TemplateProperty>();
			_fListTableColumns = fListTableColumns;
		}

		public List<string> ListTableColumns(string tableName)
		{
			return _fListTableColumns(tableName);
		}

		private bool TryGetProperty(string propertyName, out object result)
		{
			TemplateProperty templateProperty;
			bool bFound = _Properties.TryGetValue(propertyName, out templateProperty);

			result = templateProperty;
			return bFound;
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
			_Properties[indexes[0] as string] = value as TemplateProperty;
			return true;
		}

		public override bool TrySetMember(System.Dynamic.SetMemberBinder binder, object value)
		{
			_Properties[binder.Name] = value as TemplateProperty;
			return true;
		}
		#endregion

		public void Clear()
		{
			_Properties.Clear();
		}

		public TemplateContext Copy()
		{
			TemplateContext newInstance = new TemplateContext(_fListTableColumns);

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
//	Created Date:		March ‎07, ‎2013, ‏‎11:23:23 PM
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
