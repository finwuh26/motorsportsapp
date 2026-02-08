@echo off
REM Build script for MotorsportsApp

echo =========================================
echo   MotorsportsApp Build Script
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

REM Display dotnet version
for /f "delims=" %%i in ('dotnet --version') do set DOTNET_VERSION=%%i
echo √ Found .NET SDK version: %DOTNET_VERSION%
echo.

REM Check if we're in the right directory
if not exist "MotorsportsApp.slnx" (
    echo X ERROR: MotorsportsApp.slnx not found!
    echo Please run this script from the repository root directory.
    echo.
    pause
    exit /b 1
)

REM Restore dependencies
echo. Restoring NuGet packages...
dotnet restore
if %errorlevel% neq 0 (
    echo.
    echo X Failed to restore packages!
    pause
    exit /b %errorlevel%
)
echo.

REM Build the solution
echo. Building MotorsportsApp in Release configuration...
dotnet build -c Release
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
echo To run the application:
echo   run.bat
echo.
echo To publish a standalone executable:
echo   publish.bat
echo.
pause
