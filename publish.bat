@echo off
REM Publish script for MotorsportsApp

echo =========================================
echo   MotorsportsApp Publish Script
echo =========================================
echo.

REM Check if dotnet is installed
where dotnet >nul 2>&1
if %errorlevel% neq 0 (
    echo X ERROR: .NET SDK is not installed!
    echo.
    echo Please install .NET 8 SDK from:
    echo   https://dotnet.microsoft.com/download/dotnet/8.0
    echo.
    pause
    exit /b 1
)

REM Check if we're in the right directory
if not exist "MotorsportsApp.slnx" (
    echo X ERROR: MotorsportsApp.slnx not found!
    echo Please run this script from the repository root directory.
    echo.
    pause
    exit /b 1
)

echo Publishing MotorsportsApp for Windows x64...
echo This creates a standalone executable that can run without .NET installed.
echo.

dotnet publish src\MotorsportsApp.Desktop -c Release -r win-x64 --self-contained -o publish\win-x64

if %errorlevel% neq 0 (
    echo.
    echo X Publish failed!
    pause
    exit /b %errorlevel%
)

echo.
echo =========================================
echo √ Publish completed successfully!
echo =========================================
echo.
echo Your standalone application is ready!
echo.
echo Output directory: publish\win-x64
echo Executable file:  publish\win-x64\MotorsportsApp.Desktop.exe
echo.
echo You can now:
echo   1. Run the exe directly from: publish\win-x64\MotorsportsApp.Desktop.exe
echo   2. Copy the entire publish\win-x64 folder to another Windows PC
echo   3. The application will run without needing .NET installed
echo.
pause
