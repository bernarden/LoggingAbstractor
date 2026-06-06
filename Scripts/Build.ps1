properties {
    $MainDirectory      = Resolve-Path ..
    $SourceDirectory    = "$MainDirectory\Source"
    $PackageDirectory   = "$MainDirectory\Artifacts\Packages"
    $SolutionFilePath   = "$SourceDirectory\Vima.LoggingAbstractor.sln"
    $VsConfiguration    = "Release"
    $TestFrameworks     = @("net8.0", "net9.0", "net10.0")
    $ProjectsToPublish  = @(
        "$SourceDirectory\Vima.LoggingAbstractor.Core\Vima.LoggingAbstractor.Core.csproj",
        "$SourceDirectory\Vima.LoggingAbstractor.AppInsights\Vima.LoggingAbstractor.AppInsights.csproj",
        "$SourceDirectory\Vima.LoggingAbstractor.Raygun\Vima.LoggingAbstractor.Raygun.csproj",
        "$SourceDirectory\Vima.LoggingAbstractor.Sentry\Vima.LoggingAbstractor.Sentry.csproj",
        "$SourceDirectory\Vima.LoggingAbstractor.Console\Vima.LoggingAbstractor.Console.csproj"
    )
}

task default -depends Clean, Restore, Build, Test, NugetPackage

task Clean {    
    if (Test-Path $PackageDirectory) {
        Remove-Item -Path "$PackageDirectory\*" -Recurse -Force -ErrorAction SilentlyContinue
    }

    exec { dotnet clean $SolutionFilePath }
    Write-Host ("-" * 70)
}

task Restore {
    exec { dotnet restore $SolutionFilePath }
    Write-Host ("-" * 70)
}

task Build {
    exec { dotnet build $SolutionFilePath -c $VsConfiguration --no-restore /p:TreatWarningsAsErrors=true }
    Write-Host ("-" * 70)
}

task Test { 
    $TestProjects = Get-ChildItem -Path $SourceDirectory -Recurse -Filter "*Tests.csproj"
    foreach ($Project in $TestProjects) {
        Write-Host "Processing Test Suite: $($Project.Name)" -ForegroundColor Green
        exec { dotnet test $Project.FullName -c $VsConfiguration --no-build }
        Write-Host ("-" * 70)
    }
}

task NugetPackage {    
    if (!(Test-Path $PackageDirectory)) {
        New-Item $PackageDirectory -Type Directory -Force > $null
    }

    foreach ($ProjectPath in $ProjectsToPublish) {
        Write-Host "Packing: $(Split-Path $ProjectPath -Leaf)" -ForegroundColor Green
        exec { 
            dotnet pack $ProjectPath `
                -c $VsConfiguration `
                --no-build `
                -o $PackageDirectory `
                /p:IncludeSource=true `
                /p:SymbolPackageFormat=snupkg 
        }
    }
    Write-Host ("-" * 70)
}