
namespace T4SQL.MetaData
{
	public class DbmsColumn
	{
		private string _ColumnName;
		public string ColumnName
		{
			get { return _ColumnName; }
			set { _ColumnName = value; }
		}

		private bool _IsNullable;
		public bool IsNullable
		{
			get { return _IsNullable; }
			set { _IsNullable = value; }
		}

		public DbmsColumn(string columnName, bool isNullable)
		{
			_ColumnName = columnName;
			_IsNullable = isNullable;
		}

		public DbmsColumn()
		{
			_ColumnName = string.Empty;
			_IsNullable = true;
		}

		public override string ToString()
		{
			return _ColumnName;
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
//	Created Date:		July 02, 2013, 1:07:19 AM
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
