# Contributing to PatternCuttingRenderer

Thank you for your interest in contributing to PatternCuttingRenderer! This document provides guidelines and information for contributors.

## Code of Conduct

- Be respectful and inclusive
- Welcome newcomers and help them learn
- Focus on what is best for the community
- Show empathy towards other community members

## How to Contribute

### Reporting Bugs

Found a bug? Please open an issue with:

1. **Clear title**: Describe the issue concisely
2. **Steps to reproduce**: List exact steps to recreate the bug
3. **Expected behavior**: What should happen
4. **Actual behavior**: What actually happens
5. **Environment**: 
   - OS and version
   - .NET version
   - Platform (Android, iOS, Windows, macOS)
6. **Screenshots**: If applicable
7. **Error messages**: Copy full error text

### Suggesting Features

Have an idea? Create an issue with:

1. **Clear description**: What feature do you want?
2. **Use case**: Why is this feature needed?
3. **Proposed solution**: How might it work?
4. **Alternatives**: What other approaches did you consider?
5. **Mockups/diagrams**: Visual aids if helpful

### Pull Requests

Ready to contribute code? Follow these steps:

1. **Fork the repository**
2. **Clone your fork**:
   ```bash
   git clone https://github.com/YOUR_USERNAME/PatternCuttingRenderer.git
   ```
3. **Create a branch**:
   ```bash
   git checkout -b feature/your-feature-name
   ```
4. **Make your changes**
5. **Test thoroughly** on at least one platform
6. **Commit with clear messages**:
   ```bash
   git commit -m "Add feature: brief description"
   ```
7. **Push to your fork**:
   ```bash
   git push origin feature/your-feature-name
   ```
8. **Open a Pull Request** on the main repository

## Development Setup

### Prerequisites
- .NET 9.0 SDK
- MAUI workload installed
- IDE (Visual Studio 2022 or VS Code)

### Setup Steps
```bash
# Clone your fork
git clone https://github.com/YOUR_USERNAME/PatternCuttingRenderer.git
cd PatternCuttingRenderer

# Install dependencies
dotnet restore

# Build the project
dotnet build

# Run on your platform
dotnet run -f net9.0-windows10.0.19041.0  # Windows
dotnet run -f net9.0-android              # Android
```

## Coding Guidelines

