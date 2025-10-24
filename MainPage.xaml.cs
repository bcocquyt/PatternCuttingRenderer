using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;
using Svg.Skia;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PatternCuttingRenderer;

public partial class MainPage : ContentPage
{
	private SKSvg? _svgImage;
	private float _scale = 1.0f;
	private SKPoint _translate = new SKPoint(0, 0);
	private SKPoint? _lastTouchPoint;

	public MainPage()
	{
		InitializeComponent();
		LoadSampleSvg();
	}

	private void LoadSampleSvg()
	{
		// Load the sample SVG from resources
		var assembly = GetType().Assembly;
		var resourceName = "PatternCuttingRenderer.Resources.Raw.sample_pattern.svg";
		
		using (var stream = assembly.GetManifestResourceStream(resourceName))
		{
			if (stream != null)
			{
				LoadSvgFromStream(stream);
			}
		}
	}

	private void LoadSvgFromStream(Stream stream)
	{
		_svgImage = new SKSvg();
		_svgImage.Load(stream);
		
		// Reset view
		_scale = 1.0f;
		_translate = new SKPoint(0, 0);
		
		skiaCanvas.InvalidateSurface();
		StatusLabel.Text = "SVG loaded successfully";
	}

	private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
	{
		var canvas = e.Surface.Canvas;
		canvas.Clear(SKColors.White);

		if (_svgImage?.Picture == null)
			return;

		// Calculate the scale to fit the SVG in the canvas
		var canvasWidth = e.Info.Width;
		var canvasHeight = e.Info.Height;
		var svgWidth = _svgImage.Picture.CullRect.Width;
		var svgHeight = _svgImage.Picture.CullRect.Height;

		var scaleX = canvasWidth / svgWidth;
		var scaleY = canvasHeight / svgHeight;
		var autoScale = Math.Min(scaleX, scaleY) * 0.9f; // 90% to leave some margin

		// Apply transformations
		canvas.Translate(_translate.X + canvasWidth / 2, _translate.Y + canvasHeight / 2);
		canvas.Scale(_scale * autoScale);
		canvas.Translate(-svgWidth / 2, -svgHeight / 2);

		// Draw the SVG
		canvas.DrawPicture(_svgImage.Picture);
	}

	private async void OnPickFileClicked(object sender, EventArgs e)
	{
		try
		{
			var fileResult = await FilePicker.PickAsync(new PickOptions
			{
				FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
				{
					{ DevicePlatform.iOS, new[] { "public.svg-image" } },
					{ DevicePlatform.Android, new[] { "image/svg+xml" } },
					{ DevicePlatform.WinUI, new[] { ".svg" } },
					{ DevicePlatform.macOS, new[] { "svg" } },
				}),
				PickerTitle = "Select an SVG file"
			});

			if (fileResult != null)
			{
				using (var stream = await fileResult.OpenReadAsync())
				{
					LoadSvgFromStream(stream);
				}
			}
		}
		catch (Exception ex)
		{
			StatusLabel.Text = $"Error loading file: {ex.Message}";
		}
	}

	private void OnZoomInClicked(object sender, EventArgs e)
	{
		_scale *= 1.2f;
		skiaCanvas.InvalidateSurface();
	}

	private void OnZoomOutClicked(object sender, EventArgs e)
	{
		_scale /= 1.2f;
		skiaCanvas.InvalidateSurface();
	}

	private void OnResetViewClicked(object sender, EventArgs e)
	{
		_scale = 1.0f;
		_translate = new SKPoint(0, 0);
		skiaCanvas.InvalidateSurface();
	}

	private void OnCanvasTouch(object sender, SKTouchEventArgs e)
	{
		switch (e.ActionType)
		{
			case SKTouchAction.Pressed:
				_lastTouchPoint = e.Location;
				e.Handled = true;
				break;

			case SKTouchAction.Moved:
				if (_lastTouchPoint.HasValue)
				{
					var delta = e.Location - _lastTouchPoint.Value;
					_translate.X += delta.X;
					_translate.Y += delta.Y;
					_lastTouchPoint = e.Location;
					skiaCanvas.InvalidateSurface();
					e.Handled = true;
				}
				break;

			case SKTouchAction.Released:
			case SKTouchAction.Cancelled:
				_lastTouchPoint = null;
				e.Handled = true;
				break;
		}
	}
}
