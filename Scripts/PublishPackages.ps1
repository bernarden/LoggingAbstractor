$ErrorActionPreference = "Stop"
$PackagesDir = Join-Path -Path $PSScriptRoot -ChildPath "../Artifacts/Packages"
$NugetDir = Join-Path -Path $PSScriptRoot -ChildPath "../Temp/nuget.exe"

function Main {
    $packages = Get-ChildItem -Path $PackagesDir
    Write-Host "Following packages were found:" -ForegroundColor Yellow
    Write-Host $packages -Separator "`n" 

    $result = Get-Approval-To-Execute
    switch ($result) {
        0 { 
            Write-Host "Continuing..." -ForegroundColor Yellow
            Start-Sleep -s 10
            Publish-Packages $packages 
        }
            
        1 {
            Write-Host "Exiting..." -ForegroundColor Yellow
        }
    }
}

function Get-Approval-To-Execute {
    $title = "Publish Packages"
    $message = "Are you sure you want to publish the packages listed above to nuget.org?"
    $yes = New-Object System.Management.Automation.Host.ChoiceDescription "&Yes", "Packages will be published!"
    $no = New-Object System.Management.Automation.Host.ChoiceDescription "&No", "Script execution will stop."
    $options = [System.Management.Automation.Host.ChoiceDescription[]]($yes, $no)
    return $host.ui.PromptForChoice($title, $message, $options, 1) 
}

function Publish-Packages {
    param ( [System.Array] $Packages)

    $nugetPackagesPattern = ".*\.nupkg"
    $nugetExeFile = Get-Item -Path $NugetDir   
    $nugetPackages = $Packages | Where-Object {$_.Name -match $nugetPackagesPattern}
    foreach ($nugetPackage in $nugetPackages) {
        Write-Host "Publishing package: $($nugetPackage.Name)" -ForegroundColor Yellow
        $args = @("push", $nugetPackage.FullName, "-source", "https://api.nuget.org/v3/index.json")
        & $nugetExeFile.FullName $args
    }
}

Main