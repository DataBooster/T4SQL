using System.Data.Common;
using System.Configuration;

namespace T4SQL.SqlBuilder.DataAccess
{
	public static partial class ConfigHelper
	{
		private static string _ConnectionSettingKey = "T4SQLDB";
		private static string _PackageSettingKey = "Engine_Package";

		#region Properties
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
		#endregion

		static ConfigHelper()
		{
			ConfigInit();

			#region Default Initialization
			ConnectionStringSettings connSetting = ConfigurationManager.ConnectionStrings[_ConnectionSettingKey];
			_DbProviderFactory = DbProviderFactories.GetFactory(connSetting.ProviderName);
			_ConnectionString = connSetting.ConnectionString;

			_DatabasePackage = ConfigurationManager.AppSettings[_PackageSettingKey];
			if (_DatabasePackage == null)
				_DatabasePackage = string.Empty;
			#endregion
		}

		static partial void ConfigInit();
	}
}
