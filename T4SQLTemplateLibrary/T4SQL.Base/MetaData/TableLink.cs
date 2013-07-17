
namespace T4SQL.MetaData
{
	internal class TableLink
	{
		private JoinedTable _Staging;
		internal JoinedTable Staging
		{
			get { return _Staging; }
		}

		private string _ParentAlias;
		internal string ParentAlias
		{
			get
			{
				return _ParentAlias;
			}
			set
			{
				_ParentAlias = value ?? string.Empty;
				SetAlias();
			}
		}

		private string _Alias;
		internal string Alias
		{
			get { return _Alias; }
		}

		private byte _RelativePosition;
		internal byte RelativePosition
		{
			get
			{
				return _RelativePosition;
			}
			set
			{
				_RelativePosition = value;
				SetAlias();
			}
		}

		internal TableLink(JoinedTable staging)
		{
			_Staging = staging;
		}

		internal TableLink(JoinedTable staging, string parentAlias, byte relativePosition)
			: this(staging)
		{
			_RelativePosition = relativePosition;
			ParentAlias = parentAlias;
		}

		private void SetAlias()
		{
			const string alias = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

			_Alias = _ParentAlias +
				((_RelativePosition < alias.Length) ? alias[_RelativePosition].ToString() : _RelativePosition.ToString());
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
//	Created Date:		July ‎10, ‎2013, ‏‎8:35:46 PM
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
