@echo off
REM Build script for MotorsportsApp

echo Building MotorsportsApp...
dotnet build -c Release

if %errorlevel% neq 0 (
    echo Build failed!
    exit /b %errorlevel%
)

echo Build completed successfully!
