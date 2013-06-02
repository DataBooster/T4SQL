using System;
using System.Text.RegularExpressions;

namespace T4SQL
{
	public class ColumnComparer : StringComparer
	{
		private static readonly Regex _ColumnRegex;

		static ColumnComparer()
		{
			_ColumnRegex = new Regex(@"^(\s*(\[.*?\]|\"".*?\""|\S*)\s*\.+)*\s*(\[(?<sb>.*?)\]|\""(?<dq>.*?)\""|(?<ns>\S*))\s*$");
		}

		private string ExtractColumnName(string quoteName)
		{
			if (string.IsNullOrEmpty(quoteName))
				return quoteName;

			Match match = _ColumnRegex.Match(quoteName);

			if (match.Success)
			{
				Group col = match.Groups["sb"];

				if (col.Success == false)
				{
					col = match.Groups["dq"];

					if (col.Success == false)
						col = match.Groups["ns"];
				}

				if (col.Success)
					return col.Value.ToUpperInvariant();
			}

			return quoteName.ToUpperInvariant();
		}

		public override int Compare(string x, string y)
		{
			return string.Compare(ExtractColumnName(x), ExtractColumnName(y), StringComparison.Ordinal);
		}

		public override bool Equals(string x, string y)
		{
			return string.Equals(ExtractColumnName(x), ExtractColumnName(y), StringComparison.Ordinal);
		}

		public override int GetHashCode(string obj)
		{
			return ExtractColumnName(obj).GetHashCode();
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
//	Created Date:		‎May ‎30, ‎2013, ‏‎10:29:01 PM
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
