using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel;
using T4SQL.MetaData;

namespace T4SQL.Assoc
{
	[Description("Build a view which traverses all referenced tables from the root ForeignKeyBaseTable. " +
		"The view will include all columns from all relevent tables, but the end users might only access columns from a subset of the tables within the view. " +
		"If the tables are joined with outer joins, then the database optimizer can (and does) drop the un-needed tables from the plan.")]
	public partial class VNaviForeignKey : ITemplate, ITemplateProperties
	{
		#region Implement ITemplate Properties
		public TemplateContext Context { get; set; }
		#endregion

		#region Implement ITemplateProperties Methods
		public TemplateSpec GetPropertiesSpec()
		{
			TemplateSpec spec = new TemplateSpec();

			spec.AddProperty("ObjectView", "schema.VW_ObjViewName", null, "{+}The full name of object view");
			spec.AddProperty("ForeignKeyBaseTable", "schema.ForeignKeyBaseTable", null, "{+}Foreign Key Base Table");
			spec.AddProperty("AliasFormat", "{0}${1}", null, "[*]Only apply when column name is duplicate - {0}: Table; {1}: ColumnName");

			return spec;
		}
		#endregion

		#region Members to be called from the code blocks in the T4SQL Template file

		public string TemplateName { get { return this.GetTemplateName(); } }
		public string DbmsPlatform { get { return Context.DbServerEnv.DatabasePlatform; } }
		public Version DbmsVersion { get { return Context.DbServerEnv.ProductVersion; } }
		public string ObjectView { get { return this.GetPropertyValue("ObjectView"); } }
		public string ForeignKeyBaseTable { get { return this.GetPropertyValue("ForeignKeyBaseTable"); } }

		private JoinedTable _NavigationTable;
		public JoinedTable NavigationTable
		{
			get
			{
				if (_NavigationTable == null)
				{
					_NavigationTable = Context.DbServerEnv.NavigateForeignKeyTable(ForeignKeyBaseTable);
					_NavigationTable.SolveNameRepetition(this.GetPropertyValue("AliasFormat"));
				}

				return _NavigationTable;
			}
		}

		public string ColumnListClause
		{
			get
			{
				return string.Join(@",
", NavigationTable.ColumnAliases.Select(c => "\t" + c.ColumnExpression));
			}
		}

		public string JoinClause
		{
			get
			{
				return NavigationTable.BuildJoinClause().PushIndent();
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
//	Created Date:		July 03, 2013, 12:56:07 AM
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
