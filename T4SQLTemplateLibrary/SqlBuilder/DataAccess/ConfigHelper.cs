using System.Data.Common;
using System.Configuration;

namespace T4SQL.SqlBuilder.DataAccess
{
	public static partial class ConfigHelper
	{
		private static int _EnginePollInterval;
		internal static int EnginePollInterval	// milliseconds
		{
			get { return _EnginePollInterval; }
			set { _EnginePollInterval = value; }
		}

		static partial void OnInitializing()
		{
			_ConnectionSettingKey = "T4SQLDB";
			_PackageSettingKey = "Engine_Package";
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
//	Created Date:		‎March ‎21, ‎2013, ‏‎10:57:36 PM
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
