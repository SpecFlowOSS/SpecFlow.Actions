$windowsApplicationDriverPath = "https://github.com/microsoft/WinAppDriver/releases/download/v1.2.1/WindowsApplicationDriver_1.2.1.msi"

cd\

Invoke-WebRequest $windowsApplicationDriverPath -OutFile WindowsApplicationDriver_1.2.1.msi

Start-Process msiexec.exe -Wait -argumentList "/i WindowsApplicationDriver_1.2.1.msi /quiet"

Start-Process "Program Files (x86)\Windows Application Driver\WinAppDriver.exe"