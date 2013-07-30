using DbParallel.DataAccess;

namespace T4SQL.SqlBuilder.DataAccess
{
	public static partial class DbPackage
	{
		#region Initialization
		static DbPackage()
		{
		//	DbAccess.DefaultCommandType = CommandType.StoredProcedure;
		}

		public static DbAccess CreateConnection()
		{
			return new DbAccess(ConfigHelper.DbProviderFactory, ConfigHelper.ConnectionString);
		}

		private static string GetProcedure(string sp)
		{
			return ConfigHelper.DatabasePackage + sp;
		}
		#endregion
	}
}
