using System.Data.Common;
using System.Configuration;

namespace T4SQL.SqlBuilder
{
	internal static class EngineConfig
	{
		private static DbProviderFactory _DbProviderFactory;
		public static DbProviderFactory DbProviderFactory
		{
			get { return _DbProviderFactory; }
		}

		private static string _ConnectionString;
		public static string ConnectionString
		{
			get { return _ConnectionString; }
		}

		private static string _DatabasePackage;
		public static string DatabasePackage
		{
			get { return _DatabasePackage; }
		}

		private static int _EnginePollInterval;
		internal static int EnginePollInterval	// milliseconds
		{
			get { return _EnginePollInterval; }
			set { _EnginePollInterval = value; }
		}

		static EngineConfig()
		{
			const string connectionSettingKey = "T4SQLDB";
			const string packageSettingKey = "Engine_Package";

			ConnectionStringSettings connSetting = ConfigurationManager.ConnectionStrings[connectionSettingKey];
			_DbProviderFactory = DbProviderFactories.GetFactory(connSetting.ProviderName);
			_ConnectionString = connSetting.ConnectionString;

			_DatabasePackage = ConfigurationManager.AppSettings[packageSettingKey];
			if (_DatabasePackage == null)
				_DatabasePackage = string.Empty;
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
