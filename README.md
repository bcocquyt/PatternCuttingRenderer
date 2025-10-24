# PatternCuttingRenderer

A cross-platform .NET MAUI application for loading and displaying SVG cutting patterns. This application allows users to view, zoom, and pan cutting patterns for garment design and manufacturing.

## Features

- **SVG Display**: Load and render SVG files containing cutting patterns
- **Interactive Controls**: 
  - Zoom in/out functionality
  - Pan/drag to navigate large patterns
  - Reset view to default
  - Touch/mouse interaction support
- **File Picker**: Open SVG files from device storage
- **Sample Pattern**: Includes a sample shirt cutting pattern for demonstration
- **Cross-Platform**: Runs on Android, iOS, macOS (Catalyst), and Windows

## Technology Stack

- **.NET 9.0**: Latest .NET framework
- **.NET MAUI**: Multi-platform App UI framework for cross-platform development
- **SkiaSharp**: High-performance 2D graphics library for rendering
- **Svg.Skia**: SVG rendering support through SkiaSharp

## Prerequisites

To build and run this application, you need:

1. **.NET 9 SDK** or later
   - Download from: https://dotnet.microsoft.com/download

2. **.NET MAUI Workload**
   ```bash
   dotnet workload install maui
   ```

3. **Platform-Specific Requirements**:
   - **Windows**: Visual Studio 2022 17.8+ with .NET MAUI workload
   - **macOS**: Xcode 15+ and Visual Studio for Mac or VS Code
   - **Android**: Android SDK 21+ (automatically installed with MAUI workload)
   - **iOS**: macOS with Xcode 15+

## Building the Application

### From Command Line

1. **Restore Dependencies**:
   ```bash
   dotnet restore
   ```

2. **Build for Specific Platform**:
   
   **Android**:
   ```bash
   dotnet build -f net9.0-android
   ```
   
   **iOS** (requires macOS):
   ```bash
   dotnet build -f net9.0-ios
   ```
   
   **Windows**:
   ```bash
   dotnet build -f net9.0-windows10.0.19041.0
   ```
   
   **macOS Catalyst**:
   ```bash
   dotnet build -f net9.0-maccatalyst
   ```

3. **Run the Application**:
   ```bash
   # Android
   dotnet run -f net9.0-android
   
   # Windows
   dotnet run -f net9.0-windows10.0.19041.0
   ```

### From Visual Studio 2022

1. Open `PatternCuttingRenderer.csproj`
2. Select target platform (Android, iOS, Windows, etc.) from dropdown
3. Press F5 or click "Run"

### From Visual Studio Code

1. Install C# Dev Kit extension
2. Install .NET MAUI extension
3. Open folder in VS Code
4. Select target framework from status bar
5. Press F5 to run

## Project Structure

```
PatternCuttingRenderer/
├── App.xaml                      # Application resources
├── App.xaml.cs                   # Application entry point
├── AppShell.xaml                 # Shell navigation
├── AppShell.xaml.cs
├── MainPage.xaml                 # Main UI page
├── MainPage.xaml.cs              # SVG display logic
├── MauiProgram.cs                # App configuration
├── Program.cs                    # Entry point
├── Platforms/                    # Platform-specific code
│   ├── Android/
│   │   ├── MainActivity.cs
│   │   ├── MainApplication.cs
│   │   └── AndroidManifest.xml
│   ├── iOS/
│   │   ├── AppDelegate.cs
│   │   ├── Program.cs
│   │   └── Info.plist
│   ├── MacCatalyst/
│   │   ├── AppDelegate.cs
│   │   ├── Program.cs
│   │   └── Info.plist
│   └── Windows/
│       ├── App.xaml
│       ├── App.xaml.cs
│       └── Package.appxmanifest
└── Resources/                    # Application resources
    ├── AppIcon/                  # App icon
    ├── Splash/                   # Splash screen
    ├── Fonts/                    # Fonts
    ├── Images/                   # Images
    ├── Styles/                   # XAML styles
    │   ├── Colors.xaml
    │   └── Styles.xaml
    └── Raw/                      # Raw assets
        └── sample_pattern.svg    # Sample cutting pattern
```

## Usage

1. **Launch the Application**: Start the app on your target platform

2. **View Sample Pattern**: The app loads a sample shirt cutting pattern automatically

3. **Load Custom SVG**:
   - Click "Open SVG" button
   - Select an SVG file from your device
   - The pattern will be displayed on the canvas

4. **Navigate the Pattern**:
   - **Zoom In**: Click "Zoom In" button or use pinch gesture
   - **Zoom Out**: Click "Zoom Out" button or use pinch gesture
   - **Pan**: Click and drag (or touch and drag) to move around
   - **Reset View**: Click "Reset View" to return to original view

## Sample Pattern

The included `sample_pattern.svg` demonstrates a typical cutting pattern with:
- Main body outline
- Neckline and armholes
- Seam lines and measurements
- Cut notches and grain line indicators
- Pattern information box
- Legend for different line types

## Customization

### Adding Your Own Patterns

Replace or add SVG files in the `Resources/Raw/` folder and update the embedded resource reference in the `.csproj` file:

```xml
<ItemGroup>
    <EmbeddedResource Include="Resources\Raw\your_pattern.svg" />
</ItemGroup>
```

### Modifying UI

Edit `MainPage.xaml` to customize the user interface layout, colors, and controls.

### Changing Colors

Modify `Resources/Styles/Colors.xaml` to adjust the application color scheme.

## Dependencies

- **Microsoft.Maui.Controls** (9.0.10): Core MAUI framework
- **SkiaSharp.Views.Maui.Controls** (2.88.8): SkiaSharp integration for MAUI
- **Svg.Skia** (2.0.0.1): SVG rendering capabilities

All dependencies are managed through NuGet and are free from known security vulnerabilities.

## Troubleshooting

### MAUI Workload Not Found

If you get errors about missing MAUI workload:
```bash
dotnet workload install maui
```

### Android Build Issues

Ensure Android SDK is properly installed:
```bash
dotnet workload install android
```

### iOS Build Issues (macOS only)

Ensure Xcode is installed and command-line tools are configured:
```bash
xcode-select --install
```

### Windows Build Issues

Ensure you have Windows SDK 10.0.19041.0 or later installed through Visual Studio Installer.

## Future Enhancements

Potential features for future development:
- 3D rendering of patterns on body models
- Pattern editing capabilities
- Measurement tools and annotations
- Export patterns to different formats
- Print preview and printing support
- Pattern library management
- Multi-pattern view/comparison

## License

This project is open source. Please check the repository for license details.

## Contributing

Contributions are welcome! Please submit issues and pull requests through the repository.

## Support

For issues, questions, or contributions, please visit the project repository on GitHub.

