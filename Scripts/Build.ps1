properties {

  $MainDirectory = Resolve-Path ..
  $SourceDirectory = "$MainDirectory\Source"
  $TempDirectory =  "$MainDirectory\Temp"
  $ArtifactsDirectory =  "$MainDirectory\Artifacts"
  $PackageDirectory = "$ArtifactsDirectory\Packages"
  $SolutionFilePath =  "$SourceDirectory\Vima.LoggingAbstractor.sln"
  $VsConfiguration = "Release"

  $NugetFilePath = "$TempDirectory\nuget.exe"
  $NugetFileUrl = "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe"

  $NugetPackages = @(
    @{"Name" = "vswhere"; "Version" = "2.3.2"; "Alias" = "VSWhere"; "ExePath" = "tools/vswhere.exe"},
    @{"Name" = "NUnit.ConsoleRunner"; "Version" = "3.8.0"; "Alias" = "NUnitConsole"; "ExePath" = "tools/nunit3-console.exe" }
    )

  $TestsConfig=@(
    @{"Framework" = "netcoreapp1.0"; "TestingFramework" = "netcoreapp1.0"; "Utility" = "DotnetTest";},
    @{"Framework" = "netcoreapp2.0"; "TestingFramework" = "netcoreapp2.0"; "Utility" = "DotnetTest";},
    @{"Framework" = "net20"; "TestingFramework" = "net-2.0"; "Utility" = "NUnit";},  
    @{"Framework" = "net35"; "TestingFramework" = "net-3.5"; "Utility" = "NUnit";},
    @{"Framework" = "net40"; "TestingFramework" = "net-4.0"; "Utility" = "NUnit";},
    @{"Framework" = "net45"; "TestingFramework" = "net-4.5"; "Utility" = "NUnit";},
    @{"Framework" = "net451"; "TestingFramework" = "net-4.0"; "Utility" = "NUnit";},
    @{"Framework" = "net452"; "TestingFramework" = "net-4.5"; "Utility" = "NUnit";},   
    @{"Framework" = "net46"; "TestingFramework" = "net46"; "Utility" = "DotnetTest";},
    @{"Framework" = "net47"; "TestingFramework" = "net47"; "Utility" = "DotnetTest";}
    )

  $ProjectsToPublish=@(
      "$SourceDirectory\Vima.LoggingAbstractor.Core\Vima.LoggingAbstractor.Core.csproj",
      "$SourceDirectory\Vima.LoggingAbstractor.AppInsights\Vima.LoggingAbstractor.AppInsights.csproj",
      "$SourceDirectory\Vima.LoggingAbstractor.Raygun\Vima.LoggingAbstractor.Raygun.csproj",
      "$SourceDirectory\Vima.LoggingAbstractor.Sentry\Vima.LoggingAbstractor.Sentry.csproj"
    )
}

task default -depends PrepareTools, Restore, Clean, Build, Test, NugetPackage

task PrepareTools {
  if (!(Test-Path $TempDirectory))
  {
    New-Item $TempDirectory -type directory > $null
  }

  DownloadNugetFile

  DownloadNugetPackagesAndSetAliases
  
  SetMSBuildAlias
}

task Clean {
  exec { MSBuild "/t:clean" "/p:Configuration=$VsConfiguration" $SolutionFilePath /m}
}

task Restore {
   exec { MSBuild "/t:restore" "/p:Configuration=$VsConfiguration" $SolutionFilePath /m}
}

task Build {
  exec { MSBuild "/t:build" "/p:Configuration=$VsConfiguration" $SolutionFilePath /m "/p:TreatWarningsAsErrors=`"true`""}
}

task Test { 
  Push-Location $SourceDirectory
  $TestDlls = Get-ChildItem "*\bin\$VsConfiguration\*" -Recurse | Where-Object {$_.Name -match ".*tests?\.dll"}
  foreach ($TestDll in $TestDlls) {
    Push-Location $($TestDll.DirectoryName)
    $TestConfig = $TestsConfig | Where-Object {$_.Framework -eq $TestDll.Directory.Name}
    if ($TestConfig.Utility -eq "NUnit") {
      exec { NUnitConsole  $TestDll.Name --framework $TestConfig.TestingFramework}
    }else {
      $ProjectFilePath = Get-ChildItem "$($TestDll.Directory.Parent.Parent.Parent.FullName)\*" | Where-Object {$_.Name -match ".*tests?\.csproj"} | Select-Object -First 1
      exec { dotnet test $ProjectFilePath -f "$($TestConfig.TestingFramework)" -c $VsConfiguration --no-build }
    }
    Pop-Location
    Write-Host ("-" * 70)
  }
  Pop-Location
}

task NugetPackage{
  Foreach ($projectPath in $ProjectsToPublish)
  {
    exec { MSBuild "/t:pack" "/p:IncludeSource=true" "/p:Configuration=$VsConfiguration" $projectPath "/p:PackageOutputPath=$PackageDirectory" }
  }
}

function DownloadNugetFile()
{
  if (!(Test-Path $NugetFilePath))
  {
    Write-Host "Downloading Nuget file." -foregroundcolor Green
    (New-Object System.Net.WebClient).DownloadFile($NugetFileUrl, $NugetFilePath)
  }
}

function DownloadNugetPackagesAndSetAliases()
{
  foreach ($Package in $NugetPackages)
  {
    $PackageFolder = "$TempDirectory\$($Package.Name).$($Package.Version)"
    if (!(Test-Path $PackageFolder))
    {
      Write-Host ("-" * 70)
      Write-Host "Downloading $($Package.Name) package." -foregroundcolor Green
      exec { & $NugetFilePath install $Package.Name -OutputDirectory $TempDirectory -Version $Package.Version }
    }
    if($Package.Alias -and $Package.ExePath -and (Test-Path "$PackageFolder\$($Package.ExePath)") ){
        Set-Alias "$($Package.Alias)" "$PackageFolder\$($Package.ExePath)" -Scope Script
    }
  }
}

function SetMSBuildAlias()
{
  $path = VSWhere -latest -products * -requires Microsoft.Component.MSBuild -property installationPath
  if ($path) {
    $path = join-path $path 'MSBuild\15.0\Bin\MSBuild.exe'
    Set-Alias MSBuild $path -Scope Script
  }else {
      throw "MSBuild is not found. Please install VS2017 or later."
  }
}

