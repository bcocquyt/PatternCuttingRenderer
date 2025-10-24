# PatternCuttingRenderer Architecture

This document describes the architecture and design of the PatternCuttingRenderer MAUI application.

## Overview

PatternCuttingRenderer is a cross-platform mobile and desktop application built with .NET MAUI that displays SVG-based cutting patterns for garment design. The application uses SkiaSharp for high-performance 2D rendering.

## Technology Stack

### Core Frameworks
- **.NET 9.0**: Modern, cross-platform .NET framework
- **.NET MAUI**: Multi-platform App UI framework for building native apps
- **C# 12**: Primary programming language

### Graphics and Rendering
- **SkiaSharp (2.88.8)**: Cross-platform 2D graphics engine
- **Svg.Skia (2.0.0.1)**: SVG parsing and rendering library
- **SkiaSharp.Views.Maui.Controls**: MAUI integration for SkiaSharp

### UI Framework
- **XAML**: Declarative UI markup language
- **MAUI Controls**: Native UI controls for each platform

## Architecture Layers

```
┌─────────────────────────────────────────────┐
│           Presentation Layer                 │
│  (XAML Views, Code-Behind, ViewModels)      │
├─────────────────────────────────────────────┤
│           Business Logic Layer               │
│  (SVG Loading, Rendering, Transformations)  │
├─────────────────────────────────────────────┤
│         Platform Abstraction Layer           │
│         (.NET MAUI Framework)                │
├─────────────────────────────────────────────┤
│         Platform-Specific Layer              │
│   (Android, iOS, Windows, macOS)            │
└─────────────────────────────────────────────┘
```

## Project Structure

### Core Application Files

#### `App.xaml` / `App.xaml.cs`
- Application entry point and lifecycle management
- Resource dictionary definitions
- Global styling configuration

#### `AppShell.xaml` / `AppShell.xaml.cs`
- Shell-based navigation structure
- Route registration
- Flyout menu configuration (if needed)

#### `MauiProgram.cs`
- Application builder and configuration
- Dependency injection setup
- Service registration
- Font registration
- SkiaSharp integration setup

#### `Program.cs`
- Platform-independent entry point
- Creates and runs the MauiApplication

### Presentation Layer

#### `MainPage.xaml`
- Main user interface layout
- Toolbar with action buttons (Open, Zoom In/Out, Reset)
- SkiaSharp canvas for rendering
- Status bar for feedback

#### `MainPage.xaml.cs`
- UI logic and event handlers
- SVG loading and management
- Canvas rendering logic
- User interaction handling (touch, zoom, pan)
- File picker integration

### Platform-Specific Code

Each platform has its own entry point and configuration:

#### Android (`Platforms/Android/`)
- **MainActivity.cs**: Main activity and app entry
- **MainApplication.cs**: Android application class
- **AndroidManifest.xml**: Permissions and app metadata

#### iOS (`Platforms/iOS/`)
- **AppDelegate.cs**: iOS app delegate
- **Program.cs**: iOS entry point
- **Info.plist**: App capabilities and metadata

#### macOS Catalyst (`Platforms/MacCatalyst/`)
- **AppDelegate.cs**: macOS app delegate
- **Program.cs**: macOS entry point
- **Info.plist**: App configuration

#### Windows (`Platforms/Windows/`)
- **App.xaml**: Windows-specific app configuration
- **Package.appxmanifest**: Windows app metadata and capabilities

### Resources

#### `Resources/Styles/`
- **Colors.xaml**: Color palette definitions
- **Styles.xaml**: Reusable control styles

#### `Resources/Fonts/`
- OpenSans font files for consistent typography

#### `Resources/AppIcon/`
- Application icon in SVG format
- Automatically converted to platform-specific formats

#### `Resources/Splash/`
- Splash screen graphics

#### `Resources/Images/`
- Image assets used in the application

#### `Resources/Raw/`
- **sample_pattern.svg**: Example cutting pattern
- Embedded as a resource for default display

## Key Components

### SVG Rendering Engine

The application uses Svg.Skia to parse SVG files and SkiaSharp to render them:

```csharp
private SKSvg? _svgImage;

private void LoadSvgFromStream(Stream stream)
{
    _svgImage = new SKSvg();
    _svgImage.Load(stream);
    skiaCanvas.InvalidateSurface();
}
```

### Canvas Rendering

The `OnCanvasViewPaintSurface` method handles the rendering:

1. Clear the canvas
2. Calculate appropriate scaling to fit the SVG
3. Apply user transformations (zoom, pan)
4. Draw the SVG picture

```csharp
private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
{
    var canvas = e.Surface.Canvas;
    canvas.Clear(SKColors.White);
    
    // Apply transformations
    canvas.Translate(_translate);
    canvas.Scale(_scale);
    
    // Draw SVG
    canvas.DrawPicture(_svgImage.Picture);
}
```

