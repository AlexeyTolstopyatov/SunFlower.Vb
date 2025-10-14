### Copy debug target to kernel space
# This script uses by IntelliJ Idea and Rider software
# for fast and comfortable moving of linked extensions.
#
# If you're running for a first time
# You must have
#  - Installed SunFlower components
#  - .NET 8.0+
#
# This script copies _DEBUG_ built .DLL to Sunflower root
# Edit this script for your desktop and bind it with
# JetBrains project's Start-up configuration.
# I've called this bind "Data"
#
### ---USER CHANGES SCOPE STARTS---
$framework = "D:\Locals\SunFlower\src\SunFlower\bin\Debug\net8.0\SunFlower.Abstractions.dll"
$client_debug = "D:\Locals\SunFlower\src\SunFlower.Windows\bin\Debug\net8.0-windows"
$target_debug = "D:\Locals\SunFlower.Vb\SunFlower.Vb\bin\Debug\net8.0\SunFlower.Vb.dll"
### ---USER CHANGES SCOPE ENDS---

Write-Host "Importing to $client_debug\Plugins" -ForegroundColor blue
if (Test-Path "$client_debug\Plugins") {

} else {
    New-Item -ItemType Directory "$client_debug\Plugins"
}
Copy-Item -Path $framework -Destination "$client_debug\Plugins\" -Force
Copy-Item -Path $target_debug -Destination "$client_debug\Plugins\" -Force