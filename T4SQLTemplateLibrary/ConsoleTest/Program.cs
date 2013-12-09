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

			Console.WriteLine("Press the Escape (Esc) key to quit the Template Engine at any time:");

			while (Console.ReadKey().Key != ConsoleKey.Escape) ;

			Console.WriteLine("Stopping...");
			engineMain.Stop();
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
//	Created Date:		February 20, 2013, 10:35:52 PM
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
