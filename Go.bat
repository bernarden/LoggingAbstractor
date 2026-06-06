@echo off

pwsh.exe -NonInteractive -Command ^
" ^
$ErrorActionPreference = 'Stop'; ^
$ProgressPreference = 'SilentlyContinue'; ^
if (-not (Get-Module -ListAvailable -Name psake)) { ^
    Write-Host 'Installing prerequisite module: psake' -ForegroundColor Cyan; ^
    Install-Module -Name psake -Scope CurrentUser -Confirm:$false -Force ^> $null; ^
} ^
Invoke-psake .\Scripts\Build.ps1; ^
exit !($psake.build_success); ^
"