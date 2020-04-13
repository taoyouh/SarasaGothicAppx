$tenantId = $env:PUBLISH_TENANTID
$clientId = $env:PUBLISH_CLIENTID
$clientSecret = $env:PUBLISH_CLIENTSECRET | ConvertTo-SecureString -AsPlainText -Force
$appId = $env:PUBLISH_APPID
$pdpFileName = $env:PUBLISH_PDPNAME
$pdpPath = $env:PUBLISH_PDPPATH
$sbConfigPath = $env:PUBLISH_SBCONFIGPATH

Write-Host "Logging in to Dev Center" -ForegroundColor Cyan
$cred = New-Object System.Management.Automation.PSCredential $clientId, $clientSecret
Set-StoreBrokerAuthentication -TenantId $tenantId -Credential $cred

Write-Host "Getting PDP" -ForegroundColor Cyan
$converter = (Join-Path -Path ([System.Environment]::GetFolderPath('MyDocuments')) -ChildPath 'WindowsPowerShell\Modules\StoreBroker\StoreBroker\Extensions\ConvertFrom-ExistingSubmission.ps1')
& $converter -AppId $appId -Release 2004 -PdpFileName $pdpFileName -OutPath $pdpPath

Write-Host "Creating Configs"
New-StoreBrokerConfigFile -Path $sbConfigPath -AppId $appId

Clear-StoreBrokerAuthentication