using System.Text.RegularExpressions;

namespace T4SQL
{
	public static class MiscExtensions
	{
		private static readonly Regex _DateStrRgx;

		static MiscExtensions()
		{
			_DateStrRgx = new Regex(@"^\s*'?(?<dt>\d{4}-\d{2}-\d{2})'?\s*$");
		}

		public static string ConstantDateExpr(this string strDate, string dbmsPlatform = "SQL Server")
		{
			if (string.IsNullOrWhiteSpace(strDate))
				return string.Empty;
			else
			{
				Match mc = _DateStrRgx.Match(strDate);

				if (mc.Success)
					return string.Format(((dbmsPlatform == "Oracle") ? "TO_DATE('{0}', 'YYYY-MM-DD')" : "CAST('{0}' AS DATE)"), mc.Groups["dt"].Value);
				else
					return strDate;
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
//	Created Date:		November 22, 2013, 11:51:26 PM
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
