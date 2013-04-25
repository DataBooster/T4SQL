using System.Linq;
using System.Collections.Generic;
using DbParallel.DataAccess;

namespace T4SQL.SqlBuilder
{
	internal class Workspace
	{
		private string _Workitem_Table_Name;
		public string Workitem_Table_Name
		{
			get { return _Workitem_Table_Name; }
			set { _Workitem_Table_Name = value; }
		}

		private string _Property_Table_Name;
		public string Property_Table_Name
		{
			get { return _Property_Table_Name; }
			set { _Property_Table_Name = value; }
		}

		private List<Workitem> _Workitems;
		internal List<Workitem> Workitems
		{
			get { return _Workitems; }
		}

		private Workitem _recentWorkitem;

		private void SetWorkingProperty(string workitemName, string propertyName, string stringValue, string linkState, TemplateContext seedProperties)
		{
			if (_recentWorkitem == null || _recentWorkitem.Workitem_Name != workitemName)
			{
				_recentWorkitem = _Workitems.FirstOrDefault(i => i.Workitem_Name == workitemName);
				if (_recentWorkitem == null)
					return;
			}

			if (_recentWorkitem.WorkingProperties == null)
				_recentWorkitem.WorkingProperties = seedProperties.Copy();

			Dictionary<string, TemplateContext.TemplateProperty> propertyDictionary = _recentWorkitem.WorkingProperties.PropertyDictionary;
			TemplateContext.TemplateProperty templateProperty;

			if (propertyDictionary.TryGetValue(propertyName, out templateProperty) == false)
				propertyDictionary.Add(propertyName, new TemplateContext.TemplateProperty(stringValue, linkState));
			else
			{
				templateProperty.StringValue = stringValue;
				templateProperty.LinkState = linkState;
			}
		}

		public void LoadWorkitems(DbAccess dbAccess, Dictionary<string, TemplateContext> templateDefaultProperties, TemplateContext seedProperties)
		{
			TemplateContext defaultProperties;

			_Workitems = dbAccess.LoadWorkitems(_Workitem_Table_Name).ToList();

			foreach (Workitem wi in _Workitems)
				if (templateDefaultProperties.TryGetValue(wi.Template_Name, out defaultProperties))
					wi.WorkingProperties = defaultProperties.Copy();

			dbAccess.LoadWorkingProperties(_Property_Table_Name, row =>
				{
					SetWorkingProperty(row.Field<string>("WORKITEM_NAME"), row.Field<string>("PROPERTY_NAME"),
						row.Field<string>("STRING_VALUE"), row.Field<string>("LINK_STATE"), seedProperties);
				});
		}

		public void BuildWorkitems(DbAccess dbAccess)
		{

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
//	Created Date:		‎‎April ‎24, ‎2013, ‏‎11:12:18 AM
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
