using System.Collections.Generic;

namespace T4SQL.MetaData
{
	public class DbmsForeignKey
	{
		private string _ConstraintName;
		private DbmsRelationTree _ForeignKeyBaseTable;
		private List<DbmsColumn> _ForeignKeyColumns;
		private DbmsRelationTree _PrimaryUniqueKeyBaseTable;	// Child Table
		private List<DbmsColumn> _PrimaryUniqueKeyColumns;		// Child Columns

		public string ConstraintName
		{
			get { return _ConstraintName; }
		}

		public DbmsRelationTree ForeignKeyBaseTable
		{
			get { return _ForeignKeyBaseTable; }
		}

		public List<DbmsColumn> ForeignKeyColumns
		{
			get { return _ForeignKeyColumns; }
		}

		public DbmsRelationTree PrimaryUniqueKeyBaseTable
		{
			get { return _PrimaryUniqueKeyBaseTable; }
		}

		public List<DbmsColumn> PrimaryUniqueKeyColumns
		{
			get { return _PrimaryUniqueKeyColumns; }
		}

		public DbmsForeignKey(string constraintName,
			DbmsRelationTree foreignKeyBaseTable, DbmsRelationTree primaryUniqueKeyBaseTable)
		{
			_ConstraintName = constraintName;
			_ForeignKeyBaseTable = foreignKeyBaseTable;
			_PrimaryUniqueKeyBaseTable = primaryUniqueKeyBaseTable;

			_ForeignKeyColumns = new List<DbmsColumn>();
			_PrimaryUniqueKeyColumns = new List<DbmsColumn>();
		}

		public DbmsForeignKey(string constraintName,
			DbmsRelationTree foreignKeyBaseTable, string primaryUniqueKeyBaseTableName)
		{
			_ConstraintName = constraintName;
			_ForeignKeyBaseTable = foreignKeyBaseTable;
			_PrimaryUniqueKeyBaseTable = new DbmsRelationTree(primaryUniqueKeyBaseTableName, this);

			_ForeignKeyColumns = new List<DbmsColumn>();
			_PrimaryUniqueKeyColumns = new List<DbmsColumn>();
		}

		public void AddForeignKeyColumn(DbmsColumn foreignKeyColumn, DbmsColumn primaryUniqueKeyColumn)
		{
			_ForeignKeyColumns.Add(foreignKeyColumn);
			_PrimaryUniqueKeyColumns.Add(primaryUniqueKeyColumn);
		}

		public bool DetectLoopback()							// Call after the node has been added into the tree
		{
			DbmsForeignKey parentForeignKey;

			for (DbmsRelationTree baseTable = this.ForeignKeyBaseTable; ; baseTable = parentForeignKey.ForeignKeyBaseTable)
			{
				parentForeignKey = baseTable.ParentForeignKey;

				if (parentForeignKey == null)
					break;

				if (_ConstraintName.Equals(parentForeignKey.ConstraintName, System.StringComparison.OrdinalIgnoreCase))
					return true;
			}

			return false;
		}

		public bool DetectCycle(string constraintName)		// Call before the constraintName to be added into the tree
		{
			if (string.IsNullOrWhiteSpace(constraintName))
				return DetectLoopback();

			DbmsRelationTree baseTable;

			for (DbmsForeignKey foreignKey = this; foreignKey != null; foreignKey = baseTable.ParentForeignKey)
			{
				if (constraintName.Equals(foreignKey.ConstraintName, System.StringComparison.OrdinalIgnoreCase))
					return true;
				else
					baseTable = foreignKey.ForeignKeyBaseTable;
			}

			return false;
		}

		public bool IsNullable
		{
			get
			{
				foreach (DbmsColumn fkCol in _ForeignKeyColumns)
					if (fkCol.IsNullable)
						return true;

				return false;
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
//	Created Date:		‎July ‎02, ‎2013, ‏‎1:07:19 AM
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
