# SarasaGothicAppx
This project is an APPX wrapper of the font [Sarasa-Gothic](https://github.com/be5invis/Sarasa-Gothic) for Windows 10 devices.

[![Build status](https://ci.appveyor.com/api/projects/status/o3ok8j0kvovh5r7g?svg=true)](https://ci.appveyor.com/project/taoyouh/sarasagothicappx)

The project is built and released to Microsoft Store autonomously on AppVeyor.

## Install
<a href='//www.microsoft.com/store/productId/9MW0M424NCZ7?cid=storebadge&ocid=badge'><img src='https://assets.windowsphone.com/85864462-9c82-451e-9355-a3d5f874397a/English_get-it-from-MS_InvariantCulture_Default.png' alt='English badge' style='width: 284px; height: 104px;' width='284px' height='104px'/></a>

## Build
1. Execute the PowerShell script SarasaGothic/DownloadContents.ps1 in directory SarasaGothic (with SarasaGothic.csproj).
2. Open and build SarasaGothic.sln in Visual Studio.

Or, in Developer PowerShell for VS:
```
Push-Location .\SarasaGothic
.\DownloadContents.ps1
Pop-Location

.\Build\Config.ps1
.\Build\Build.ps1
```