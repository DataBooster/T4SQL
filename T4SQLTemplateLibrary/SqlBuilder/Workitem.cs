using System;
using System.Text;
using System.CodeDom.Compiler;

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

		private Type _TemplateClass;
		public Type TemplateClass
		{
			get { return _TemplateClass; }
			set { _TemplateClass = value; }
		}

		public void Compile()
		{
			const int maxErrorLength = 2000;

			if (_TemplateClass == null)
			{
				_Compiled_Error = "Invalid Template Class full name!";
				return;
			}

			try
			{
				ITemplate template = Activator.CreateInstance(_TemplateClass) as ITemplate;

				template.Context = _WorkingProperties;
				_Object_Code = template.TransformText();

				if (template.Errors.HasErrors)
				{
					StringBuilder errors = new StringBuilder();

					foreach (CompilerError err in template.Errors)
					{
						if (string.IsNullOrWhiteSpace(err.ErrorText))
							continue;

						if (errors.Length > 0)
							errors.AppendLine();

						errors.Append(err.ErrorText);
					}

					if (errors.Length > 0)
						_Compiled_Error = errors.ToString().Left(maxErrorLength);
				}
			}
			catch (Exception e)
			{
				_Compiled_Error = e.Message.Left(maxErrorLength);
			}
		}

		private string _Compiled_Error;
		public string Compiled_Error { get { return _Compiled_Error; } }

		private string _Object_Code;
		public string Object_Code { get { return _Object_Code; } }
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
//	Created Date:		April 24, 2013, 11:12:37 AM
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
