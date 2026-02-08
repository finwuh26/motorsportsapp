#!/bin/bash
# Build script for MotorsportsApp
# Works on Linux and macOS

set -e  # Exit on any error

echo "========================================="
echo "  MotorsportsApp Build Script"
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

# Display dotnet version
echo "✓ Found .NET SDK version: $(dotnet --version)"
echo ""

# Check if we're in the right directory
if [ ! -f "MotorsportsApp.slnx" ]; then
    echo "❌ ERROR: MotorsportsApp.slnx not found!"
    echo "Please run this script from the repository root directory."
    echo ""
    exit 1
fi

# Restore dependencies
echo "📦 Restoring NuGet packages..."
dotnet restore
if [ $? -ne 0 ]; then
    echo ""
    echo "❌ Failed to restore packages!"
    exit 1
fi
echo ""

# Build the solution
echo "🔨 Building MotorsportsApp in Release configuration..."
dotnet build -c Release
if [ $? -ne 0 ]; then
    echo ""
    echo "❌ Build failed!"
    exit 1
fi

echo ""
echo "========================================="
echo "✅ Build completed successfully!"
echo "========================================="
echo ""
echo "To run the application (Windows only):"
echo "  dotnet run --project src/MotorsportsApp.Desktop -c Release"
echo ""
echo "To publish a standalone executable:"
echo "  ./publish.sh     (Linux/Mac)"
echo "  publish.bat      (Windows)"
echo ""
