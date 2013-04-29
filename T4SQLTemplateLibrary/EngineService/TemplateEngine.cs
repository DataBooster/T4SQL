using System.ServiceProcess;
using T4SQL.SqlBuilder;

namespace T4SQL.EngineService
{
	public partial class TemplateEngine : ServiceBase
	{
		private EngineMain _engineMain;

		public TemplateEngine()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			_engineMain = new EngineMain();

			_engineMain.Start();
		}

		protected override void OnStop()
		{
			_engineMain.Stop();
		}

		protected override void OnShutdown()
		{
			OnStop();
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
//	Created Date:		‎February ‎20, ‎2013, ‏‎10:33:17 PM
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
