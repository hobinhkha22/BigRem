@echo off

"F:\Softwares-2\Devops\sonar-scanner-msbuild-4.6.2.2108-net46\SonarScanner.MSBuild.exe" begin /k:"BR"  /n:"Remember"
"C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin\amd64\MSBuild.exe" "E:\Programming\C#\BigRememberGit\BigRemember.sln" /t:Rebuild /p:Platform="Any CPU" /p:Configuration="Debug" /p:BuildProjectReferences=true
"F:\Softwares-2\Devops\sonar-scanner-msbuild-4.6.2.2108-net46\SonarScanner.MSBuild.exe" end