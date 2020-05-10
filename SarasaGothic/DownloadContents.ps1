if (-not $(Test-Path ./Temp))
{
    New-Item -ItemType Directory ./Temp
}

if (-not $(Test-Path ./Temp/sarasa-gothic-ttc-0.12.6.7z))
{
    Invoke-WebRequest https://github.com/be5invis/Sarasa-Gothic/releases/download/v0.12.6/sarasa-gothic-ttc-0.12.6.7z -OutFile ./Temp/sarasa-gothic-ttc-0.12.6.7z
}

if (-not $(Test-Path ./Assets/Fonts))
{
    New-Item -ItemType Directory ./Assets/Fonts
}

7z x ./Temp/sarasa-gothic-ttc-0.12.6.7z "-o./Assets/Fonts"