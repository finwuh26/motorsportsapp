@echo off
REM Publish script for MotorsportsApp

echo Publishing MotorsportsApp for Windows x64...
dotnet publish src\MotorsportsApp.Desktop -c Release -r win-x64 --self-contained -o publish\win-x64

if %errorlevel% neq 0 (
    echo Publish failed!
    exit /b %errorlevel%
)

echo.
echo Publish completed successfully!
echo Output directory: publish\win-x64
echo Executable: publish\win-x64\MotorsportsApp.Desktop.exe
