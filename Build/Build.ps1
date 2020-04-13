$projectPath = $env:BUILD_PROJECTFILE
$platform = $env:BUILD_PLATFORM
$destination = Join-Path $(Get-Location) $env:BUILD_PATH

Write-Host "Restoring packages" -ForegroundColor Cyan
msbuild $projectPath /t:restore /verbosity:minimal
if ($LASTEXITCODE -ne 0)
{
    Write-Error ("msbuild exited with code " + $LASTEXITCODE)
    exit $LASTEXITCODE
}

Write-Host "Building project" -ForegroundColor Cyan
msbuild $projectPath /verbosity:minimal /p:AppxBundlePlatforms=$platform /p:AppxPackageDir=$destination /p:AppxBundle=Always /p:UapAppxPackageBuildMode=StoreUpload /p:configuration="release"
if ($LASTEXITCODE -ne 0)
{
    Write-Error ("msbuild exited with code " + $LASTEXITCODE)
    exit $LASTEXITCODE
}
