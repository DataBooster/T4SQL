using System;
using T4SQL.SqlBuilder;

namespace T4SQL.ConsoleTest
{
	class Program
	{
		static void Main(string[] args)
		{
			EngineMain engineMain = new EngineMain();

			engineMain.Start();

			engineMain.WriteDebug("Press the Escape (Esc) key to quit the Template Engine at any time:");

			while (Console.ReadKey().Key != ConsoleKey.Escape) ;

			engineMain.Stop();
		}
	}
}
