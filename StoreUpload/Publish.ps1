$tenantId = $env:PUBLISH_TENANTID
$clientId = $env:PUBLISH_CLIENTID
$clientSecret = $env:PUBLISH_CLIENTSECRET | ConvertTo-SecureString -AsPlainText -Force
$appId = $env:PUBLISH_APPID

$outName = "Submission"

$configPath = $env:PUBLISH_SBCONFIGPATH
$pdpRootPath = $env:PUBLISH_PDPPATH
$imageRootPath = $env:PUBLISH_IMAGEPATH
$appxPath = $env:BUILD_PATH

$outPath = $env:PUBLISH_STOREPACKAGEPATH

Write-Host "Initializing submission package path" -ForegroundColor Cyan
if (Test-Path -Path $outPath)
{
    Remove-Item -Force -Recurse -Path $outPath
}
New-Item -Type Directory -Force -Path $outPath

Write-Host "Logging in to Dev Center" -ForegroundColor Cyan
$cred = New-Object System.Management.Automation.PSCredential $clientId, $clientSecret
Set-StoreBrokerAuthentication -TenantId $tenantId -Credential $cred

Write-Host ("Looking for appxupload at " + $appxPath) -ForegroundColor Cyan
$appxuploads = (Get-ChildItem -Path $appxPath | Where-Object Name -like "*.appxupload")

Write-Host "Creating submission package:" -ForegroundColor Cyan
New-SubmissionPackage -ConfigPath $configPath -PDPRootPath $pdpRootPath -ImagesRootPath $imageRootPath -OutPath $outPath -OutName $outName -AppxPath $appxuploads.FullName

Write-Host "Submitting package to Dev Center" -ForegroundColor Cyan
Update-ApplicationSubmission -AppId $appId -SubmissionDataPath (Join-Path $outPath ($outName + ".json")) -PackagePath (Join-Path $outPath ($outName + ".zip")) -Force -ReplacePackages -UpdateListings -TargetPublishMode Manual -AutoCommit

Write-Host "Clearing Authentication" -ForegroundColor Cyan
Clear-StoreBrokerAuthentication