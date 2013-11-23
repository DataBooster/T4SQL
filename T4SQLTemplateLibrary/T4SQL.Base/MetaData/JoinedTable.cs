using System;
using System.Collections.Generic;
using System.Text;

namespace T4SQL.MetaData
{
	public class JoinedTable
	{
		public class ColumnAlias
		{
			private DbmsRelationTree _Table;
			private DbmsColumn _Column;

			private bool _IsUniqueName;
			internal bool IsUniqueName
			{
				get { return _IsUniqueName; }
				set { _IsUniqueName = value; }
			}

			private string _Alias;
			public string Alias
			{
				get { return _Alias; }
				set { _Alias = value; }
			}

			internal ColumnAlias(DbmsRelationTree table, DbmsColumn column)
			{
				_Table = table;
				_Column = column;
				_IsUniqueName = false;
				_Alias = null;
			}

			public string TableName
			{
				get { return _Table.TableName; }
			}

			public string TableQualifiedName
			{
				get { return _Table.QualifiedName; }
			}

			public string TableAlias
			{
				get { return _Table.LinkProperty.Alias; }
			}

			public string ColumnName
			{
				get { return _Column.ColumnName; }
			}

			internal string DetectedName
			{
				get { return string.IsNullOrEmpty(_Alias) ? ColumnName : _Alias; }
			}

			public string ColumnExpression
			{
				get
				{
					if (string.IsNullOrEmpty(_Alias))
						return string.Format("{0}.{1}", TableAlias, ColumnName);
					else
						return string.Format("{0}.{1} AS {2}", TableAlias, ColumnName, Alias);
				}
			}

			public override string ToString()
			{
				return ColumnExpression;
			}
		}

		private DbmsRelationTree _RootTable;
		private List<ColumnAlias> _ColumnAliases;

		public List<ColumnAlias> ColumnAliases
		{
			get { return _ColumnAliases; }
		}

		internal JoinedTable(DbmsRelationTree rootTable)
		{
			_RootTable = rootTable;
			_ColumnAliases = new List<ColumnAlias>();
			TraverseRelationTree(rootTable, string.Empty, 0);
		}

		private void TraverseRelationTree(DbmsRelationTree nodeTable, string parentAlias, byte position)
		{
			nodeTable.LinkProperty = new TableLink(this, parentAlias, position);

			foreach (DbmsColumn col in nodeTable.DisplayColumns)
				_ColumnAliases.Add(new ColumnAlias(nodeTable, col));

			for (byte pos = 0; pos < nodeTable.ForeignKeys.Count; pos++)
				TraverseRelationTree(nodeTable.ForeignKeys[pos].PrimaryUniqueKeyBaseTable, nodeTable.LinkProperty.Alias, pos);
		}

		public string BuildJoinClause()
		{
			return BuildJoinClause(_RootTable);
		}

		private string BuildJoinClause(DbmsRelationTree startTable)
		{
			StringBuilder joinClause = new StringBuilder();

			joinClause.AppendFormat(startTable.QualifiedName);
			joinClause.Append(" ");
			joinClause.AppendLine(startTable.LinkProperty.Alias);

			foreach (DbmsForeignKey fk in startTable.ForeignKeys)	// INNER JOIN first
				if (!fk.IsNullable)									// ForeignKeyColumns is not null
					AppendJoinSource(joinClause, fk);

			foreach (DbmsForeignKey fk in startTable.ForeignKeys)	// Then LEFT JOIN
				if (fk.IsNullable)									// ForeignKeyColumns is nullable
					AppendJoinSource(joinClause, fk);

			return joinClause.ToString();
		}

		private void AppendJoinSource(StringBuilder joinClause, DbmsForeignKey foreignKey)
		{
			string fkTableAlias = foreignKey.ForeignKeyBaseTable.LinkProperty.Alias;
			string pkTableAlias = foreignKey.PrimaryUniqueKeyBaseTable.LinkProperty.Alias;
			List<DbmsColumn> fkColumns = foreignKey.ForeignKeyColumns;
			List<DbmsColumn> pkColumns = foreignKey.PrimaryUniqueKeyColumns;
			int nCols = foreignKey.ForeignKeyColumns.Count;
			bool parentheses = foreignKey.PrimaryUniqueKeyBaseTable.ForeignKeys.Count > 0;

			if (foreignKey.IsNullable)
				joinClause.Append("LEFT JOIN");
			else
				joinClause.Append("INNER JOIN");

			if (parentheses)
				joinClause.AppendLine(" (");
			else
				joinClause.AppendLine();

			joinClause.Append(BuildJoinClause(foreignKey.PrimaryUniqueKeyBaseTable).PushIndent());

			if (parentheses)
				joinClause.Append(") ");

			joinClause.Append("ON (");

			for (int i = 0; i < nCols; i++)
			{
				if (i > 0)
					joinClause.Append(" AND ");

				joinClause.AppendFormat("{0}.{1} = {2}.{3}", fkTableAlias, fkColumns[i].ColumnName, pkTableAlias, pkColumns[i].ColumnName);
			}

			joinClause.AppendLine(")");
		}

		public bool SolveNameRepetition(string renameFormat)	// renameFormat: "{0}${1}" - {0}: Table; {1}: ColumnName
		{
			int[] solvedModes = new int[_ColumnAliases.Count];
			bool solvedAll = false;
			ColumnAlias col1, col2;
			int cntRepetition;

			for (int slnMode = 1; slnMode < 5 && !solvedAll; slnMode++)
			{
				solvedAll = true;

				for (int i = 0; i < _ColumnAliases.Count - 1; i++)
				{
					col1 = _ColumnAliases[i];

					if (!col1.IsUniqueName && solvedModes[i] < slnMode)
					{
						cntRepetition = 1;

						for (int j = i + 1; j < _ColumnAliases.Count; j++)
						{
							col2 = _ColumnAliases[j];

							if (!col2.IsUniqueName && solvedModes[j] < slnMode)
								if (col2.DetectedName.Equals(col1.DetectedName, StringComparison.OrdinalIgnoreCase))
								{
									cntRepetition++;
									RenameColumnAlias(col2, slnMode, renameFormat, cntRepetition);
									solvedModes[j] = slnMode;
								}
						}

						if (cntRepetition == 1)
							col1.IsUniqueName = true;
						else
						{
							RenameColumnAlias(col1, slnMode, renameFormat, 1);
							solvedModes[i] = slnMode;
							solvedAll = false;
						}
					}
				}
			}

			return solvedAll;
		}

		private void RenameColumnAlias(ColumnAlias col, int slnMode, string renameFormat, int cntRepetition)
		{
			const int limitColumnLen = 30;
			const string finalNoFmt = "{0}${1}";
			string tryName;

			switch (slnMode)
			{
				case 0:		// Try to use just the ColumnName directly
					break;
				case 1:		// Try to use "TableName$ColumnName" if the total length <= 30 (for compatibility Oracle)
					tryName = string.Format(renameFormat, col.TableName, col.ColumnName);
					if (tryName.Length <= limitColumnLen)
						col.Alias = tryName;
					break;
				case 2:		// Try to use "TableAlias$ColumnName"
					col.Alias = string.Format(renameFormat, col.TableAlias, col.ColumnName);
					break;
				case 3:		// Reset Column Alias for slnMode 4
					col.Alias = null;
					break;
				case 4:		// Still not been solved by above, Rename as "ColumnName$123" ...
					col.Alias = string.Format(finalNoFmt, col.ColumnName, cntRepetition);
					break;
			}
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
//	Created Date:		July 10, 2013, 6:54:08 PM
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
