using System;
using System.Collections.Generic;
using System.Linq;

namespace T4SQL.MetaData
{
	public class DbmsRelationTree
	{
		private string _nodeTableName;
		private DbmsForeignKey _ParentForeignKey;
		private List<DbmsForeignKey> _ForeignKeys;
		private DbmsColumn[] _Columns;

		public DbmsRelationTree(string tableName, DbmsForeignKey parentForeignKey = null)
		{
			_nodeTableName = tableName;
			_ParentForeignKey = parentForeignKey;
			_ForeignKeys = new List<DbmsForeignKey>();
		}

		public string TableName
		{
			get { return _nodeTableName; }
			set { _nodeTableName = value; }
		}

		public DbmsForeignKey ParentForeignKey
		{
			get { return _ParentForeignKey; }
		}

		public List<DbmsForeignKey> ForeignKeys
		{
			get { return _ForeignKeys; }
		}

		public DbmsColumn[] Columns
		{
			get { return _Columns; }
			set { _Columns = value; }
		}

		public void AddForeignKeyColumn(string constraintName, string foreignKeyColumn, bool foreignKeyNullable,
			string referencedTable, string referencedColumn, bool referencedNullable, bool detectLoopback = true)
		{
			DbmsForeignKey fk = _ForeignKeys.LastOrDefault(s => constraintName.Equals(s.ConstraintName, StringComparison.OrdinalIgnoreCase));

			if (fk == null)
			{
				if (detectLoopback && _ParentForeignKey != null)
					if (_ParentForeignKey.DetectLoopback(constraintName))
						return;

				fk = new DbmsForeignKey(constraintName, this, referencedTable);
				_ForeignKeys.Add(fk);
			}

			fk.AddForeignKeyColumn(new DbmsColumn(foreignKeyColumn, foreignKeyNullable), new DbmsColumn(referencedColumn, referencedNullable));
		}

		private string _Alias;
		internal string Alias
		{
			get { return _Alias; }
			set { _Alias = value; }
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
//	Created Date:		July ‎02, ‎2013, ‏‎1:07:19 AM
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
