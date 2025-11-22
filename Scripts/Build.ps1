$projectPath = Join-Path $PSScriptRoot "../SarasaGothic.sln" -Resolve

if ($null -eq $platform) {
    $platform = "x86|x64|ARM|ARM64"
}
$firstPlatform = $platform.Split("|")[0]

Write-Host "Restoring packages" -ForegroundColor Cyan
msbuild $projectPath /t:restore /verbosity:minimal $extraParams
if ($LASTEXITCODE -ne 0) {
    throw ("msbuild exited with code " + $LASTEXITCODE)
}

Write-Host "Building project" -ForegroundColor Cyan
msbuild $projectPath /verbosity:minimal /p:AppxBundlePlatforms=$platform /p:platform=$firstPlatform /p:AppxBundle=Always /p:UapAppxPackageBuildMode=StoreUpload /p:configuration="release" $extraParams
if ($LASTEXITCODE -ne 0) {
    throw ("msbuild exited with code " + $LASTEXITCODE)
}
