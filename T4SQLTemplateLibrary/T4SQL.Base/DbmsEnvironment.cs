using System;
using System.Linq;
using System.Collections.Generic;
using T4SQL.MetaData;

namespace T4SQL
{
	public class DbmsEnvironment
	{
		private readonly Func<string, IEnumerable<DbmsColumn>> _fListTableColumns;
		private readonly Func<string, DbmsRelationTree> _fLoadForeignKeys;

		private readonly string _DatabasePlatform;
		private readonly string _DatabaseProduct;
		private readonly Version _ProductVersion;
		private readonly string _ServerName;

		public string DatabasePlatform { get { return _DatabasePlatform; } }
		public string DatabaseProduct { get { return _DatabaseProduct; } }
		public Version ProductVersion { get { return _ProductVersion; } }
		public string ServerName { get { return _ServerName; } }

	//	internal Func<string, IEnumerable<DbmsColumn>> FuncListTableColumns { get { return _fListTableColumns; } }
	//	internal Func<string, DbmsRelationTree> FuncLoadForeignKeys { get { return _fLoadForeignKeys; } }

		public DbmsEnvironment(Func<string, IEnumerable<DbmsColumn>> fListTableColumns,
			Func<string, DbmsRelationTree> fLoadForeignKeys,
			string databasePlatform, string databaseProduct, string productVersion, string serverName)
		{
			_fListTableColumns = fListTableColumns;
			_fLoadForeignKeys = fLoadForeignKeys;

			_DatabasePlatform = databasePlatform;
			_DatabaseProduct = databaseProduct;
			_ProductVersion = new Version(productVersion);
			_ServerName = serverName;
		}

		public IEnumerable<string> ListTableColumns(string tableName, string specificColumns = "*")
		{
			if (string.IsNullOrWhiteSpace(specificColumns) || specificColumns.Trim() == "*")
				return _fListTableColumns(tableName).Select(col => col.ColumnName);
			else
				return specificColumns.SplitColumns();
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
//	Created Date:		May ‎14, ‎2013, ‏‎12:00:23 AM
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
