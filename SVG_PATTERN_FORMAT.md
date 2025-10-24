# SVG Pattern Format Guide

This document describes the SVG format used for cutting patterns in PatternCuttingRenderer and provides guidance for creating your own patterns.

## Overview

PatternCuttingRenderer uses standard SVG (Scalable Vector Graphics) format to represent cutting patterns. SVG is:
- **Vector-based**: Scales infinitely without quality loss
- **XML-based**: Human-readable and editable
- **Standards-compliant**: Works across platforms and tools
- **Feature-rich**: Supports paths, text, measurements, and metadata

## Basic SVG Structure

```xml
<?xml version="1.0" encoding="UTF-8"?>
<svg xmlns="http://www.w3.org/2000/svg" 
     width="800" 
     height="1000" 
     viewBox="0 0 800 1000">
  <!-- Pattern elements go here -->
</svg>
```

### Key Attributes
- **width/height**: Physical dimensions
- **viewBox**: Coordinate system (minX, minY, width, height)
- **xmlns**: XML namespace (required)

## Pattern Elements

### 1. Cutting Lines

Main pattern outlines typically use `<path>` elements:

```xml
<path d="M 200,100 L 200,150 C 200,200 220,250 250,280 Z"
      fill="none" 
      stroke="#2c3e50" 
      stroke-width="3"
      stroke-dasharray="10,5"/>
```

**Path Commands:**
- `M x,y`: Move to point
- `L x,y`: Line to point
- `C x1,y1 x2,y2 x,y`: Cubic Bezier curve
- `Z`: Close path

**Styling:**
- `fill`: Interior color (use "none" for outlines)
- `stroke`: Line color
- `stroke-width`: Line thickness
- `stroke-dasharray`: Dashed line pattern

### 2. Seam Lines

Mark seam allowances with different styles:

```xml
<line x1="250" y1="280" x2="250" y2="700" 
      stroke="#f39c12" 
      stroke-width="2"/>
```

### 3. Measurements

Add dimension lines with arrows:

```xml
<!-- Define arrow marker -->
<defs>
  <marker id="arrowhead" markerWidth="10" markerHeight="10" 
          refX="5" refY="5" orient="auto">
    <polygon points="0 0, 10 5, 0 10" fill="#d35400"/>
  </marker>
</defs>

<!-- Measurement line -->
<line x1="200" y1="400" x2="600" y2="400" 
      stroke="#d35400" 
      marker-end="url(#arrowhead)" 
      marker-start="url(#arrowhead)"/>
<text x="400" y="390" text-anchor="middle" fill="#d35400">400mm</text>
```

### 4. Notches

Mark alignment points with small shapes:

```xml
<polygon points="250,300 260,300 255,290" fill="#2c3e50"/>
```

### 5. Grain Lines

Indicate fabric grain direction:

```xml
<line x1="400" y1="200" x2="400" y2="800" 
      stroke="#000" 
      stroke-width="2"/>
<polygon points="400,200 395,215 405,215" fill="#000"/>
<text x="420" y="500">GRAIN</text>
```

### 6. Text and Labels

Add pattern information:

```xml
<text x="400" y="30" 
      font-family="Arial" 
      font-size="24" 
      text-anchor="middle" 
      fill="#333">
  Pattern Title
</text>
```

### 7. Pattern Information

Include metadata in a box:

```xml
<g id="info">
  <rect x="50" y="850" width="150" height="120" 
        fill="#ecf0f1" 
        stroke="#34495e" 
        stroke-width="2"/>
  <text x="60" y="875" font-size="12" font-weight="bold">
    Pattern Info:
  </text>
  <text x="60" y="895" font-size="11">Size: Medium</text>
  <text x="60" y="910" font-size="11">Scale: 1:1</text>
  <text x="60" y="925" font-size="11">Seam: 15mm</text>
</g>
```

## Color Conventions

Suggested color scheme for pattern elements:

