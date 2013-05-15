using System;
using System.Collections.Generic;

namespace T4SQL
{
	public class ServerEnvironment
	{
		private readonly Func<string, List<string>> _fListTableColumns;

		private readonly string _DatabaseProduct;
		private readonly string _ProductVersion;
		private readonly string _ServerName;

		public string DatabaseProduct { get { return _DatabaseProduct; } }
		public string ProductVersion { get { return _ProductVersion; } }
		public string ServerName { get { return _ServerName; } }

		public ServerEnvironment(Func<string, List<string>> fListTableColumns,
			string databaseProduct, string productVersion, string serverName)
		{
			_fListTableColumns = fListTableColumns;

			_DatabaseProduct = databaseProduct;
			_ProductVersion = productVersion;
			_ServerName = serverName;
		}

		public List<string> ListTableColumns(string tableName)
		{
			return _fListTableColumns(tableName);
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
//	Created Date:		May ‎14, ‎2013, ‏‎12:00:23 aM
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