### User Interaction

Touch/mouse events are handled for:
- **Pan**: Dragging to move the view
- **Zoom**: Pinch gestures or button clicks
- **File Selection**: Platform file picker

## Data Flow

```
┌──────────────┐
│ User Action  │
└──────┬───────┘
       │
       v
┌─────────────────┐
│ Event Handler   │
└──────┬──────────┘
       │
       v
┌─────────────────┐
│ Update State    │
│ (_scale, _translate)
└──────┬──────────┘
       │
       v
┌─────────────────┐
│ InvalidateSurface()
└──────┬──────────┘
       │
       v
┌─────────────────┐
│ OnPaintSurface  │
└──────┬──────────┘
       │
       v
┌─────────────────┐
│ Render to Canvas│
└─────────────────┘
```

## Design Patterns

### MVVM (Model-View-ViewModel)
While the current implementation uses code-behind for simplicity, the architecture supports MVVM:
- **View**: XAML files (MainPage.xaml)
- **ViewModel**: Can be added for data binding
- **Model**: SVG data and pattern metadata

### Dependency Injection
The application uses .NET's built-in DI container:
```csharp
builder.Services.AddSingleton<IPatternService, PatternService>();
```

### Repository Pattern
Can be implemented for pattern storage and retrieval:
```csharp
public interface IPatternRepository
{
    Task<Pattern> GetPatternAsync(string id);
    Task<IEnumerable<Pattern>> GetAllPatternsAsync();
}
```

## Cross-Platform Considerations

### Platform Abstraction
MAUI provides abstractions for:
- File system access
- File picker
- Display information
- Device capabilities

### Platform-Specific Code
Use conditional compilation for platform-specific features:
```csharp
#if ANDROID
    // Android-specific code
#elif IOS
    // iOS-specific code
#elif WINDOWS
    // Windows-specific code
#endif
```

Or use dependency injection with platform implementations:
```csharp
#if ANDROID
builder.Services.AddSingleton<IPlatformService, AndroidPlatformService>();
#elif IOS
builder.Services.AddSingleton<IPlatformService, iOSPlatformService>();
#endif
```

## Performance Optimizations

### Rendering Performance
- Use hardware acceleration (enabled by default in SkiaSharp)
- Invalidate surface only when necessary
- Cache rendered bitmaps for complex SVGs

### Memory Management
- Dispose of SKSvg objects when no longer needed
- Use weak references for large resources
- Implement proper cleanup in Dispose methods

### Startup Performance
- Lazy load resources
- Use async initialization
- Minimize work in constructors

## Security Considerations

### File Access
- Use MAUI's FilePicker for secure file access
- Validate file types before loading
- Implement file size limits

### SVG Parsing
- The Svg.Skia library handles malformed SVG safely
- Consider implementing additional validation for untrusted sources

### Permissions
- Request only necessary permissions
- Follow platform-specific permission guidelines

## Testing Strategy

### Unit Tests
- Test business logic independently
- Mock MAUI services
- Test transformation calculations

### Integration Tests
- Test file loading
- Test rendering pipeline
- Test user interactions

### UI Tests
- Use MAUI UI testing framework
- Test on multiple platforms
- Test different screen sizes and orientations

## Future Enhancements

### 3D Rendering
- Integrate 3D graphics engine (e.g., UrhoSharp)
- Map 2D patterns onto 3D body models
- Add rotation and camera controls

### Pattern Editing
- Add drawing tools
- Implement measurement tools
- Support pattern creation

### Cloud Integration
- Save patterns to cloud storage
- Sync across devices
- Share patterns with team members

### Advanced Features
- Multiple pattern comparison
- Fabric texture overlay
- Print preview and PDF export
- Animation of pattern assembly

## Development Guidelines

### Code Style
- Follow C# coding conventions
- Use meaningful variable names
- Comment complex algorithms
- Keep methods small and focused

### Version Control
- Use feature branches
- Write descriptive commit messages
- Tag releases appropriately

### Documentation
- Document public APIs
- Update README for user-facing changes
- Maintain architecture documentation

## Deployment

### Build Configuration
- **Debug**: Development and testing
- **Release**: Production deployment with optimizations

### Platform-Specific Builds
- Android: APK or AAB for Google Play
- iOS: IPA for App Store
- Windows: MSIX for Microsoft Store
- macOS: PKG or DMG for distribution

## References

- [.NET MAUI Documentation](https://docs.microsoft.com/dotnet/maui/)
- [SkiaSharp Documentation](https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/graphics/skiasharp/)
- [MVVM Pattern](https://docs.microsoft.com/dotnet/architecture/maui/mvvm)
- [MAUI Shell](https://docs.microsoft.com/dotnet/maui/fundamentals/shell/)
