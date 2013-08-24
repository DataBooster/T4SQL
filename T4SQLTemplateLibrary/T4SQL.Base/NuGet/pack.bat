@ECHO OFF
CD /D %~dp0
..\..\.nuget\NuGet.exe pack ..\T4SQL.Base.csproj -Prop Configuration=Release
MOVE /Y *.nupkg nupkg
