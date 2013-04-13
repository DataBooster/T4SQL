﻿using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;

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

		public static IEnumerable<string> Cut(this string str, string separator = ",")
		{
			return str.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).Where(s => s.Length > 0);
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
