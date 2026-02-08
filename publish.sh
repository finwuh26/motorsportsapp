#!/bin/bash
# Publish script for MotorsportsApp
# Creates a standalone executable with all dependencies included

set -e  # Exit on any error

echo "========================================="
echo "  MotorsportsApp Publish Script"
echo "========================================="
echo ""

# Check if dotnet is installed
if ! command -v dotnet &> /dev/null; then
    echo "❌ ERROR: .NET SDK is not installed!"
    echo ""
    echo "Please install .NET 8 SDK from:"
    echo "  https://dotnet.microsoft.com/download/dotnet/8.0"
    echo ""
    exit 1
fi

# Check if we're in the right directory
if [ ! -f "MotorsportsApp.slnx" ]; then
    echo "❌ ERROR: MotorsportsApp.slnx not found!"
    echo "Please run this script from the repository root directory."
    echo ""
    exit 1
fi

# Determine the runtime identifier based on the OS
if [[ "$OSTYPE" == "linux-gnu"* ]]; then
    RUNTIME="linux-x64"
    echo "📦 Publishing for Linux (x64)..."
elif [[ "$OSTYPE" == "darwin"* ]]; then
    RUNTIME="osx-x64"
    echo "📦 Publishing for macOS (x64)..."
else
    RUNTIME="linux-x64"
    echo "⚠️  Unknown OS, defaulting to Linux (x64)..."
fi

echo ""
echo "Note: This is a Windows WPF application."
echo "The published output can only run on Windows."
echo "Publishing for demonstration purposes only."
echo ""

# Publish the application
dotnet publish src/MotorsportsApp.Desktop -c Release -r win-x64 --self-contained -o publish/win-x64

if [ $? -ne 0 ]; then
    echo ""
    echo "❌ Publish failed!"
    exit 1
fi

echo ""
echo "========================================="
echo "✅ Publish completed successfully!"
echo "========================================="
echo ""
echo "Output directory: publish/win-x64"
echo "Executable: publish/win-x64/MotorsportsApp.Desktop.exe"
echo ""
echo "Note: This .exe file can only run on Windows."
echo "To run it, copy the entire publish/win-x64 folder to a Windows machine."
echo ""
