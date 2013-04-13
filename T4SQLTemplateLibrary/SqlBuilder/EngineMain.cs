using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using DbParallel.DataAccess;

namespace T4SQL.SqlBuilder
{
	public class EngineMain
	{
		public enum ServiceMode
		{
			Standby,		// The Service run in Standby Mode.
			Primary			// The Service run in Primary Mode.
		}
		private static int _ServiceModeMaxLen = 0;
		public static int ServiceModeMaxLen
		{
			get
			{
				if (_ServiceModeMaxLen == 0)
					_ServiceModeMaxLen = Enum.GetNames(typeof(ServiceMode)).Max(e => e.Length);
				return _ServiceModeMaxLen;
			}
		}

		private readonly DbAccess _MainDbAccess;
		private readonly EventLog _ServiceEventLog;
		private readonly TemplateManager _TemplateManager;
		private volatile bool _KeepPolling;
		private Task _MainTask;
		private Func<string, List<string>> _fListTableColumns;

		public EngineMain(EventLog serviceEventLog = null)
		{
			_MainDbAccess = DbPackage.CreateConnection();
			_fListTableColumns = tableName => _MainDbAccess.ListTableColumns(tableName);

			_MainDbAccess.LoadEngineConfig();
			_ServiceEventLog = serviceEventLog;
			_TemplateManager = new TemplateManager();
		}

		public void Start()
		{
			_TemplateManager.LoadAddIns();

			_MainTask = Task.Factory.StartNew(() =>
			{
				if (_MainDbAccess.StandbyPing() == ServiceMode.Primary)
				{
					_MainDbAccess.RegisterTemplates(_TemplateManager.TemplateClassDictionary);
					_KeepPolling = true;

					if (_ServiceEventLog != null)
						_ServiceEventLog.WriteEntry("T4SQL Template Engine is started successfully.");
				}
				else
				{
					_KeepPolling = false;

					if (_ServiceEventLog != null)
						_ServiceEventLog.WriteEntry("The server cannot be started due to another engine is in service!");
				}

				while (_KeepPolling)
				{
					_MainDbAccess.PrimaryPing();

					try
					{
						dynamic bag = new TemplateContext(_fListTableColumns);
						bag["test1"] = new TemplateContext.TemplateProperty("v1", "a1");
						bag["test2"] = new TemplateContext.TemplateProperty("v2", "a2");
						bag["test3"] = new TemplateContext.TemplateProperty("v3", "a3");
						var t = bag.test1;
						//t = bag.test4;
						var u = bag["test3"];
						//u = bag["test4"];
						u = bag[4];
					}
					catch (Exception e)
					{
						if (_ServiceEventLog != null)
							_ServiceEventLog.WriteEntry(e.Message, EventLogEntryType.Error);

						_MainDbAccess.LogSysError(e.Source, e.Message);
					}

					Thread.Sleep(EngineConfig.EnginePollInterval);
				}
			});
		}

		public void Stop()
		{
			_KeepPolling = false;
			_MainTask.Wait();
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
//	Created Date:		‎March ‎22, ‎2013, ‏‎12:28:29 AM
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
