$tenantId = "05fd4458-757f-4d64-8c5d-ca5617532083"
$clientId = "46a5301d-5df3-488d-9c8c-7b8d2732e3ab"
$appxPath = Join-Path $PSScriptRoot "../SarasaGothic/AppPackages" -Resolve
$appId = "9MW0M424NCZ7"

$submissionPackagePath = Join-Path $PSScriptRoot "../StoreBroker/StorePackage"
$submissionPackageName = "Submission"
$clientSecret = $env:PUBLISH_CLIENTSECRET | ConvertTo-SecureString -AsPlainText -Force
$configPath = Join-Path $PSScriptRoot "../StoreBroker/SBConfig.json" -Resolve
$pdpRootPath = Join-Path $PSScriptRoot "../StoreBroker/PDP" -Resolve
$imageRootPath = Join-Path $PSScriptRoot "../StoreBroker/Images" -Resolve


Write-Host "Initializing submission package path" -ForegroundColor Cyan
if (Test-Path -Path $submissionPackagePath) {
    Remove-Item -Force -Recurse -Path $submissionPackagePath
}
New-Item -Type Directory -Force -Path $submissionPackagePath

Write-Host "Logging in to Dev Center" -ForegroundColor Cyan
$cred = New-Object System.Management.Automation.PSCredential $clientId, $clientSecret
Set-StoreBrokerAuthentication -TenantId $tenantId -Credential $cred

Write-Host ("Looking for appxupload at " + $appxPath) -ForegroundColor Cyan
$appxuploads = Get-ChildItem -Path $appxPath | Where-Object { ".appxupload", ".msixupload" -contains $_.Extension }

Write-Host "Creating submission package:" -ForegroundColor Cyan
New-SubmissionPackage -ConfigPath $configPath -PDPRootPath $pdpRootPath -ImagesRootPath $imageRootPath -OutPath $submissionPackagePath -OutName $submissionPackageName -AppxPath $appxuploads.FullName

Write-Host "Submitting package to Dev Center" -ForegroundColor Cyan
Update-ApplicationSubmission -AppId $appId -SubmissionDataPath (Join-Path $submissionPackagePath "${submissionPackageName}.json" -Resolve) -PackagePath (Join-Path $submissionPackagePath ($submissionPackageName + ".zip")) -Force -ReplacePackages -UpdateListings -TargetPublishMode Manual -AutoCommit

Write-Host "Clearing Authentication" -ForegroundColor Cyan
Clear-StoreBrokerAuthentication