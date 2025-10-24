# Building and Running PatternCuttingRenderer

This guide provides detailed instructions for building and running the PatternCuttingRenderer MAUI application on different platforms.

## System Requirements

### All Platforms
- .NET 9.0 SDK or later
- .NET MAUI workload

### Windows Development
- Windows 10 version 1809 or later (Windows 11 recommended)
- Visual Studio 2022 version 17.8 or later with:
  - .NET Multi-platform App UI development workload
  - Windows 10 SDK (10.0.19041.0 or later)
- OR Visual Studio Code with:
  - C# Dev Kit extension
  - .NET MAUI extension

### macOS Development
- macOS 12 or later (macOS 13 Ventura or later recommended)
- Xcode 15 or later
- Visual Studio for Mac 2022 or Visual Studio Code with extensions

### Linux Development
- Note: MAUI apps cannot be built on Linux for Android/iOS/Windows targets
- Consider using Windows or macOS for full development experience

## Initial Setup

### 1. Install .NET 9 SDK

Download and install from: https://dotnet.microsoft.com/download/dotnet/9.0

Verify installation:
```bash
dotnet --version
# Should show 9.0.x
```

### 2. Install MAUI Workload

```bash
dotnet workload install maui
```

This will install support for:
- Android
- iOS (macOS only)
- macOS Catalyst (macOS only)
- Windows (Windows only)

Verify MAUI installation:
```bash
dotnet workload list
```

You should see `maui` in the installed workloads list.

### 3. Install Platform-Specific Tools

#### For Android Development (All Platforms)
The Android SDK is installed automatically with the MAUI workload.

To manually install or update:
```bash
dotnet workload install android
```

#### For iOS/macOS Development (macOS only)
1. Install Xcode from the Mac App Store
2. Install Xcode Command Line Tools:
   ```bash
   xcode-select --install
   ```
3. Accept Xcode license:
   ```bash
   sudo xcodebuild -license accept
   ```

#### For Windows Development (Windows only)
Install Windows SDK through Visual Studio Installer:
- Windows 10 SDK (10.0.19041.0 or later)
- Or Windows 11 SDK

## Building the Application

### Clone the Repository

```bash
git clone https://github.com/bcocquyt/PatternCuttingRenderer.git
cd PatternCuttingRenderer
```

### Restore NuGet Packages

```bash
dotnet restore
```

### Build for Specific Platform

#### Android
```bash
# Debug build
dotnet build -f net9.0-android

# Release build
dotnet build -f net9.0-android -c Release

# Build and create APK
dotnet publish -f net9.0-android -c Release
```

The APK will be in: `bin/Release/net9.0-android/publish/`

#### iOS (macOS only)
```bash
# Debug build for simulator
dotnet build -f net9.0-ios

# Build for device
dotnet build -f net9.0-ios -c Release /p:RuntimeIdentifier=ios-arm64
```

#### macOS Catalyst (macOS only)
```bash
# Debug build
dotnet build -f net9.0-maccatalyst

# Release build
dotnet build -f net9.0-maccatalyst -c Release
```

#### Windows (Windows only)
```bash
# Debug build
dotnet build -f net9.0-windows10.0.19041.0

# Release build
dotnet build -f net9.0-windows10.0.19041.0 -c Release

# Create MSIX package
dotnet publish -f net9.0-windows10.0.19041.0 -c Release -p:RuntimeIdentifierOverride=win10-x64
```

## Running the Application

### From Command Line

#### Android
1. Start an Android emulator or connect a device
2. List available devices:
   ```bash
   dotnet build -f net9.0-android -t:Run
   ```

#### Windows
```bash
dotnet run -f net9.0-windows10.0.19041.0
```

### From Visual Studio 2022 (Windows)

1. Open `PatternCuttingRenderer.csproj` or `PatternCuttingRenderer.sln`
2. Select target platform from the dropdown (Android, Windows, etc.)
3. Select target device/emulator
4. Press F5 or click the "Play" button

### From Visual Studio for Mac

1. Open `PatternCuttingRenderer.sln`
2. Select target platform (Android, iOS, macOS)
3. Select target device/simulator
4. Press ⌘+Return or click "Run"

### From Visual Studio Code

