@echo off
REM Run script for MotorsportsApp

echo =========================================
echo   MotorsportsApp Run Script
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

echo Building MotorsportsApp...
dotnet build src\MotorsportsApp.Desktop -c Release
if %errorlevel% neq 0 (
    echo.
    echo X Build failed!
    pause
    exit /b %errorlevel%
)

echo.
echo =========================================
echo √ Build completed successfully!
echo =========================================
echo.
echo Executable created at:
echo   src\MotorsportsApp.Desktop\bin\Release\net8.0-windows\win-x64\MotorsportsApp.Desktop.exe
echo.
echo Starting MotorsportsApp...
echo.

REM Run the built executable
src\MotorsportsApp.Desktop\bin\Release\net8.0-windows\win-x64\MotorsportsApp.Desktop.exe
