/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

:r .\1-CreateSampleWorkspace.sql
:r .\2-GenerateTestData.sql
:r .\3-SetupWorkitems.sql
:r .\4-SetupWorkitemsProperties.sql
:r .\5-PreviewWorkitems.sql
