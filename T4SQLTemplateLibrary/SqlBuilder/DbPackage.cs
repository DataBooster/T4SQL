using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using DbParallel.DataAccess;

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

		public static void LoadEngineConfig(this DbAccess dbAccess)
		{
			const string sp = "GET_CONFIG";
			DbParameter outPollInterval = null;

			dbAccess.ExecuteNonQuery(GetProcedure(sp), parameters =>
			{
				outPollInterval = parameters.Add().SetName("outPoll_Interval").SetDirection(ParameterDirection.Output).SetDbType(DbType.Byte);
			});

			EngineConfig.EnginePollInterval = outPollInterval.Parameter<byte>() * 1000;
		}

		private static EngineMain.ServiceMode Ping(DbAccess dbAccess, string sp)
		{
			DbParameter outParameter = null;

			dbAccess.ExecuteNonQuery(GetProcedure(sp), parameters =>
			{
				outParameter = parameters.Add().SetName("outSwitch_To_Mode").SetDirection(ParameterDirection.Output).SetSize(EngineMain.ServiceModeMaxLen);
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
			string inDefault_Value, string inLink_State, string inProperty_Description)
		{
			const string sp = "REGISTER_TEMPLATE_SPEC";

			dbAccess.ExecuteNonQuery(GetProcedure(sp), parameters =>
			{
				parameters.Add("inClass_Name", inClass_Name);
				parameters.Add("inProperty_Name", inProperty_Name);
				parameters.Add("inDefault_Value", inDefault_Value);
				parameters.Add("inLink_State", inLink_State);
				parameters.Add("inProperty_Description", inProperty_Description);
			});
		}

		internal static List<string> ListTableColumns(this DbAccess dbAccess, string tableName)
		{
			const string sp = "LIST_COLUMN";
			List<string> columns = new List<string>();

			dbAccess.ExecuteReader(GetProcedure(sp), parameters =>
				{
					parameters.Add("inTable_Name", tableName);
				}, reader =>
				{
					columns.Add(reader.Field<string>("COLUMN_NAME"));
				});

			return columns;
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
