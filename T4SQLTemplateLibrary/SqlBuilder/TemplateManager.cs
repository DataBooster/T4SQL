using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace T4SQL.SqlBuilder
{
	public class TemplateManager
	{
		private readonly Type _typeITemplate;
		private readonly Dictionary<string, Type> _templateClassDictionary;
		private readonly Dictionary<string, TemplateContext> _templateDefaultProperties;

		public Dictionary<string, Type> TemplateClassDictionary
		{
			get { return _templateClassDictionary; }
		}

		public Dictionary<string, TemplateContext> TemplateDefaultProperties
		{
			get { return _templateDefaultProperties; }
		}

		public TemplateManager()
		{
			_typeITemplate = typeof(ITemplate);
			_templateClassDictionary = new Dictionary<string, Type>();
			_templateDefaultProperties = new Dictionary<string, TemplateContext>();
		}

		protected bool RegisterTemplate(Type template)
		{
			if (template.IsClass == false || template.GetInterface(_typeITemplate.Name) == null)
				return false;
			else
				if (_templateClassDictionary.ContainsKey(template.FullName))
					return false;
				else
				{
					_templateClassDictionary.Add(template.FullName, template);
					return true;
				}
		}

		protected int RegisterTemplate(Assembly addInAssembly)
		{
			int cnt = 0;

			foreach (Type t in addInAssembly.GetExportedTypes())
				if (RegisterTemplate(t))
					cnt++;

			return cnt;
		}

		public void LoadAddIns()
		{
			Type typeAddInsLocator = typeof(AddInsLocator);
			string builtInDll = typeAddInsLocator.Module.FullyQualifiedName;
			string addInDir = Path.GetDirectoryName(builtInDll);

			RegisterTemplate(typeAddInsLocator.Assembly);									// Register built-in Templates

			if (addInDir.StartsWith(AppDomain.CurrentDomain.BaseDirectory))
				foreach (string dllFile in Directory.GetFiles(addInDir, "*.dll", SearchOption.AllDirectories))
					if (!dllFile.Equals(builtInDll, StringComparison.OrdinalIgnoreCase))	// Register add-in Templates
					{
						try
						{
							RegisterTemplate(Assembly.LoadFrom(dllFile));
						}
						catch
						{
						}
					}
		}

		public string GenerateCode(string templateFullName, TemplateContext templateContext)
		{
			Type templateType;

			if (_templateClassDictionary.TryGetValue(templateFullName, out templateType))
			{
				ITemplate template = (ITemplate)Activator.CreateInstance(templateType);

				template.Context = templateContext;

				return template.TransformText();
			}
			else
				return null;
		}

		internal void ClearTemplateDefaultProperties()
		{
			foreach (KeyValuePair<string, TemplateContext> kvp in _templateDefaultProperties)
				kvp.Value.Clear();
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
//	Created Date:		March 10, 2013, 11:51:50 PM
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
