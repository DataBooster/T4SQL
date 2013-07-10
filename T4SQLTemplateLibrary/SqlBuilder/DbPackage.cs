using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using DbParallel.DataAccess;
using T4SQL.MetaData;

namespace T4SQL.SqlBuilder
{
	internal static class DbPackage
	{
		public static DbAccess CreateConnection()
		{
			return new DbAccess(EngineConfig.DbProviderFactory, EngineConfig.ConnectionString);
		}

		private static string GetProcedure(string sp)
		{
			return EngineConfig.DatabasePackage + sp;
		}

		public static DbmsEnvironment GetDbServerEnv(this DbAccess dbAccess)
		{
			const string sp = "GET_DB_SERVER_ENV";
			DbParameter outDatabase_Platform = null;
			DbParameter outDatabase_Product = null;
			DbParameter outProduct_Version = null;
			DbParameter outServer_Name = null;

			dbAccess.ExecuteNonQuery(GetProcedure(sp), parameters =>
			{
				outDatabase_Platform = parameters.AddOutput("outDatabase_Platform", 32);
				outDatabase_Product = parameters.AddOutput("outDatabase_Product", 256);
				outProduct_Version = parameters.AddOutput("outProduct_Version", 64);
				outServer_Name = parameters.AddOutput("outServer_Name", 64);
			});

			return new DbmsEnvironment(tableName => dbAccess.ListTableColumns(tableName),
				tableName => dbAccess.LoadForeignKeys(tableName),
				outDatabase_Platform.Parameter<string>(), outDatabase_Product.Parameter<string>(),
				outProduct_Version.Parameter<string>(), outServer_Name.Parameter<string>());
		}

		public static void LoadEngineConfig(this DbAccess dbAccess)
		{
			const string sp = "GET_CONFIG";
			DbParameter outPollInterval = null;

			dbAccess.ExecuteNonQuery(GetProcedure(sp), parameters =>
			{
				outPollInterval = parameters.AddOutput("outPoll_Interval").SetDbType(DbType.Byte);
			});

			EngineConfig.EnginePollInterval = outPollInterval.Parameter<byte>() * 1000;
		}

		private static EngineMain.ServiceMode Ping(DbAccess dbAccess, string sp)
		{
			DbParameter outParameter = null;

			dbAccess.ExecuteNonQuery(GetProcedure(sp), parameters =>
			{
				outParameter = parameters.AddOutput("outSwitch_To_Mode", EngineMain.ServiceModeMaxLen);
			});

			EngineMain.ServiceMode newMode;

			if (Enum.TryParse<EngineMain.ServiceMode>(outParameter.Value as string, out newMode))
				return newMode;
			else
				return EngineMain.ServiceMode.Standby;
		}

		public static EngineMain.ServiceMode StandbyPing(this DbAccess dbAccess)
		{
			return Ping(dbAccess, "STANDBY_PING");
		}

		public static EngineMain.ServiceMode PrimaryPing(this DbAccess dbAccess)
		{
			return Ping(dbAccess, "PRIMARY_PING");
		}

		public static void RegisterTemplates(this DbAccess dbAccess, Dictionary<string, Type> templateClassDictionary)
		{
			const string sp0 = "SERVICE_RESTART";
			const string sp1 = "REGISTER_TEMPLATE";

			dbAccess.ExecuteNonQuery(GetProcedure(sp0));

			foreach (KeyValuePair<string, Type> kvp in templateClassDictionary)
				dbAccess.ExecuteNonQuery(GetProcedure(sp1), parameters =>
				{
					parameters.Add("inFull_Name", kvp.Key);
					parameters.Add("inModule", kvp.Value.Module.Name);
					parameters.Add("inAssembly_String", kvp.Value.Assembly.FullName);
					parameters.Add("inClass_Description", kvp.Value.GetDescriptionAttribute().Left(512));
				});
		}

		public static void RegisterTemplatesSpec(this DbAccess dbAccess, string inClass_Name, string inProperty_Name,
			string inDefault_Value, string inLink_State, string inProperty_Description, short inProperty_Order)
		{
			const string sp = "REGISTER_TEMPLATE_SPEC";

			dbAccess.ExecuteNonQuery(GetProcedure(sp), parameters =>
			{
				parameters.Add("inClass_Name", inClass_Name);
				parameters.Add("inProperty_Name", inProperty_Name);
				parameters.Add("inDefault_Value", inDefault_Value);
				parameters.Add("inLink_State", inLink_State);
				parameters.Add("inProperty_Description", inProperty_Description);
				parameters.Add("inProperty_Order", inProperty_Order);
			});
		}

