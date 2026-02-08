# 🏎️ MotorsportsApp - Quick Start Guide

**Complete beginner-friendly guide to build and run the F1 Live Timing application.**

---

## 📋 What You Need Before Starting

### Required Software
1. **.NET 8 SDK** - The programming framework this app uses
2. **Git** (optional) - To download the code from GitHub
3. **Windows 10 or 11** - Required to run the WPF desktop application

### Optional (for developers)
- **Visual Studio 2022** or **JetBrains Rider** - If you want to modify the code

---

## 🚀 Step-by-Step Instructions

### Step 1: Install .NET 8 SDK

1. Go to: https://dotnet.microsoft.com/download/dotnet/8.0
2. Click **Download .NET 8.0 SDK** (x64) for Windows
3. Run the installer and follow the installation wizard
4. Click "Install" and wait for it to complete

**How to verify it worked:**
1. Open Command Prompt (press `Windows Key` + `R`, type `cmd`, press Enter)
2. Type: `dotnet --version`
3. You should see something like `8.0.xxx`

If you see a version number, you're good! ✅

---

### Step 2: Get the Code

**Option A: Using Git (Recommended)**
1. Install Git from: https://git-scm.com/download/win
2. Open Command Prompt
3. Navigate to where you want the code (e.g., `cd C:\Projects`)
4. Run: `git clone https://github.com/finwuh26/motorsportsapp.git`
5. Run: `cd motorsportsapp`

**Option B: Download ZIP**
1. Go to: https://github.com/finwuh26/motorsportsapp
2. Click the green **Code** button
3. Click **Download ZIP**
4. Extract the ZIP file to a folder (e.g., `C:\Projects\motorsportsapp`)
5. Open Command Prompt and navigate to that folder: `cd C:\Projects\motorsportsapp`

---

### Step 3: Build the Application

Now that you're in the motorsportsapp folder, you have **two simple options**:

**Option A: Double-Click Method (Easiest!)**
1. Find the file `build.bat` in Windows Explorer
2. Double-click it
3. A window will open and show the build progress
4. Wait for it to say "Build completed successfully!"
5. Press any key to close the window

**Option B: Command Line Method**
1. Make sure you're in the motorsportsapp folder in Command Prompt
2. Type: `build.bat`
3. Press Enter
4. Wait for "Build completed successfully!"

**What's happening?**
- The script checks if .NET is installed
- Downloads all required packages (NuGet packages)
- Compiles the application code
- Creates executable files

**This takes about 30-60 seconds the first time.**

---

### Step 4: Run the Application

After building successfully, run the app:

**Option A: Double-Click Method**
1. Find the file `run.bat` in Windows Explorer
2. Double-click it
3. The F1 Live Timing app will start!

**Option B: Command Line Method**
1. In Command Prompt, type: `run.bat`
2. Press Enter
3. The app will start!

The application window should appear with the F1 Live Timing interface. 🎉

---

## 🎯 Quick Reference

### All-In-One Build and Run
```batch
# In Command Prompt, from the motorsportsapp folder:
build.bat && run.bat
```

This builds AND runs in one command.

---

## 🐛 Troubleshooting

### "dotnet is not recognized as an internal or external command"
- **Problem:** .NET SDK is not installed or not in your PATH
- **Solution:** 
  1. Install .NET 8 SDK from https://dotnet.microsoft.com/download/dotnet/8.0
  2. Restart Command Prompt after installation
  3. Try again

### "MotorsportsApp.slnx not found"
- **Problem:** You're not in the right folder
- **Solution:** 
  1. Make sure you're in the motorsportsapp folder (where you extracted/cloned the code)
  2. Look for files like `build.bat`, `run.bat`, and `MotorsportsApp.slnx`
  3. If you don't see them, navigate to the correct folder

### Build fails with errors
- **Problem:** Corrupted packages or network issues
- **Solution:** 
  1. Delete the `bin` and `obj` folders in all project directories
  2. Run `build.bat` again
  3. If still failing, run: `dotnet clean` then `build.bat`

### The app won't start
- **Problem:** Build didn't complete successfully
- **Solution:** 
  1. Run `build.bat` again
  2. Make sure you see "Build completed successfully!"
  3. Then try `run.bat`

### Missing packages or dependencies
- **Problem:** NuGet packages didn't download correctly
- **Solution:**
  1. Run: `dotnet restore`
  2. Then run: `build.bat`

---

## 📁 Project Structure

```
motorsportsapp/
├── build.bat          ← Build script for Windows
├── build.sh           ← Build script for Linux/Mac
├── run.bat            ← Run script for Windows
├── publish.bat        ← Create standalone executable
├── src/               ← Source code
│   ├── MotorsportsApp.Desktop/    ← Main WPF application
│   ├── MotorsportsApp.Core/       ← Business logic
│   ├── MotorsportsApp.Services/   ← API clients
│   ├── MotorsportsApp.Data/       ← Database
│   └── MotorsportsApp.Models/     ← Data models
└── MotorsportsApp.slnx            ← Solution file
```

---

## 🔧 Advanced: Creating a Standalone Executable

Want to run the app without needing .NET installed, or share it with friends?

1. Double-click `publish.bat` (or run it from Command Prompt)
2. Wait for it to complete
3. Find the executable in: `publish\win-x64\MotorsportsApp.Desktop.exe`
4. You can copy this entire `publish\win-x64` folder anywhere and run the `.exe`

The standalone version includes all dependencies, so it's larger (~150MB) but doesn't need .NET to be installed.

---

## 🌐 Linux and macOS Users

The application is a **Windows WPF desktop app** and can only run on Windows. However, you can still:

1. Build the class libraries on Linux/macOS: `./build.sh`
2. Contribute to the codebase
3. Run the WPF app in a Windows VM or using Wine (experimental)

---

## 📖 Additional Documentation

- **README.md** - Overview of the application and features
- **BUILD.md** - Detailed build instructions and environment info
- **DEVELOPMENT.md** - Developer guide for contributing
- **ARCHITECTURE.md** - Technical architecture details

---

## ❓ Still Need Help?

1. Check the **Issues** page on GitHub: https://github.com/finwuh26/motorsportsapp/issues
2. Create a new issue with:
   - What you tried to do
   - What error message you got
   - Your Windows version and .NET version

---

## 🎉 Success!

If you see the F1 Live Timing application window, congratulations! You've successfully built and run the app.

### What's Next?

- Explore the upcoming races
- View driver information
- Check out the planned features in README.md
- Consider contributing to the project!

**Enjoy your F1 Live Timing experience!** 🏁
