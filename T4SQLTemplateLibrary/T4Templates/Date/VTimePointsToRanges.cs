﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 10.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace T4SQL.Date
{
    using T4SQL;
    using System.Linq;
    using System;
    
    
    #line 1 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.tt"
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "10.0.0.0")]
    public partial class VTimePointsToRanges : VTimePointsToRangesBase
    {
        public virtual string TransformText()
        {
            
            #line 4 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.tt"

	if (DbmsPlatform == "SQL Server")
	{

            
            #line default
            #line hidden
            this.Write("IF OBJECT_ID(N\'");
            
            #line 1 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(ObjectView));
            
            #line default
            #line hidden
            this.Write("\', N\'V\') IS NULL\r\n\tEXECUTE (\'CREATE VIEW ");
            
            #line 2 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(ObjectView));
            
            #line default
            #line hidden
            this.Write(" AS SELECT NULL AS CREATE_OR_REPLACE\');\r\nGO\r\n\r\nALTER VIEW ");
            
            #line 5 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(ObjectView));
            
            #line default
            #line hidden
            this.Write(" AS\r\n-- This code was generated by ");
            
            #line 6 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(TemplateName));
            
            #line default
            #line hidden
            this.Write(" @ ");
            
            #line 6 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(DateTime.Now.ToString()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 7 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.SqlServer.sql"

	string tEndDateCol = IsEndDateNext ? "t2." + DateColumn : string.Format("DATEADD(day, -1, t2.{0})", DateColumn);

	if (DbmsVersion > new Version(11, 0))	// SQL Server 2012
	{

            
            #line default
            #line hidden
            this.Write("SELECT\r\n\t");
            
            #line 14 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(SelectColumns.InsertLeft()));
            
            #line default
            #line hidden
            this.Write("\r\n\t");
            
            #line 15 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(DateColumn));
            
            #line default
            #line hidden
            this.Write("\t\tAS ");
            
            #line 15 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(RangeStartDateColumn));
            
            #line default
            #line hidden
            this.Write(",\r\n\tLEAD(");
            
            #line 16 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(tEndDateCol));
            
            #line default
            #line hidden
            this.Write(", 1");
            
            #line 16 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(DefaultEndDate.IsNullString() ? "" : ", " + DefaultEndDate));
            
            #line default
            #line hidden
            this.Write(") OVER (PARTITION BY ");
            
            #line 16 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(Key_Columns));
            
            #line default
            #line hidden
            this.Write(" ORDER BY ");
            
            #line 16 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(DateColumn));
            
            #line default
            #line hidden
            this.Write(")\r\n\t\t\t\t\t\t\t\tAS ");
            
            #line 17 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(RangeEndDateColumn));
            
            #line default
            #line hidden
            this.Write("\r\nFROM\r\n\t");
            
            #line 19 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(SourceView));
            
            #line default
            #line hidden
            this.Write("\tt2\r\n");
            
            #line 20 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.SqlServer.sql"

	}
	else									// SQL Server 2008, 2005
	{
		if (!DefaultEndDate.IsNullString())
			tEndDateCol = string.Format("ISNULL({0}, {1})", tEndDateCol, DefaultEndDate);

            
            #line default
            #line hidden
            this.Write("WITH TR AS\r\n(\r\n\tSELECT\r\n\t\t*,\r\n\t\tROW_NUMBER() OVER (PARTITION BY ");
            
            #line 31 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(Key_Columns));
            
            #line default
            #line hidden
            this.Write(" ORDER BY ");
            
            #line 31 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(DateColumn));
            
            #line default
            #line hidden
            this.Write(") AS ROW$NUMBER\r\n\tFROM\r\n\t\t");
            
            #line 33 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(SourceView));
            
            #line default
            #line hidden
            this.Write("\r\n)\r\nSELECT\r\n\t");
            
            #line 36 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(SelectColumns.Select(c => "t1." + c).InsertLeft()));
            
            #line default
            #line hidden
            this.Write("\r\n\tt1.");
            
            #line 37 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(DateColumn));
            
            #line default
            #line hidden
            this.Write("\t\tAS ");
            
            #line 37 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(RangeStartDateColumn));
            
            #line default
            #line hidden
            this.Write(",\r\n\t");
            
            #line 38 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(tEndDateCol));
            
            #line default
            #line hidden
            this.Write("\t\t\tAS ");
            
            #line 38 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(RangeEndDateColumn));
            
            #line default
            #line hidden
            this.Write("\r\nFROM\r\n\tTR t1 LEFT OUTER JOIN\r\n\tTR t2 ON (");
            
            #line 41 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.SqlServer.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(String.Join(" AND ", KeyColumns.Select(k => string.Format(@"t1.{0} = t2.{0}", k)))));
            
            #line default
            #line hidden
            this.Write("\r\n\t\tAND t1.ROW$NUMBER + 1 = t2.ROW$NUMBER)\r\n");
            
            #line 43 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.SqlServer.sql"

	}

            
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
--	Created Date:		‎May ‎20, ‎2013, ‏‎12:00:44 AM
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
            
            #line 9 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.tt"

	} else
	if (DbmsPlatform == "Oracle")
	{

            
            #line default
            #line hidden
            this.Write("CREATE OR REPLACE VIEW ");
            
            #line 1 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.Oracle.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(ObjectView));
            
            #line default
            #line hidden
            this.Write(" AS\r\nSELECT\r\n-- This code was generated by ");
            
            #line 3 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.Oracle.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(TemplateName));
            
            #line default
            #line hidden
            this.Write(" @ ");
            
            #line 3 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.Oracle.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(DateTime.Now.ToString()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 4 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.Oracle.sql"

	string tEndDateCol = IsEndDateNext ? DateColumn : string.Format("({0} - 1)", DateColumn);

            
            #line default
            #line hidden
            this.Write("\t");
            
            #line 7 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.Oracle.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(SelectColumns.InsertLeft()));
            
            #line default
            #line hidden
            this.Write("\r\n\t");
            
            #line 8 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.Oracle.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(DateColumn));
            
            #line default
            #line hidden
            this.Write("\t\tAS ");
            
            #line 8 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.Oracle.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(RangeStartDateColumn));
            
            #line default
            #line hidden
            this.Write(",\r\n\tLEAD(");
            
            #line 9 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.Oracle.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(tEndDateCol));
            
            #line default
            #line hidden
            this.Write(", 1");
            
            #line 9 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.Oracle.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(DefaultEndDate.IsNullString() ? "" : ", " + DefaultEndDate));
            
            #line default
            #line hidden
            this.Write(") OVER (PARTITION BY ");
            
            #line 9 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.Oracle.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(Key_Columns));
            
            #line default
            #line hidden
            this.Write(" ORDER BY ");
            
            #line 9 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.Oracle.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(DateColumn));
            
            #line default
            #line hidden
            this.Write(")\r\n\t\t\t\t\t\t\t\tAS ");
            
            #line 10 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.Oracle.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(RangeEndDateColumn));
            
            #line default
            #line hidden
            this.Write("\r\nFROM\r\n\t");
            
            #line 12 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.Oracle.sql"
            this.Write(this.ToStringHelper.ToStringWithCulture(SourceView));
            
            #line default
            #line hidden
            this.Write("\r\n\r\nWITH READ ONLY;\r\n");
            this.Write("\r\n");
            
            #line 15 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.tt"

	} else
	{
		Error("T4SQL.Date.VTimePointsToRanges is not implemented for " + DbmsPlatform);
	}

            
            #line default
            #line hidden
            
            #line 21 "E:\Projects\T4SQL\T4SQLTemplateLibrary\T4Templates\Date\VTimePointsToRanges.tt"

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
    public class VTimePointsToRangesBase
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
