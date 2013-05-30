using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace T4SQL
{
	public static class UtilityExtensions
	{
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

		public static IEnumerable<string> SplitToCollection(this string input, char separator = ',', char leftQuote = '"', char rightQuote = '"')
		{
			Regex splitter = new Regex(string.Format(@"\G(?<item>([^{0}{1}]*[{1}][^{2}]*[{2}][^{0}{1}]*)+|[^{0}]*)[{0}]",
				Regex.Escape(new string(separator, 1)), Regex.Escape(new string(leftQuote, 1)), Regex.Escape(new string(rightQuote, 1))));

			return splitter.Matches(input + separator).Cast<Match>().Select(m => m.Groups["item"].Value.Trim());
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
