using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace T4SQL
{
	public static class UtilityExtensions
	{
		private static readonly ColumnComparer _ColumnComparer;
		private static readonly Regex _ColumnSplitter;
		private static readonly Regex[] _ColumnAliasModels;
		private static readonly Regex _AggFunFmt;

		static UtilityExtensions()
		{
			_ColumnComparer = new ColumnComparer();

			_ColumnSplitter = new Regex(@"\G(?<column>" +
				@"(""[^""]*""" +
				@"|'[^']*'" +
				@"|((?<lp>\()[^\(\)]*)+((?<rp-lp>\))(?(lp)[^\(\)]*))+" +
				@"|((?<ls>\[)[^\[\]]*)+((?<rs-ls>\])(?(ls)[^\[\]]*))+" +
				@"|((?<lb>\{)[^\{\}]*)+((?<rb-lb>\})(?(lb)[^\{\}]*))+" +
				@"|[^,\(\[\{""]*" +
				@")+),",
				RegexOptions.ExplicitCapture | RegexOptions.Compiled);

			_ColumnAliasModels = new Regex[] {
				new Regex(@"^(?<expr>" +
				    @"[\w@#][\w@$]*" +
				    @"|""[^""]*""" +
				    @"|((?<ls>\[)[^\[\]]*)+((?<rs-ls>\])(?(ls)[^\[\]]*))+(?(ls)(?!))" +
				    @"|((?<lp>\()[^\(\)]*)+((?<rp-lp>\))(?(lp)[^\(\)]*))+(?(lp)(?!))" +
				    @"|((?<lb>\{)[^\{\}]*)+((?<rb-lb>\})(?(lb)[^\{\}]*))+(?(lb)(?!))" +
				    @"|'[^']*'" +
				    @")$",
				    RegexOptions.ExplicitCapture | RegexOptions.Compiled),

				new Regex(@"^(?<expr>" +
				    @"(([^\(\[\{""'\w@#$\s][^\(\[\{""'\w@#$]*)?" +
				    @"(""[^""]*""" +
				    @"|((?<ls>\[)[^\[\]]*)+((?<rs-ls>\])(?(ls)[^\[\]]*))+(?(ls)(?!))" +
				    @"|((?<lp>\()[^\(\)]*)+((?<rp-lp>\))(?(lp)[^\(\)]*))+(?(lp)(?!))" +
				    @"|((?<lb>\{)[^\{\}]*)+((?<rb-lb>\})(?(lb)[^\{\}]*))+(?(lb)(?!))" +
				    @"|'[^']*'" +
				    @"|(?(\b[aA][sS]\b)(?!)|\b[\w@#][\w@$]*\b)" +
				    @")|\s+" +
				    @")+)" +
					@"(\b[aA][sS]\b\s*)?" +
					@"(?<alias>" +
					@"((?<lsa>\[)[^\[\]]*)+((?<rsa-lsa>\])(?(lsa)[^\[\]]*))+(?(lsa)(?!))" +
				    @"|""[^""]+""" +
				    @"|'[^']+'" +
				    @"|\b[\w@#][^'\[\]""=\s]*" +
				    @")$",
				    RegexOptions.ExplicitCapture | RegexOptions.Compiled),

				new Regex(@"^(?<alias>" +
					@"((?<ls>\[)[^\[\]]*)+((?<rs-ls>\])(?(ls)[^\[\]]*))+(?(ls)(?!))" +
					@"|""[^""]+""" +
					@"|'[^']+'" +
					@"|[^'\[""=]+" +
					@")\s*=\s*(?<expr>\S.*)$",
					RegexOptions.ExplicitCapture | RegexOptions.Compiled)
			};

			_AggFunFmt = new Regex(@"^\s*(?<fun>[^(]+)\s*(\(\s*(?<fmt>\{.+\})?\s*\)\s*)?$",
				RegexOptions.ExplicitCapture | RegexOptions.Compiled);
		}

		public static IEnumerable<string> SplitColumns(this string columns)
		{
			return _ColumnSplitter.Matches(columns + ",").Cast<Match>().Select(m => m.Groups["column"].Value.Trim());
		}

		public static Tuple<string, string> SegmentColumnAlias(this string columnClause)
		{
			string columnExpr, columnAlias = null;
			Match mc = null;

			columnClause = columnClause.Trim();

			foreach (Regex columnAliasModel in _ColumnAliasModels)
			{
				mc = columnAliasModel.Match(columnClause);

				if (mc.Success)
					break;
			}

			if (mc.Success)
			{
				Group ga = mc.Groups["alias"];
				columnExpr = mc.Groups["expr"].Value.TrimEnd();

				if (ga.Success)
				{
					columnAlias = ga.Value.TrimEnd();

					if (columnAlias.Equals("AS", StringComparison.OrdinalIgnoreCase))
						columnAlias = null;
				}
			}
			else
				columnExpr = columnClause;

			return Tuple.Create(columnExpr, columnAlias);
		}

		public static string NormalizeAggFunFmt(this string aggFunFmt)
		{
			Match m = _AggFunFmt.Match(aggFunFmt);

			if (m.Success)
			{
				Group gp = m.Groups["fmt"];
				string fmt = gp.Success ? gp.Value : "{0}";

				return string.Format(@"{0}({1})", m.Groups["fun"].Value.TrimEnd(), fmt);
			}
			else
				return aggFunFmt;
		}

		public static string GetDescriptionAttribute(this Type clsType)
		{
			Object[] fnds = clsType.GetCustomAttributes(typeof(DescriptionAttribute), false);

			return (fnds.Length > 0) ? (fnds[0] as DescriptionAttribute).Description : null;
		}

		public static string Left(this string str, int length)
		{
			if (str == null)
				return null;
			else if (length < 0)
				return string.Empty;
			else if (length >= str.Length)
				return str;
			else
				return str.Substring(0, length);
		}

		public static string Right(this string str, int length)
		{
			if (str == null)
				return null;
			else if (length < 0)
				return string.Empty;
			else if (length >= str.Length)
				return str;
			else
				return str.Substring(str.Length - length, length);
		}

		public static bool IsNullString(this string strNull)
		{
			return string.IsNullOrWhiteSpace(strNull) ? true : strNull.Equals("NULL", StringComparison.OrdinalIgnoreCase);
		}

		public static bool IsTrueString(this string strTrueOrFalse)
		{
			if (strTrueOrFalse == null)
				return false;

			string str = strTrueOrFalse.Trim().ToUpper();

			if (str == "TRUE" || str == "T" || str == "YES" || str == "Y" || str == "1")
				return true;
			else
				return false;
		}

		public static string InsertLeft(this string str, string separator = ", ")
		{
			return string.IsNullOrEmpty(str) ? string.Empty : str + separator;
		}

		public static string InsertLeft(this IEnumerable<string> values, string separator = ", ")
		{
			return InsertLeft(string.Join(separator, values), separator);
		}

		public static string InsertRight(this string str, string separator = ", ")
		{
			return string.IsNullOrEmpty(str) ? string.Empty : separator + str;
		}

		public static string InsertRight(this IEnumerable<string> values, string separator = ", ")
		{
			return InsertRight(string.Join(separator, values), separator);
		}

		public static string GetTemplateName(this ITemplate templateClass)
		{
			return templateClass.GetType().FullName;
		}

		public static string GetPropertyValue(this ITemplate template, string propertyName, string errorFormat = null)
		{
			try
			{
				return template.Context.Properties[propertyName].StringValue;
			}
			catch (KeyNotFoundException)
			{
				if (string.IsNullOrWhiteSpace(errorFormat))
					errorFormat = TemplateContext._PropertyNotFoundErrorFormat;

				template.Error(string.Format(errorFormat, propertyName));
			}

			return null;
		}

		public static IEnumerable<string> UnionColumns(this IEnumerable<string> first, IEnumerable<string> second)
		{
			return first.Union(second, _ColumnComparer);
		}

		public static IEnumerable<string> ExceptColumns(this IEnumerable<string> first, IEnumerable<string> second)
		{
			return first.Except(second, _ColumnComparer);
		}

		public static IEnumerable<string> IntersectColumns(this IEnumerable<string> first, IEnumerable<string> second)
		{
			return first.Intersect(second, _ColumnComparer);
		}

		public static IEnumerable<string> DistinctColumns(this IEnumerable<string> source)
		{
			return source.Distinct(_ColumnComparer);
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
//	Created Date:		April ‎05, ‎2013, ‏‎12:02:15 AM
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