### C# Style
- Follow [Microsoft C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Use meaningful variable names
- Add XML documentation comments for public APIs
- Keep methods focused and concise

### Example:
```csharp
/// <summary>
/// Loads an SVG pattern from the specified stream.
/// </summary>
/// <param name="stream">The stream containing SVG data.</param>
/// <exception cref="ArgumentNullException">Thrown when stream is null.</exception>
public void LoadSvgFromStream(Stream stream)
{
    if (stream == null)
        throw new ArgumentNullException(nameof(stream));
        
    _svgImage = new SKSvg();
    _svgImage.Load(stream);
    InvalidateSurface();
}
```

### XAML Style
- Use proper indentation (2 or 4 spaces)
- Group related attributes
- Use meaningful names for x:Name
- Keep layouts simple and readable

### Example:
```xml
<Button x:Name="openButton"
        Text="Open SVG"
        BackgroundColor="{StaticResource Primary}"
        TextColor="White"
        Clicked="OnPickFileClicked" />
```

### File Organization
- One class per file (with exceptions for small nested classes)
- Match file names to class names
- Group related files in folders
- Keep platform-specific code in Platforms/ folder

## Testing

### Before Submitting
- [ ] Build succeeds on your platform
- [ ] Application runs without crashes
- [ ] New features work as expected
- [ ] Existing features still work
- [ ] No obvious performance issues
- [ ] Code follows style guidelines
- [ ] Documentation updated if needed

### Test Platforms
Try to test on multiple platforms if possible:
- Windows
- Android (emulator or device)
- iOS (simulator or device - macOS required)
- macOS Catalyst

### Adding Tests
While we don't currently have extensive unit tests, contributions of tests are welcome:

```csharp
[Fact]
public void LoadSvgFromStream_ValidStream_LoadsSuccessfully()
{
    // Arrange
    var stream = GetTestSvgStream();
    var renderer = new PatternRenderer();
    
    // Act
    renderer.LoadSvgFromStream(stream);
    
    // Assert
    Assert.NotNull(renderer.CurrentPattern);
}
```

## Commit Guidelines

### Commit Messages
Use clear, descriptive commit messages:

**Good:**
- "Add zoom slider control to main page"
- "Fix crash when loading invalid SVG files"
- "Update README with new build instructions"
- "Refactor SVG loading logic for better error handling"

**Avoid:**
- "Fixed stuff"
- "Update"
- "WIP"
- "Test commit"

### Commit Structure
```
[Type] Brief description (50 chars or less)

More detailed explanation if needed (wrap at 72 characters).
Explain the problem being solved and why this approach was chosen.

- Bullet points for multiple changes
- Reference issues with #123
- Include breaking changes warning if applicable
```

### Types
- **feat**: New feature
- **fix**: Bug fix
- **docs**: Documentation changes
- **style**: Code style changes (formatting, semicolons, etc.)
- **refactor**: Code refactoring
- **perf**: Performance improvements
- **test**: Adding tests
- **chore**: Maintenance tasks

## Pull Request Process

### PR Checklist
- [ ] Branch is up to date with main
- [ ] All tests pass
- [ ] Code follows style guidelines
- [ ] Documentation updated
- [ ] Commit messages are clear
- [ ] PR description explains changes
- [ ] Screenshots included for UI changes

### PR Description Template
```markdown
## Description
Brief description of the changes

## Type of Change
- [ ] Bug fix
- [ ] New feature
- [ ] Breaking change
- [ ] Documentation update

## Testing
- [ ] Tested on Windows
- [ ] Tested on Android
- [ ] Tested on iOS
- [ ] Tested on macOS

## Screenshots
(If applicable)

## Related Issues
Fixes #123
Relates to #456

## Additional Notes
Any other relevant information
```

### Review Process
1. Maintainers review your PR
2. Feedback may be provided
3. Make requested changes
4. PR is approved and merged
5. Your contribution is live!

## Areas for Contribution

### Good First Issues
- Documentation improvements
- UI polish and refinements
- Additional sample patterns
- Bug fixes
- Code comments and explanations

### Feature Ideas
- 3D pattern rendering
- Pattern editing tools
- Pattern library management
- Export functionality
- Print preview
- Measurement tools
- Pattern animation
- Collaborative features
- Cloud storage integration

### Advanced Contributions
- Performance optimizations
- Platform-specific features
- Accessibility improvements
- Internationalization (i18n)
- Automated testing
- CI/CD pipeline setup

## Questions?

- Open an issue for questions
- Tag with "question" label
- Check existing issues first
- Be patient waiting for responses

## Recognition

Contributors will be:
- Listed in CONTRIBUTORS.md
- Mentioned in release notes
- Appreciated in the community!

## License

By contributing, you agree that your contributions will be licensed under the MIT License.

## Resources

### Documentation
- [README.md](README.md) - Project overview
- [BUILD.md](BUILD.md) - Build instructions
- [ARCHITECTURE.md](ARCHITECTURE.md) - Technical details
- [QUICKSTART.md](QUICKSTART.md) - Quick setup

### External Resources
- [.NET MAUI Docs](https://docs.microsoft.com/dotnet/maui/)
- [SkiaSharp Docs](https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/graphics/skiasharp/)
- [C# Programming Guide](https://docs.microsoft.com/dotnet/csharp/)
- [Git Basics](https://git-scm.com/book/en/v2/Getting-Started-Git-Basics)

## Thank You!

Your contributions make this project better for everyone. We appreciate your time and effort!

---

*This document is a living document and may be updated as the project evolves.*
