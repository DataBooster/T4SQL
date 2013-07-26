Installation Notes:

[T4SQL Template] - Template of Templates (for SQL Server and Oracle)
should have been installed in your Visual Studio Custom Item Templates.
Please make sure the [T4SQL Template] is in the Add New Item dialog box, Installed Templates, under Visual C# Items.

In case the automatic installation fail due to insufficient permissions or other issue,
you may need to manually install it by following instructions:

Copy the file T4SQLTemplate.CSharp.zip
	from the project NuGet package directory (packages\T4SQL.Base.x.x.x.x\Visual Studio\Templates\ItemTemplates\Visual C#\)
	to your Visual Studio Item Templates Directory
		(Find Visual Studio [Tools] menu, [Options] dialog box, under [Projects and Solutions]\[General], in [user item templates location])


See Also:
http://msdn.microsoft.com/en-us/library/y3kkate1.aspx (How to: Locate and Organize Project and Item Templates)
http://msdn.microsoft.com/en-us/library/e1x5ayd4.aspx (How to: Share Templates Across a Development Team)
http://t4sql.codeplex.com/ (T4SQL Template Library)
