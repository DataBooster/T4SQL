using System;
using System.Collections.Generic;
using System.Dynamic;

namespace T4SQL
{
	public partial class TemplateContext : DynamicObject
	{
		public class TemplateProperty
		{
			public string Value { get; set; }
			public string Link { get; set; }

			public TemplateProperty(string propertyValue, string propertyLink)
			{
				Value = propertyValue;
				Link = propertyLink;
			}
		}

		private Dictionary<string, TemplateProperty> _PropertyDictionary;
		public Dictionary<string, TemplateProperty> PropertyDictionary
		{
			get { return _PropertyDictionary; }
		}

		private Func<string, List<string>> _fListTableColumns;

		public TemplateContext(Func<string, List<string>> fListTableColumns)
		{
			_PropertyDictionary = new Dictionary<string, TemplateProperty>();
			_fListTableColumns = fListTableColumns;
		}

		public List<string> ListTableColumns(string tableName)
		{
			return _fListTableColumns(tableName);
		}

		private bool TryGetProperty(string propertyName, out object result)
		{
			TemplateProperty templateProperty;
			bool bFound = _PropertyDictionary.TryGetValue(propertyName, out templateProperty);

			result = templateProperty;
			return bFound;
		}

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
			_PropertyDictionary[indexes[0] as string] = value as TemplateProperty;
			return true;
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
