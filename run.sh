#!/bin/bash
# Run script for MotorsportsApp

echo "========================================="
echo "  MotorsportsApp Run Script"
echo "========================================="
echo ""
echo "⚠️  Note: This is a Windows WPF application."
echo "It can only run on Windows operating systems."
echo ""
echo "You are running on: $(uname -s)"
echo ""

if [[ "$OSTYPE" == "linux-gnu"* ]] || [[ "$OSTYPE" == "darwin"* ]]; then
    echo "This script cannot run the WPF application on Linux/macOS."
    echo ""
    echo "To run the application:"
    echo "  1. Copy the project to a Windows machine"
    echo "  2. Run: build.bat"
    echo "  3. Run: run.bat"
    echo ""
    echo "Alternatively, you can try running in a Windows VM or using Wine (experimental)."
    echo ""
    exit 1
fi

echo "Running MotorsportsApp..."
dotnet run --project src/MotorsportsApp.Desktop -c Release
