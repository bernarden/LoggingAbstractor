@echo off

pwsh.exe -NonInteractive -Command ^
" ^
$ErrorActionPreference = 'Stop'; ^
$ProgressPreference = 'SilentlyContinue'; ^
[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12; ^
if (!(Get-PackageProvider -ListAvailable -Name NuGet)) { ^
    Write-Host "Installing nuget package provider."; ^
    Install-PackageProvider -Name NuGet -Scope CurrentUser -Confirm:$false -Force ^> $null; ^
}^
if (!(Get-Module -ListAvailable -Name psake)) { ^
    Write-Host "Installing psake module."; ^
    Install-Module -Name psake -Scope CurrentUser -Confirm:$false -Force ^> $null; ^
} ^
Invoke-psake .\Scripts\Build.ps1; ^
exit !($psake.build_success); ^
"