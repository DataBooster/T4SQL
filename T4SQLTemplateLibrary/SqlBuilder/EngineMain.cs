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
		private List<Workspace> _WorkspaceTasks;
		private volatile bool _KeepPolling;
		private Task _MainTask;
		private ServerEnvironment _DbServerEnv;

		public EngineMain(EventLog serviceEventLog = null)
		{
			_MainDbAccess = DbPackage.CreateConnection();
			_DbServerEnv = _MainDbAccess.GetDbServerEnv();

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

					foreach (KeyValuePair<string, Type> kvp in _TemplateManager.TemplateClassDictionary)
						RegisterTemplateSpec(_MainDbAccess, kvp.Value);

					_KeepPolling = true;

					LogEvent("T4SQL Template Engine is started successfully.");
				}
				else
				{
					_KeepPolling = false;

					LogEvent("The server cannot be started due to another engine is in service!", EventLogEntryType.FailureAudit);
				}

				while (_KeepPolling)
				{
					_MainDbAccess.PrimaryPing();

					try
					{
						RefreshTemplateDefaultProperties();

						_WorkspaceTasks = _MainDbAccess.LoadWorkspaceTasks().ToList();

						foreach (Workspace ws in _WorkspaceTasks)
							ws.BuildWorkitems(_MainDbAccess, _TemplateManager, _DbServerEnv);
					}
					catch (Exception e)
					{
						LogEvent(e.Message, EventLogEntryType.Error, e.Source);
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

		[ConditionalAttribute("DEBUG")]
		public void WriteDebug(string message)
		{
			if (_ServiceEventLog == null)
				Console.WriteLine(message);
			else
				Debug.WriteLine(message);
		}

		protected void LogEvent(string message, EventLogEntryType eventType = EventLogEntryType.Information, string errorSource = null)
		{
			if (errorSource == null)
			{
				if (_ServiceEventLog != null)
					_ServiceEventLog.WriteEntry(message, eventType);
			}
			else
			{
				if (_ServiceEventLog != null)
					_ServiceEventLog.WriteEntry(message, eventType);

				_MainDbAccess.LogSysError(errorSource, message);
			}

			WriteDebug(message);
		}

		private void SetTemplateDefaultProperty(string templateFullName, string propertyName, string defaultValue, string linkState)
		{
			TemplateContext defaultProperties;

			if (_TemplateManager.TemplateDefaultProperties.TryGetValue(templateFullName, out defaultProperties) == false)
			{
				defaultProperties = new TemplateContext(_DbServerEnv);
				_TemplateManager.TemplateDefaultProperties.Add(templateFullName, defaultProperties);
			}

			TemplateContext.TemplateProperty templateProperty;

			if (defaultProperties.Properties.TryGetValue(propertyName, out templateProperty) == false)
				defaultProperties.Properties.Add(propertyName, new TemplateContext.TemplateProperty(defaultValue, linkState));
			else
			{
				templateProperty.StringValue = defaultValue;
				templateProperty.LinkState = linkState;
			}
		}

		private void RefreshTemplateDefaultProperties()
		{
			_TemplateManager.ClearTemplateDefaultProperties();

			_MainDbAccess.LoadDefaultProperties(row =>
				{
					SetTemplateDefaultProperty(row.Field<string>("CLASS_NAME"), row.Field<string>("PROPERTY_NAME"),
						row.Field<string>("DEFAULT_VALUE"), row.Field<string>("LINK_STATE"));
				});
		}

		private bool RegisterTemplateSpec(DbAccess dbAccess, Type templateClass)
		{
			if (templateClass.GetInterface(typeof(ITemplateProperties).FullName) == null)
				return false;
			else
			{
				ITemplateProperties templateProperties = Activator.CreateInstance(templateClass) as ITemplateProperties;

				if (templateProperties == null)
					return false;
				else
				{
					TemplateSpec templateSpec = templateProperties.GetPropertiesSpec();

					if (templateSpec == null)
						return false;

					foreach (KeyValuePair<string, TemplateSpec.TemplatePropertySpec> p in templateSpec.Properties)
						dbAccess.RegisterTemplatesSpec(templateClass.FullName, p.Key, p.Value.StringValue, p.Value.LinkState, p.Value.Description, p.Value.SortOrder);

					return true;
				}
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