- **Black (#000000)**: Main cutting lines
- **Blue (#3498db)**: Shoulder and construction lines
- **Red (#e74c3c)**: Special lines (neckline, armhole)
- **Green (#27ae60)**: Center lines and fold lines
- **Orange (#f39c12)**: Side seams
- **Purple (#9b59b6)**: Curve indicators
- **Gray (#95a5a6)**: Grid or background elements

## Pattern Metadata

Add custom metadata using SVG `<metadata>` tag:

```xml
<metadata>
  <pattern xmlns="http://example.com/pattern">
    <name>Shirt Front</name>
    <size>Medium</size>
    <scale>1:1</scale>
    <seamAllowance>15mm</seamAllowance>
    <fabric>Cotton</fabric>
    <pieces>1</pieces>
  </pattern>
</metadata>
```

## Best Practices

### 1. Coordinate System
- Use millimeters for garment patterns
- Place (0,0) at top-left corner
- Keep pattern centered in viewBox

### 2. Organization
- Group related elements with `<g>` tags
- Use meaningful `id` attributes
- Add comments for complex sections

```xml
<g id="body_outline">
  <!-- Body pattern elements -->
</g>
```

### 3. Layers
Organize pattern elements in logical groups:
- Background/grid
- Main cutting lines
- Seam allowances
- Notches and marks
- Text and labels
- Measurements

### 4. Styling
- Define reusable styles in `<defs>` section
- Use CSS classes for consistent styling
- Keep stroke widths proportional to pattern size

### 5. Accessibility
- Add `<title>` and `<desc>` elements
- Use semantic grouping
- Include text alternatives

```xml
<title>Shirt Front Pattern - Size Medium</title>
<desc>Main body pattern piece for a medium-sized shirt front panel</desc>
```

## Example Pattern Template

```xml
<?xml version="1.0" encoding="UTF-8"?>
<svg xmlns="http://www.w3.org/2000/svg" width="800" height="1000" viewBox="0 0 800 1000">
  <title>Pattern Name</title>
  <desc>Pattern description</desc>
  
  <defs>
    <!-- Define reusable elements -->
    <marker id="arrow" markerWidth="10" markerHeight="10" refX="5" refY="5" orient="auto">
      <polygon points="0 0, 10 5, 0 10" fill="#000"/>
    </marker>
  </defs>
  
  <!-- Background -->
  <rect width="800" height="1000" fill="#ffffff"/>
  
  <!-- Main Pattern -->
  <g id="main_pattern">
    <!-- Cutting lines -->
    <path d="M ... Z" fill="none" stroke="#000" stroke-width="3"/>
  </g>
  
  <!-- Markings -->
  <g id="markings">
    <!-- Notches, grain lines, etc. -->
  </g>
  
  <!-- Measurements -->
  <g id="measurements">
    <!-- Dimension lines and text -->
  </g>
  
  <!-- Information -->
  <g id="info">
    <!-- Pattern metadata box -->
  </g>
</svg>
```

## Creating Patterns

### Tools for Creating SVG Patterns

1. **Adobe Illustrator**: Professional vector graphics editor
2. **Inkscape**: Free, open-source vector graphics editor
3. **Affinity Designer**: Affordable professional tool
4. **Figma**: Web-based design tool
5. **Text Editor**: For manual SVG creation/editing

### Workflow

1. **Design Pattern**: Create pattern in your preferred tool
2. **Export as SVG**: Save with appropriate settings
3. **Optimize**: Remove unnecessary elements
4. **Add Metadata**: Include pattern information
5. **Test**: Load in PatternCuttingRenderer
6. **Refine**: Adjust based on display

## Converting Existing Patterns

### From DXF (CAD format)
1. Open in CAD software (AutoCAD, LibreCAD)
2. Export as SVG
3. Clean up and add styling

### From PDF
1. Open in vector graphics software
2. Select and copy pattern elements
3. Paste into new SVG document
4. Adjust and style appropriately

### From Raster Images
1. Import into vector tracing software (Inkscape, Illustrator)
2. Use trace/vectorize function
3. Clean up traced paths
4. Add proper styling and metadata

## Advanced Features

### Interactive Patterns
Add clickable regions with metadata:

```xml
<g id="piece1" onclick="showDetails('piece1')">
  <path d="..."/>
  <metadata>
    <details>Piece information</details>
  </metadata>
</g>
```

### Multi-Size Patterns
Include multiple sizes in one file:

```xml
<g id="size_small" display="none">
  <!-- Small size pattern -->
</g>
<g id="size_medium">
  <!-- Medium size pattern -->
</g>
<g id="size_large" display="none">
  <!-- Large size pattern -->
</g>
```

### Animated Instructions
Use SVG animation for assembly instructions:

```xml
<path d="...">
  <animate attributeName="stroke-opacity" 
           from="0" to="1" 
           dur="1s" 
           repeatCount="indefinite"/>
</path>
```

## Validation

### Check Your Pattern
- Valid XML syntax
- Proper namespace declarations
- Closed path commands
- Valid color codes
- Reasonable coordinate values
- No unsupported features

### Testing
- Test at different zoom levels
- Verify text readability
- Check line visibility
- Ensure measurements are correct
- Test on different platforms

## Resources

- [SVG Specification](https://www.w3.org/TR/SVG/)
- [MDN SVG Tutorial](https://developer.mozilla.org/en-US/docs/Web/SVG/Tutorial)
- [SVG Path Reference](https://developer.mozilla.org/en-US/docs/Web/SVG/Tutorial/Paths)
- [Inkscape Documentation](https://inkscape.org/doc/)

## Pattern Libraries

Consider sharing your patterns:
- GitHub repositories
- Pattern-making communities
- Fashion tech forums
- Open-source design projects

---

**Need help?** Check the sample pattern in `Resources/Raw/sample_pattern.svg` for a complete working example.
