if (-not $(Test-Path ./Temp))
{
    New-Item -ItemType Directory ./Temp
}

if (-not $(Test-Path ./Temp/sarasa-gothic-ttc.7z))
{
    Invoke-WebRequest https://github.com/be5invis/Sarasa-Gothic/releases/download/v0.10.2/sarasa-gothic-ttc-0.10.2.7z -OutFile ./Temp/sarasa-gothic-ttc.7z
}

if (-not $(Test-Path ./Assets/Fonts))
{
    New-Item -ItemType Directory ./Assets/Fonts
}

7z x ./Temp/sarasa-gothic-ttc.7z "-o./Assets/Fonts"