# Quick Start Guide

Get up and running with PatternCuttingRenderer in minutes!

## Prerequisites

- .NET 9.0 SDK: https://dotnet.microsoft.com/download/dotnet/9.0
- Visual Studio 2022 (Windows) or Visual Studio Code with MAUI extensions

## Quick Setup (5 minutes)

### 1. Install .NET MAUI Workload

Open a terminal and run:

```bash
dotnet workload install maui
```

This installs support for Android, iOS, Windows, and macOS.

### 2. Clone or Download

```bash
git clone https://github.com/bcocquyt/PatternCuttingRenderer.git
cd PatternCuttingRenderer
```

### 3. Restore Dependencies

```bash
dotnet restore
```

### 4. Run the Application

**On Windows:**
```bash
dotnet run -f net9.0-windows10.0.19041.0
```

**On Android (with emulator running):**
```bash
dotnet run -f net9.0-android
```

**On macOS:**
```bash
dotnet run -f net9.0-maccatalyst
```

## Using Visual Studio 2022

1. Open `PatternCuttingRenderer.sln`
2. Select your target platform (Android, Windows, etc.) from the dropdown
3. Press **F5** or click the **Run** button
4. The app will launch and display a sample cutting pattern

## First Launch

When you first run the app:

1. **Sample Pattern Loads**: A shirt cutting pattern is displayed automatically
2. **Try the Controls**:
   - Click **Zoom In** / **Zoom Out** to change scale
   - **Click and drag** on the pattern to pan around
   - Click **Reset View** to return to the default view
3. **Load Your Own SVG**:
   - Click **Open SVG**
   - Select any SVG file from your device
   - The pattern will be displayed immediately

## What's Included

- ✅ Complete .NET MAUI application structure
- ✅ SVG rendering with SkiaSharp
- ✅ Interactive zoom and pan controls
- ✅ Sample cutting pattern for demonstration
- ✅ Cross-platform support (Android, iOS, Windows, macOS)
- ✅ File picker for loading custom SVG files

## Troubleshooting

### "MAUI workload not found"
```bash
dotnet workload install maui
```

### Build errors on first run
```bash
dotnet clean
dotnet restore
dotnet build
```

### Android emulator not detected
1. Open Visual Studio
2. Go to Tools > Android > Android Device Manager
3. Create and start a virtual device

### Can't build for iOS/macOS (Windows users)
iOS and macOS builds require a Mac. Windows users can build for Android and Windows only.

## Next Steps

- 📖 Read the [README.md](README.md) for detailed features
- 🏗️ Check [BUILD.md](BUILD.md) for platform-specific build instructions
- 🏛️ Review [ARCHITECTURE.md](ARCHITECTURE.md) to understand the code structure
- 🔧 Modify `MainPage.xaml` to customize the UI
- 🎨 Edit `Resources/Styles/Colors.xaml` to change the color scheme
- 📦 Add your own SVG patterns to `Resources/Raw/`

## Common Tasks

### Change App Colors
Edit `Resources/Styles/Colors.xaml`:
```xml
<Color x:Key="Primary">#512BD4</Color>  <!-- Change this -->
```

### Add a New SVG Pattern
1. Place SVG file in `Resources/Raw/`
2. Add to `PatternCuttingRenderer.csproj`:
   ```xml
   <EmbeddedResource Include="Resources\Raw\your_pattern.svg" />
   ```
3. Load in `MainPage.xaml.cs`:
   ```csharp
   var resourceName = "PatternCuttingRenderer.Resources.Raw.your_pattern.svg";
   ```

### Build for Release
```bash
# Android
dotnet build -f net9.0-android -c Release

# Windows
dotnet build -f net9.0-windows10.0.19041.0 -c Release
```

## Getting Help

- 🐛 **Found a bug?** Open an issue on GitHub
- 💬 **Need help?** Check the detailed documentation in BUILD.md
- 📚 **Want to learn more?** Read ARCHITECTURE.md

## Success!

You now have a working cross-platform SVG viewer for cutting patterns. Start exploring and customizing!

Happy coding! 🎉
