using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Configuration;
using System.Threading;
using T4SQL.SqlBuilder;

namespace T4SQL.ConsoleTest
{
	class Program
	{
		static void Main(string[] args)
		{
			EngineMain engineMain = new EngineMain();
			engineMain.Start();
			Thread.Sleep(10000);
			engineMain.Stop();
		}
	}
}
