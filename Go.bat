@echo off

powershell.exe -NonInteractive -Command ^
"import-module .\Tools\Psake\psake.psm1;^
invoke-psake .\Scripts\Build.ps1;^
exit !($psake.build_success);"