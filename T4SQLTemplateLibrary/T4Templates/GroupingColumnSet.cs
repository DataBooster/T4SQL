using System;
using System.Linq;

namespace T4SQL
{
	public class GroupingColumnSet
	{
		private readonly string _Column_List;
		private readonly string _GroupingAlias;
		private readonly string[] _Columns;

		public string Column_List { get { return _Column_List; } }
		public string GroupingAlias { get { return _GroupingAlias; } }
		public string[] Columns { get { return _Columns; } }

		public GroupingColumnSet(string column_List, string groupingAlias)
		{
			if (column_List.StartsWith("(", StringComparison.Ordinal) && column_List.EndsWith(")", StringComparison.Ordinal))
			{
				_Column_List = column_List;
				column_List = column_List.Substring(1, _Column_List.Length - 2);
			}
			else
				_Column_List = "(" + column_List + ")";

			_Columns = column_List.SplitColumns().Where(c => string.IsNullOrEmpty(c) == false).ToArray();

			_GroupingAlias = groupingAlias;
		}

		public int GetGroupingId(string[] allGroupingColumns)
		{
			int groupingId = 0;

			for (int i = 0; i < allGroupingColumns.Length; i++)
				groupingId = groupingId << 1 | (_Columns.Contains(allGroupingColumns[i], ColumnComparer.Dequote) ? 0 : 1);

			return groupingId;
		}

		public override string ToString()
		{
			return string.IsNullOrWhiteSpace(_GroupingAlias) ? _Column_List : _GroupingAlias;
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
//	Created Date:		‎June ‎16, ‎2013, ‏‎11:42:42 PM
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
