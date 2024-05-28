$version = "1.0.13"

if (-not $(Test-Path ./Temp))
{
    New-Item -ItemType Directory ./Temp
}

if (-not $(Test-Path ./Temp/sarasa-gothic-ttc-${version}.7z))
{
    Invoke-WebRequest https://github.com/be5invis/Sarasa-Gothic/releases/download/v${version}/Sarasa-TTC-${version}.7z -OutFile ./Temp/sarasa-gothic-ttc-${version}.7z
}

if (-not $(Test-Path ./Assets/Fonts))
{
    New-Item -ItemType Directory ./Assets/Fonts
}

7z x ./Temp/sarasa-gothic-ttc-${version}.7z "-o./Assets/Fonts"
