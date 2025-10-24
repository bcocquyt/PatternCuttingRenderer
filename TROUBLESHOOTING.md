# Troubleshooting Guide

Common issues and solutions for PatternCuttingRenderer.

## Table of Contents
- [Installation Issues](#installation-issues)
- [Build Errors](#build-errors)
- [Runtime Errors](#runtime-errors)
- [Platform-Specific Issues](#platform-specific-issues)
- [Performance Issues](#performance-issues)
- [SVG Loading Issues](#svg-loading-issues)

---

## Installation Issues

### .NET SDK Not Found

**Problem**: `dotnet: command not found` or similar error

**Solution**:
1. Download .NET 9.0 SDK from https://dotnet.microsoft.com/download
2. Install following platform instructions
3. Restart terminal/IDE
4. Verify: `dotnet --version`

### MAUI Workload Installation Fails

**Problem**: `dotnet workload install maui` fails

**Solution**:
```bash
# Update .NET first
dotnet --version

# Try installing with sudo (macOS/Linux)
sudo dotnet workload install maui

# Or repair existing installation
dotnet workload repair

# Check for updates
dotnet workload update
```

### Visual Studio Can't Find MAUI

**Problem**: MAUI templates not available in Visual Studio

**Solution**:
1. Open Visual Studio Installer
2. Click "Modify" on your VS installation
3. Check ".NET Multi-platform App UI development" workload
4. Click "Modify" to install
5. Restart Visual Studio

---

## Build Errors

### NETSDK1147: Workload Not Installed

**Error**: `To build this project, the following workloads must be installed: maui-android`

**Solution**:
```bash
# Install missing workload
dotnet workload install maui-android

# Or install all MAUI workloads
dotnet workload install maui
```

### SkiaSharp Assembly Not Found

**Error**: `Could not load file or assembly 'SkiaSharp'`

**Solution**:
```bash
# Clean and restore
dotnet clean
dotnet restore

# Force NuGet package restore
dotnet restore --force

# Clear NuGet cache if needed
dotnet nuget locals all --clear
dotnet restore
```

### Android SDK Not Found

**Error**: `Android SDK not found` or `ANDROID_HOME not set`

**Solution (Windows)**:
```cmd
setx ANDROID_HOME "%LOCALAPPDATA%\Android\Sdk"
```

**Solution (macOS/Linux)**:
```bash
export ANDROID_HOME=$HOME/Library/Android/sdk  # macOS
export ANDROID_HOME=$HOME/Android/Sdk           # Linux

# Add to ~/.bash_profile or ~/.zshrc for persistence
```

### iOS Build Fails - Provisioning Profile

**Error**: `No valid iOS code signing keys found`

**Solution**:
1. Open Xcode
2. Go to Settings > Accounts
3. Sign in with your Apple ID
4. Select your Team
5. Xcode will generate free provisioning profile
6. Try build again

### Windows SDK Not Found

**Error**: `Windows SDK version 10.0.19041.0 not found`

**Solution**:
1. Open Visual Studio Installer
2. Modify installation
3. Under "Individual Components" tab
4. Search for "Windows 10 SDK"
5. Check version 10.0.19041.0 or higher
6. Install

---

## Runtime Errors

### Application Crashes on Launch

**Symptoms**: App closes immediately after launch

**Debugging Steps**:
```bash
# Run with verbose output
dotnet run -f net9.0-windows10.0.19041.0 --verbosity detailed

# Check for exceptions in Output window (Visual Studio)
# Look at Android Logcat (for Android)
# Check iOS Device Log (for iOS)
```

**Common Causes**:
1. Missing resources (fonts, images)
2. Invalid XAML syntax
3. Unhandled exception in constructor
4. Platform-specific issue

### "File Not Found" When Loading Sample Pattern

**Error**: Sample pattern doesn't load on startup

**Solution**:
1. Check `Resources/Raw/sample_pattern.svg` exists
2. Verify in `.csproj`:
   ```xml
   <EmbeddedResource Include="Resources\Raw\sample_pattern.svg" />
   ```
3. Rebuild project
4. Check resource name matches exactly (case-sensitive)

### Touch Events Not Working

**Problem**: Can't pan or interact with canvas

**Solution**:
1. Verify `EnableTouchEvents="True"` on SKCanvasView in XAML
2. Check `Touch="OnCanvasTouch"` is set
3. Ensure method signature matches:
   ```csharp
   private void OnCanvasTouch(object sender, SKTouchEventArgs e)
   ```
4. Try on physical device (emulator touch may differ)

### Zoom Doesn't Work

**Problem**: Zoom buttons don't affect view

**Solution**:
1. Check `_scale` is being updated in event handlers
2. Ensure `InvalidateSurface()` is called after scale change
3. Verify `OnCanvasViewPaintSurface` applies scale transformation
4. Check for exceptions in event handlers

---

## Platform-Specific Issues

### Android

#### Emulator Won't Start

**Solution**:
1. Open Android Device Manager
2. Check emulator settings
3. Enable hardware acceleration (HAXM/Hyper-V)
4. Increase RAM allocation
5. Try different emulator image

#### App Requires Permissions

**Problem**: Can't access files

**Solution**:
Add to `AndroidManifest.xml`:
```xml
<uses-permission android:name="android.permission.READ_EXTERNAL_STORAGE" />
<uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />
```

#### Slow Performance on Emulator

**Solution**:
1. Use physical device for testing
2. Enable hardware acceleration
3. Use x86_64 emulator image
4. Close other applications
5. Increase emulator RAM

### iOS

#### "iPhone is busy" Error

**Solution**:
1. Unplug and replug device
2. Trust computer on iPhone
3. Wait for "Processing symbol files"
4. Try again

#### App Not Installing

**Solution**:
1. Check provisioning profile
2. Verify bundle identifier matches
3. Check device is registered in Apple Developer Portal
4. Clean build folder (Xcode: Product > Clean Build Folder)

### Windows

#### MSIX Package Issues

**Problem**: Can't create or install MSIX package

**Solution**:
1. Run as Administrator
2. Enable Developer Mode (Settings > Update & Security > For developers)
3. Check certificate is trusted
4. Use `dotnet publish` instead of `dotnet build`

#### Windows Security Blocks App

**Solution**:
1. Click "More info"
2. Click "Run anyway"
3. Or add exception in Windows Security

### macOS

#### "App is damaged" Message

**Solution**:
```bash
# Remove quarantine attribute
xattr -d com.apple.quarantine /path/to/app
```

#### Gatekeeper Blocks App

**Solution**:
1. Right-click app
2. Select "Open"
3. Click "Open" in dialog
4. Or disable Gatekeeper temporarily (not recommended)

---

## Performance Issues

### Slow Rendering

**Symptoms**: Canvas updates slowly, laggy interactions

**Solutions**:
1. **Optimize SVG**: Simplify complex paths
2. **Use Release Build**: Debug builds are slower
   ```bash
   dotnet build -c Release
   ```
3. **Reduce Invalidate Calls**: Only invalidate when needed
4. **Hardware Acceleration**: Ensure it's enabled (default)

### High Memory Usage

**Symptoms**: App consumes too much RAM

**Solutions**:
1. **Dispose Objects**: Properly dispose SKSvg objects
2. **Cache Bitmaps**: Don't recreate on every paint
3. **Limit Pattern Size**: Very large SVGs may need optimization
4. **Profile Memory**: Use platform profiling tools

### Slow Startup

**Symptoms**: App takes long to launch

**Solutions**:
1. **Lazy Load Resources**: Don't load everything at startup
2. **Async Initialization**: Use async/await for loading
3. **Optimize Images**: Use appropriate sizes
4. **Enable AOT** (iOS/macOS): Faster startup in Release builds

---

## SVG Loading Issues

### SVG Doesn't Display

**Problem**: Canvas is blank after loading SVG

**Debugging**:
```csharp
// Add logging
private void LoadSvgFromStream(Stream stream)
{
    _svgImage = new SKSvg();
    var result = _svgImage.Load(stream);
    
    if (_svgImage.Picture == null)
    {
        StatusLabel.Text = "Failed to load SVG";
        return;
    }
    
    StatusLabel.Text = $"SVG loaded: {_svgImage.Picture.CullRect}";
    skiaCanvas.InvalidateSurface();
}
```

**Common Issues**:
1. SVG file is corrupt or malformed
2. Unsupported SVG features
3. Empty viewBox or invalid dimensions
4. Stream is closed before reading

### Colors Don't Display Correctly

**Problem**: SVG colors different from expected

**Solutions**:
1. Check color format (hex, rgb, named colors)
2. Verify stroke and fill attributes
3. Check for CSS styling (may not be supported)
4. Test SVG in browser first

### Text Doesn't Render

**Problem**: Text elements don't show

**Solutions**:
1. Ensure fonts are available
2. Check font-family matches installed fonts
3. Use web-safe fonts
4. Convert text to paths in SVG editor

### SVG Too Large/Small

**Problem**: SVG doesn't fit canvas properly

**Solution**:
```csharp
// Adjust auto-scaling factor
var autoScale = Math.Min(scaleX, scaleY) * 0.9f; // Change 0.9f
```

---

## Getting More Help

### Enable Diagnostic Logging

Add to `MauiProgram.cs`:
```csharp
#if DEBUG
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Trace);
#endif
```

### Collect Crash Logs

**Android**:
```bash
adb logcat *:E
```

**iOS**:
Open Console.app and filter by app name

**Windows**:
Check Event Viewer > Application logs

### Check GitHub Issues

Search existing issues: https://github.com/bcocquyt/PatternCuttingRenderer/issues

### Ask for Help

1. Check documentation first
2. Search existing issues
3. Create new issue with:
   - Clear description
   - Steps to reproduce
   - Platform and version info
   - Error messages
   - Screenshots

---

## Still Having Issues?

Contact the community:
- Open a GitHub issue
- Check discussion forums
- Review documentation
- Update to latest version

Remember to include:
- Exact error message
- Platform and versions
- Steps to reproduce
- What you've already tried

---

*Last Updated: 2024*
*For latest troubleshooting tips, check the GitHub repository.*
