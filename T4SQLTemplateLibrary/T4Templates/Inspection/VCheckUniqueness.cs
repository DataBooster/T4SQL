﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 10.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace T4SQL.Inspection
{
    using T4SQL;
    using System.Linq;
    using System;
    
    
    #line 1 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.tt"
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "10.0.0.0")]
    public partial class VCheckUniqueness : VCheckUniquenessBase
    {
        public virtual string TransformText()
        {
            
            #line 4 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.tt"

	if (DbmsPlatform == "SQL Server")
	{

            
            #line default
            #line hidden
            this.Write("IF OBJECT_ID(N\'");
            
            #line 1 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(ObjectView));
            
            #line default
            #line hidden
            this.Write("\', N\'V\') IS NULL\r\n\tEXECUTE (\'CREATE VIEW ");
            
            #line 2 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(ObjectView));
            
            #line default
            #line hidden
            this.Write(" AS SELECT NULL AS CREATE_OR_REPLACE\');\r\nGO\r\n\r\nALTER VIEW ");
            
            #line 5 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(ObjectView));
            
            #line default
            #line hidden
            this.Write(" AS\r\n-- This code was generated by ");
            
            #line 6 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(TemplateName));
            
            #line default
            #line hidden
            this.Write(" @ ");
            
            #line 6 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(DateTime.Now.ToString()));
            
            #line default
            #line hidden
            this.Write("\r\nSELECT\r\n\tT.*\r\n");
            
            #line 9 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.SqlServer.sql"

	if (!string.IsNullOrWhiteSpace(KeyColumns))
	{
		if (!string.IsNullOrWhiteSpace(OrderBy) && !string.IsNullOrWhiteSpace(RowNumberAlias))
		{

            
            #line default
            #line hidden
            this.Write("\t, ROW_NUMBER() OVER (PARTITION BY ");
            
            #line 15 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(KeyColumns));
            
            #line default
            #line hidden
            this.Write(" ORDER BY ");
            
            #line 15 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(OrderBy));
            
            #line default
            #line hidden
            this.Write(") AS ");
            
            #line 15 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(RowNumberAlias));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 16 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.SqlServer.sql"

		}

		if (!string.IsNullOrWhiteSpace(CountAlias))
		{

            
            #line default
            #line hidden
            this.Write("\t, COUNT(*) OVER (PARTITION BY ");
            
            #line 22 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(KeyColumns));
            
            #line default
            #line hidden
            this.Write(") AS ");
            
            #line 22 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(CountAlias));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 23 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.SqlServer.sql"

		}
	}

            
            #line default
            #line hidden
            this.Write("FROM\r\n\t");
            
            #line 28 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(SourceTable));
            
            #line default
            #line hidden
            this.Write("\tT\r\n");
            
            #line 29 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(SourceFilter.InsertRight("WHERE\r\n	")));
            
            #line default
            #line hidden
            this.Write(@";
GO

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		January 13, 2014, 11:00:42 PM
--	Primary Host:		http://t4sql.codeplex.com
--	Change Log:
--	Author				Date			Comment
--
--
--
--
--	(Keep code clean)
--
----------------------------------------------------------------------------------------------------
");
            this.Write("\r\n");
            
            #line 9 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.tt"

	} else
	if (DbmsPlatform == "Oracle")
	{

            
            #line default
            #line hidden
            this.Write("CREATE OR REPLACE VIEW ");
            
            #line 1 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.Oracle.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(ObjectView));
            
            #line default
            #line hidden
            this.Write(" AS\r\nSELECT\r\n-- This code was generated by ");
            
            #line 3 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.Oracle.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(TemplateName));
            
            #line default
            #line hidden
            this.Write(" @ ");
            
            #line 3 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.Oracle.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(DateTime.Now.ToString()));
            
            #line default
            #line hidden
            this.Write("\r\n\tT.*\r\n");
            
            #line 5 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.Oracle.sql"

	if (!string.IsNullOrWhiteSpace(KeyColumns))
	{
		if (!string.IsNullOrWhiteSpace(OrderBy) && !string.IsNullOrWhiteSpace(RowNumberAlias))
		{

            
            #line default
            #line hidden
            this.Write("\t, ROW_NUMBER() OVER (PARTITION BY ");
            
            #line 11 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.Oracle.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(KeyColumns));
            
            #line default
            #line hidden
            this.Write(" ORDER BY ");
            
            #line 11 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.Oracle.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(OrderBy));
            
            #line default
            #line hidden
            this.Write(") AS ");
            
            #line 11 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.Oracle.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(RowNumberAlias));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 12 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.Oracle.sql"

		}

		if (!string.IsNullOrWhiteSpace(CountAlias))
		{

            
            #line default
            #line hidden
            this.Write("\t, COUNT(*) OVER (PARTITION BY ");
            
            #line 18 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.Oracle.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(KeyColumns));
            
            #line default
            #line hidden
            this.Write(") AS ");
            
            #line 18 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.Oracle.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(CountAlias));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 19 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.Oracle.sql"

		}
	}

            
            #line default
            #line hidden
            
            #line 23 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.Oracle.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(RowidAlias.InsertRight("	, T.ROWID AS ")));
            
            #line default
            #line hidden
            this.Write("\r\nFROM\r\n\t");
            
            #line 25 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.Oracle.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(SourceTable));
            
            #line default
            #line hidden
            this.Write("\tT\r\n");
            
            #line 26 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.Oracle.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(SourceFilter.InsertRight("WHERE\r\n	")));
            
            #line default
            #line hidden
            this.Write(@"
WITH READ ONLY;

----------------------------------------------------------------------------------------------------
--
--	Copyright 2013 Abel Cheng
--	This source code is subject to terms and conditions of the Apache License, Version 2.0.
--	See http://www.apache.org/licenses/LICENSE-2.0.
--	All other rights reserved.
--	You must not remove this notice, or any other, from this software.
--
--	Original Author:	Abel Cheng <abelcys@gmail.com>
--	Created Date:		January 13, 2014, 11:00:42 PM
--	Primary Host:		http://t4sql.codeplex.com
--	Change Log:
--	Author				Date			Comment
--
--
--
--
--	(Keep code clean)
--
----------------------------------------------------------------------------------------------------
");
            this.Write("\r\n");
            
            #line 15 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.tt"

	} else
	{
		Error("T4SQL.Inspection.VCheckUniqueness is not implemented for " + DbmsPlatform);
	}

            
            #line default
            #line hidden
            
            #line 21 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Inspection\VCheckUniqueness.tt"

	/*
		Before building the project or checking in code, if any included file changes,
		you should re-transform the including template in the solution by:

		*	Right-click one or more files in Solution Explorer and then click Run Custom Tool.
			Use this method to transform a selected set of templates.
		or
		*	Click Transform All Templates in the Solution Explorer toolbar.
			This will transform all the templates in the Visual Studio solution.
		or
		*	Installed Visual Studio Visualization and Modeling SDK
			http://archive.msdn.microsoft.com/vsvmsdk,
			And setup all the templates transformed automatically:
			http://msdn.microsoft.com/en-us/library/dd820620.aspx#Regenerating
			http://msdn.microsoft.com/en-us/library/ee847423.aspx
			http://msdn.microsoft.com/en-us/library/ff521399.aspx
	*/

            
            #line default
            #line hidden
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "10.0.0.0")]
    public class VCheckUniquenessBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