1. Install required extensions:
   - C# Dev Kit
   - .NET MAUI
2. Open the project folder
3. Select target framework from status bar
4. Press F5 to start debugging

## Platform-Specific Instructions

### Android Emulator Setup

1. Open Android Studio or use Visual Studio's Android Device Manager
2. Create a new Virtual Device:
   - Recommended: Pixel 5 API 33 or later
   - Enable hardware acceleration if available
3. Start the emulator before running the app

### iOS Simulator Setup (macOS only)

The iOS Simulator is included with Xcode. To launch manually:
```bash
open -a Simulator
```

Select a device from the Hardware menu.

### Windows Development

No additional setup needed. The app runs directly on Windows.

## Troubleshooting

### "Workload 'maui' not found"

**Solution**:
```bash
dotnet workload install maui
```

### Android Build Fails with SDK Error

**Solution**:
```bash
# Update Android workload
dotnet workload update
dotnet workload install android

# Or set ANDROID_HOME environment variable
export ANDROID_HOME=$HOME/Library/Android/sdk  # macOS/Linux
set ANDROID_HOME=%LOCALAPPDATA%\Android\Sdk    # Windows
```

### iOS Build Fails with Provisioning Error

**Solution**:
1. Open Xcode
2. Go to Settings > Accounts
3. Add your Apple ID
4. Select your team in the project settings
5. Xcode will automatically create a provisioning profile

### Windows Build Fails with SDK Error

**Solution**:
1. Open Visual Studio Installer
2. Modify your Visual Studio installation
3. Ensure "Windows 10 SDK (10.0.19041.0)" or later is installed
4. Restart Visual Studio

### "Could not load file or assembly SkiaSharp..."

**Solution**:
```bash
# Clean and rebuild
dotnet clean
dotnet restore
dotnet build
```

### MAUI Workload Not Available on Linux

**Note**: Full MAUI development is not supported on Linux. You can:
1. Use Windows or macOS for development
2. Use Windows Subsystem for Linux (WSL) with Windows
3. Use a cloud development environment

## Performance Tips

### Debug Builds
- Slower performance due to debugging overhead
- Use for development and testing

### Release Builds
- Optimized performance
- Use for testing final app behavior
- Required for app store submission

### AOT Compilation (iOS/macOS)
Add to .csproj for better startup performance:
```xml
<PropertyGroup Condition="'$(Configuration)' == 'Release'">
    <MtouchLink>Full</MtouchLink>
    <EnableLLVM>true</EnableLLVM>
</PropertyGroup>
```

### Android Linking
For smaller APK size:
```xml
<PropertyGroup Condition="'$(Configuration)' == 'Release'">
    <AndroidLinkMode>Full</AndroidLinkMode>
</PropertyGroup>
```

## Publishing

### Android (Google Play Store)
```bash
# Create signed APK
dotnet publish -f net9.0-android -c Release -p:AndroidKeyStore=true -p:AndroidSigningKeyStore=keystore.keystore -p:AndroidSigningKeyAlias=key -p:AndroidSigningKeyPass=password -p:AndroidSigningStorePass=password
```

### iOS (App Store)
1. Build with Release configuration
2. Archive in Xcode
3. Upload to App Store Connect

### Windows (Microsoft Store)
```bash
# Create MSIX package
dotnet publish -f net9.0-windows10.0.19041.0 -c Release -p:RuntimeIdentifierOverride=win10-x64
```

## Development Workflow

1. **Make Changes**: Edit code in your preferred IDE
2. **Test Locally**: Run on emulator/simulator
3. **Debug**: Use breakpoints and debugging tools
4. **Build Release**: Create optimized build
5. **Test on Device**: Deploy to physical device
6. **Publish**: Submit to app store

## Additional Resources

- [.NET MAUI Documentation](https://docs.microsoft.com/dotnet/maui/)
- [SkiaSharp Documentation](https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/graphics/skiasharp/)
- [MAUI GitHub Repository](https://github.com/dotnet/maui)
- [.NET CLI Documentation](https://docs.microsoft.com/dotnet/core/tools/)

## Getting Help

- Check the README.md for basic usage
- Review this BUILD.md for build issues
- Open an issue on GitHub for bugs
- Consult .NET MAUI documentation for framework questions
