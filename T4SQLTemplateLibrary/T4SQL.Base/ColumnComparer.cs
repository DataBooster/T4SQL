using System;
using System.Text.RegularExpressions;

namespace T4SQL
{
	public class ColumnComparer : StringComparer
	{
		private static readonly ColumnComparer _DequoteComparer;
		private static readonly Regex _ColumnExtract, _ColumnDequote;

		static ColumnComparer()
		{
			_ColumnExtract = new Regex(@"(?<col>" +
				@"(""[^""]*""" +
				@"|'[^']*'" +
				@"|((?<lp>\()[^\(\)]*)+((?<rp-lp>\))(?(lp)[^\(\)]*))+" +
				@"|((?<ls>\[)[^\[\]]*)+((?<rs-ls>\])(?(ls)[^\[\]]*))+" +
				@"|((?<lb>\{)[^\{\}]*)+((?<rb-lb>\})(?(lb)[^\{\}]*))+" +
				@"|[^\(\[\{""'\.]*" +
				@")+)\.",
				RegexOptions.ExplicitCapture | RegexOptions.Compiled);

			_ColumnDequote = new Regex(@"^""(?<dq>.*)""$|^'(?<dq>.*)'$|^\[(?<dq>.*)\]$|^\((?<dq>.*)\)$|^\{(?<dq>.*)\}$");

			_DequoteComparer = new ColumnComparer();
		}

		public static ColumnComparer Dequote { get { return _DequoteComparer; } }

		private string ExtractColumnName(string quoteName)
		{
			if (string.IsNullOrEmpty(quoteName))
				return quoteName;

			MatchCollection mcs = _ColumnExtract.Matches(quoteName + ".");
			Match lastMatch = null;

			foreach (Match m in mcs)
				lastMatch = m;

			if (lastMatch != null)
				quoteName = lastMatch.Groups["col"].Value;

			return _ColumnDequote.Replace(quoteName.Trim(), @"${dq}").ToUpperInvariant();
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
