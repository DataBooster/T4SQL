using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T4SQL.SqlBuilder
{
	internal class Workitem
	{
		private string _Workitem_Name;
		public string Workitem_Name
		{
			get { return _Workitem_Name; }
			set { _Workitem_Name = value; }
		}

		private string _Template_Name;
		public string Template_Name
		{
			get { return _Template_Name; }
			set { _Template_Name = value; }
		}

		private TemplateContext _WorkingProperties;
		public TemplateContext WorkingProperties
		{
			get { return _WorkingProperties; }
			set { _WorkingProperties = value; }
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
//	Created Date:		‎‎April ‎24, ‎2013, ‏‎11:12:37 AM
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