		internal static IEnumerable<DbmsColumn> ListTableColumns(this DbAccess dbAccess, string tableName)
		{
			const string sp = "LIST_COLUMN";

			return dbAccess.ExecuteReader<DbmsColumn>(GetProcedure(sp), parameters =>
				{
					parameters.Add("inTable_Name", tableName);
				}, map =>
				{
					map.Add("COLUMN_NAME", t => t.ColumnName);
					map.Add("IS_NULLABLE", t => t.IsNullable);
				});
		}

		private static void LoadForeignKeys(this DbAccess dbAccess, DbmsRelationTree foreignKeyBaseTable)
		{
			const string sp = "GET_FOREIGN_KEY";
			DbParameter outTable_Name = null;

			dbAccess.ExecuteReader(GetProcedure(sp), parameters =>
			{
				parameters.Add("inTable_Name", foreignKeyBaseTable.TableName);
				outTable_Name = parameters.AddOutput("outTable_Name", 128);
			}, reader =>
			{
				foreignKeyBaseTable.AddForeignKeyColumn(
					reader.Field<string>("CONSTRAINT_NAME"),
					reader.Field<string>("FOREIGN_KEY_COLUMN"),
					reader.Field<bool>("FOREIGN_KEY_NULLABLE"),
					reader.Field<string>("REFERENCED_TABLE"),
					reader.Field<string>("REFERENCED_COLUMN"),
					reader.Field<bool>("REFERENCED_NULLABLE"));
			});

			foreignKeyBaseTable.TableName = outTable_Name.Parameter<string>();
			foreignKeyBaseTable.Columns = ListTableColumns(dbAccess, foreignKeyBaseTable.TableName).ToArray();

			foreach (DbmsForeignKey fk in foreignKeyBaseTable.ForeignKeys)
				LoadForeignKeys(dbAccess, fk.PrimaryUniqueKeyBaseTable);
		}

		internal static DbmsRelationTree LoadForeignKeys(this DbAccess dbAccess, string tableName)
		{
			DbmsRelationTree rootTable = new DbmsRelationTree(tableName);

			LoadForeignKeys(dbAccess, rootTable);

			return rootTable;
		}

		internal static void LoadDefaultProperties(this DbAccess dbAccess, Action<DbDataReader> setDefaultProperty)
		{
			const string sp = "LIST_DEFAULT_PROPERTY";

			dbAccess.ExecuteReader(GetProcedure(sp), null, setDefaultProperty);
		}

		internal static IEnumerable<Workspace> LoadWorkspaceTasks(this DbAccess dbAccess)
		{
			const string sp = "LIST_WORKSPACE";

			return dbAccess.ExecuteReader<Workspace>(GetProcedure(sp), null);
		}

		internal static IEnumerable<Workitem> LoadWorkitems(this DbAccess dbAccess, string workitemTable)
		{
			const string sp = "LIST_WORKITEM";

			return dbAccess.ExecuteReader<Workitem>(GetProcedure(sp), parameters =>
				{
					parameters.Add("inWorkitem_Table", workitemTable);
				});
		}

		internal static void LoadWorkingProperties(this DbAccess dbAccess, string propertyTable, Action<DbDataReader> setWorkingProperty)
		{
			const string sp = "LIST_WORKING_PROPERTY";

			dbAccess.ExecuteReader(GetProcedure(sp), parameters =>
			{
				parameters.Add("inProperty_Table", propertyTable);
			}, setWorkingProperty);
		}

		internal static void CompileWorkitem(this DbAccess dbAccess, string workitem_Table, string workitem_Name, string compiled_Error, string object_Code)
		{
			const string sp = "COMPILE_WORKITEM";

			dbAccess.ExecuteNonQuery(GetProcedure(sp), parameters =>
			{
				parameters.Add("inWorkitem_Table", workitem_Table);
				parameters.Add("inWorkitem_Name", workitem_Name);
				parameters.Add("inCompiled_Error", compiled_Error);
				parameters.Add("inObject_Code", object_Code);
			});
		}

		public static void LogSysError(this DbAccess dbAccess, string strReference, string strMessage)
		{
			const string sp = "LOG_SYS_ERROR";

			dbAccess.ExecuteNonQuery(GetProcedure(sp), parameters =>
			{
				parameters.Add("inReference", strReference);
				parameters.Add("inMessage", strMessage);
			});
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
//	Created Date:		‎March ‎22, ‎2013, ‏‎12:01:05 AM
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
