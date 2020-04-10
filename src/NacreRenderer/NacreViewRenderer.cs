using Nacre;
using SkiaSharp;
using SkiaSharp.Views;
using SkiaSharp.Views.Tizen;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Platform.Tizen.Native;

[assembly: ExportRenderer(typeof(NacreView), typeof(Nacre.Renderer.NacreViewRenderer))]

namespace Nacre.Renderer
{
    class NacreViewRenderer : LayoutRenderer
    {
        SKCanvasView canvasView;
        NacreView Self => Element as NacreView;

        ElmSharp.Rectangle debugBox;

        ElmSharp.Rect formsGeometry;

        public NacreViewRenderer() : base()
        {
            RegisterPropertyHandler(NacreView.BorderProperty, OnBorderChanged);
            RegisterPropertyHandler(NacreView.ShadowsProperty, OnShadowsChanged);
            RegisterPropertyHandler(NacreView.BackgroundsProperty, OnBackgroundsChanged);
        }

        public override ElmSharp.Rect GetNativeContentGeometry()
        {
            Console.WriteLine($"GetNativeContentGeometry :  {formsGeometry} ");
            return formsGeometry;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Layout> args)
        {
            base.OnElementChanged(args);
            if (args.NewElement != null)
            {
                Element.BatchCommitted += OnBatchCommited;
                initialize();
            }
            if (args.OldElement != null)
            {
                Element.BatchCommitted -= OnBatchCommited;
            }
        }

        protected override void UpdateLayout()
        {
            base.UpdateLayout();
            formsGeometry = Control.Geometry;
            Console.WriteLine($"formsGeometry = {formsGeometry}");
            UpdateCanvasSize(formsGeometry);
        }

        void OnBackgroundsChanged(bool initialize)
        {
            if (!initialize)
                UpdateCanvasSize(formsGeometry);
        }
        void OnShadowsChanged(bool initialize)
        {
            if (!initialize)
                UpdateCanvasSize(formsGeometry);
        }
        void OnBorderChanged(bool initialize)
        {
            if (!initialize)
                UpdateCanvasSize(formsGeometry);
        }
        void OnBatchCommited(object sender, EventArg<VisualElement> e) => UpdateCanvasSize(formsGeometry);

        void initialize()
        {
            debugBox = new ElmSharp.Rectangle(Forms.NativeParent)
            {
                Color = ElmSharp.Color.Purple
            };
            //debugBox.Show();
            Control.PackEnd(debugBox);
            Interop.evas_object_clip_unset(debugBox);

            canvasView = new SKCanvasView(Forms.NativeParent);
            canvasView.PaintSurface += OnPaintCanvas;
            canvasView.Show();
            Control.PackEnd(canvasView);
            Interop.evas_object_clip_unset(canvasView);

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                var content = (Element as ContentView).Content;
                if (content != null)
                {
                    var rect = Platform.GetRenderer(content).GetNativeContentGeometry();
                    Console.WriteLine($"content = {rect}");
                    Console.WriteLine($"content Element = {content.X}, {content.Y}, {content.Width}, {content.Height}");
                    Console.WriteLine($"formsGeometry = {formsGeometry}");
                    Console.WriteLine($"Element = {Element.X}, {Element.Y}, {Element.Width}, {Element.Height}");
                }
                return false;
            });
        }

        void UpdateCanvasSize(ElmSharp.Rect rect)
        {
            if (Element.Batched) return;

            double left = 0;
            double top = 0;
            double right = 0;
            double bottom = 0;

            foreach (var shadow in Self.Shadows)
            {
                if (shadow.Inset) continue;
                var spreadSize = shadow.BlurRadius * 2 + shadow.SpreadRadius;
                var sl = shadow.OffsetX - spreadSize;
                var sr = shadow.OffsetX + spreadSize;
                var st = shadow.OffsetY - spreadSize;
                var sb = shadow.OffsetY + spreadSize;
                if (left > sl) left = sl;
                if (top > st) top = st;
                if (right < sr) right = sr;
                if (bottom < sb) bottom = sb;
            }

            left -= Self.Border.LeftWidth;
            top -= Self.Border.TopWidth;
            right += Self.Border.RightWidth;
            bottom += Self.Border.BottomWidth;

            var canvasGeometry = new ElmSharp.Rect(rect.X + (int)left, rect.Y + (int)top, rect.Width + (int)right - (int)left, rect.Height + (int)bottom - (int)top);
            if (canvasView != null)
            {
                canvasView.Geometry = canvasGeometry;
                canvasView.Invalidate();
            }
            
            if (debugBox != null)
            {
                debugBox.Geometry = canvasView.Geometry;
            }
        }

        void OnPaintCanvas(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear();

            var left = formsGeometry.Left - canvasView.Geometry.Left;
            var top = formsGeometry.Top - canvasView.Geometry.Top;
            var path = new SKPath();
            var rect = new SKRect(left, top, left + formsGeometry.Width, top + formsGeometry.Height);

            var borderTop = new SKPath();
            var borderRight = new SKPath();
            var borderBottom = new SKPath();
            var borderLeft = new SKPath();

            var topLeft = new SKRect(rect.Left, rect.Top, rect.Left + (float)Self.Border.TopLeft.Width * 2, rect.Top + (float)Self.Border.TopLeft.Height * 2);
            var topRight = new SKRect(rect.Right - (float)Self.Border.TopRight.Width * 2, rect.Top, rect.Right, rect.Top + (float)Self.Border.TopRight.Height * 2);
            var bottomLeft = new SKRect(rect.Left, rect.Bottom - (float)Self.Border.BottomLeft.Height * 2, rect.Left + (float)Self.Border.BottomLeft.Width * 2, rect.Bottom);
            var bottomRight = new SKRect(rect.Right - (float)Self.Border.BottomRight.Width * 2, rect.Bottom - (float)Self.Border.BottomRight.Height * 2, rect.Right, rect.Bottom);

            path.ArcTo(topLeft, 180, 90, false);
            path.ArcTo(topRight, 270, 90, false);
            path.ArcTo(bottomRight, 0, 90, false);
            path.ArcTo(bottomLeft, 90, 90, false);
            path.Close();

            borderTop.ArcTo(topLeft, 225, 45, false);
            borderTop.ArcTo(topRight, 270, 45, false);

            borderLeft.ArcTo(bottomLeft, 135, 45, false);
            borderLeft.ArcTo(topLeft, 180, 45, false);

            borderRight.ArcTo(topRight, 315, 45, false);
            borderRight.ArcTo(bottomRight, 0, 45, false);

            borderBottom.ArcTo(bottomRight, 45, 45, false);
            borderBottom.ArcTo(bottomLeft, 90, 45, false);

            using (var paint = new SKPaint())
            {
                paint.IsAntialias = true;
                paint.Style = SKPaintStyle.StrokeAndFill;
                foreach (var shadow in Self.Shadows)
                {
                    if (shadow.Inset) continue;
                    canvas.Save();
                    canvas.ClipPath(path, SKClipOperation.Difference, true);
                    paint.StrokeWidth = (float)shadow.SpreadRadius;
                    paint.ImageFilter = SKImageFilter.CreateDropShadow((float)shadow.OffsetX, (float)shadow.OffsetY, (float)shadow.BlurRadius, (float)shadow.BlurRadius, shadow.Color.ToSK(),
                            SKDropShadowImageFilterShadowMode.DrawShadowOnly);
                    canvas.DrawPath(path, paint);
                    canvas.Restore();
                }
                paint.ImageFilter = null;

                foreach (var bg in Self.Backgrounds)
                {
                    switch (bg.Source)
                    {
                        case SolidColor solid:
                            paint.Style = SKPaintStyle.Fill;
                            paint.Color = solid.Color.ToSK();
                            canvas.DrawPath(path, paint);
                        break;
                        case LinearGradient linear:
                            path.GetBounds(out var bgRect);
                            paint.Style = SKPaintStyle.Fill;
                            paint.StrokeWidth = 0;

                            var rad = Math.PI / 180 * linear.Angle;
                            var cx = bgRect.Left + bgRect.Width / 2;
                            var cy = bgRect.Top + bgRect.Height / 2;
                            var tan = (float)Math.Tan(rad);
                            var cos = (float)Math.Cos(rad);

                            float x1 = 0, y1 = 0, x2 = 0, y2 = 0, px = 0, py = 0;
                            if (tan == 0)
                            {
                                x1 = bgRect.Left;
                                y1 = cy;
                                x2 = bgRect.Right;
                                y2 = cy;
                            }
                            else if (cos == 0)
                            {
                                x1 = cx;
                                y1 = bgRect.Top;
                                x2 = cx;
                                y2 = bgRect.Bottom;
                            }
                            else
                            {
                                if (tan > 0 && cos > 0)
                                {
                                    px = bgRect.Right;
                                    py = bgRect.Bottom;
                                }
                                else if (tan > 0 && cos < 0)
                                {
                                    px = bgRect.Left;
                                    py = bgRect.Top;
                                }
                                else if (tan < 0 && cos > 0)
                                {
                                    px = bgRect.Right;
                                    py = bgRect.Top;
                                }
                                else if (tan < 0 && cos < 0)
                                {
                                    px = bgRect.Left;
                                    py = bgRect.Bottom;
                                }
                                var p = (-tan * px + py + tan * cx - cy) / (tan * tan + 1);
                                x1 = p * tan + px;
                                y1 = py - p;
                                x2 = 2 * cx - x1;
                                y2 = 2 * cy - y1;
                            }

                            paint.StrokeWidth = 0;
                            paint.Style = SKPaintStyle.Fill;
                            paint.Shader = SKShader.CreateLinearGradient(
                                new SKPoint(x1, y1),
                                new SKPoint(x2, y2),
                                linear.Colors.Select(color => color.ToSK()).ToArray(),
                                linear.ColorPosition.ToArray(), SKShaderTileMode.Repeat);
                            canvas.DrawPath(path, paint);
                        break;
                    }
                }
                
                paint.Shader = null;
                paint.Style = SKPaintStyle.Stroke;
                foreach (var shadow in Self.Shadows)
                {
                    if (!shadow.Inset) continue;
                    canvas.Save();
                    paint.StrokeWidth = (float)shadow.SpreadRadius;
                    paint.ImageFilter = SKImageFilter.CreateDropShadow((float)shadow.OffsetX, (float)shadow.OffsetY, (float)shadow.BlurRadius, (float)shadow.BlurRadius, shadow.Color.ToSK(),
                            SKDropShadowImageFilterShadowMode.DrawShadowOnly);
                    canvas.ClipPath(path, antialias: true);
                    canvas.DrawPath(path, paint);
                    canvas.Restore();
                }

                paint.ImageFilter = null;

                paint.StrokeCap = SKStrokeCap.Square;
                paint.Style = SKPaintStyle.Stroke;

                if (Self.Border.TopWidth > 0)
                {
                    paint.Color = Self.Border.TopColor.ToSK();
                    paint.StrokeWidth = (float)Self.Border.TopWidth;
                    canvas.DrawPath(borderTop, paint);
                }

                if (Self.Border.RightWidth > 0)
                {
                    paint.Color = Self.Border.RightColor.ToSK();
                    paint.StrokeWidth = (float)Self.Border.RightWidth;
                    canvas.DrawPath(borderRight, paint);
                }

                if (Self.Border.BottomWidth > 0)
                {
                    paint.Color = Self.Border.BottomColor.ToSK();
                    paint.StrokeWidth = (float)Self.Border.BottomWidth;
                    canvas.DrawPath(borderBottom, paint);
                }

                if (Self.Border.LeftWidth > 0)
                {
                    paint.Color = Self.Border.LeftColor.ToSK();
                    paint.StrokeWidth = (float)Self.Border.LeftWidth;
                    canvas.DrawPath(borderLeft, paint);
                }
            }
        }
    }
}

/*
using System;
using SkiaSharp;

void Draw(SKCanvas canvas, int width, int height)
{
	var yellow = new SKColor(253, 200, 89, 255);

	//var bgp = new SKPaint { Color = yellow };
	//canvas.DrawRect(0, 0, width, height, bgp);
	
	var x = 50;
	var y = 50;
	var w = 150;
	var h = 110;
	var rx = 20;
	var ry = 20;

	var path = new SKPath();
	path.AddRoundRect(new SKRect(x, y, x + w, y + h), rx, ry);
	
	var pathTop = new SKPath();
	pathTop.AddArc(new SKRect(x, y, x + rx*2, y + ry*2), 225, 45);
	pathTop.LineTo(180, 50);
	pathTop.AddArc(new SKRect(160, 50, 200, 90), 270, 45);
	
	var pathRight = new SKPath();
	pathRight.AddArc(new SKRect(160, 50, 200, 90), 315, 45);
	pathRight.LineTo(200, 140);
	pathRight.AddArc(new SKRect(160, 120, 200, 160), 0, 45);
	
	var pathBottom = new SKPath();
	pathBottom.AddArc(new SKRect(160, 120, 200, 160), 45, 45);
	pathBottom.LineTo(70, 160);
	pathBottom.AddArc(new SKRect(50, 120, 90, 160), 90, 45);
	
	var pathLeft = new SKPath();
	pathLeft.AddArc(new SKRect(50, 120, 90, 160), 135, 45);
	pathLeft.LineTo(50, 70);
	pathLeft.AddArc(new SKRect(50, 50, 90, 90), 180, 45);
	
	var p = new SKPaint
	{
		IsAntialias = true,
		Color = yellow,
	};
	
	canvas.Save();
	
	p.Style = SKPaintStyle.Fill;
	canvas.DrawPath(path, p);
	
	p.StrokeWidth = 2;
	p.Style = SKPaintStyle.Stroke;
	
	p.Color = SKColors.Red;
	canvas.DrawPath(pathTop, p);
	
	p.Color = SKColors.Green;
	canvas.DrawPath(pathRight, p);
	
	p.Color = SKColors.Blue;
	canvas.DrawPath(pathBottom, p);
	
	p.Color = SKColors.Black;
	canvas.DrawPath(pathLeft, p);
	
	canvas.Restore();
}

*/

/*
using System;
using SkiaSharp;

void Draw(SKCanvas canvas, int width, int height)
{
	var yellow = new SKColor(253, 200, 89, 255);

	var bgp = new SKPaint { Color = yellow };
	canvas.DrawRect(0, 0, width, height, bgp);

	var path = new SKPath();
	path.AddRoundRect(new SKRect(50, 50, 200, 160), 20, 20);
	
	var p = new SKPaint
	{
		IsAntialias = true,
		Color = yellow,
	};
	
	canvas.Save();
	
	p.Style = SKPaintStyle.StrokeAndFill;
	p.ImageFilter = SKImageFilter.CreateDropShadow(5, 5, 5, 5, SKColors.Gray,
		SKDropShadowImageFilterShadowMode.DrawShadowOnly);
	canvas.DrawPath(path, p);
	
	p.Style = SKPaintStyle.StrokeAndFill;
	p.ImageFilter = SKImageFilter.CreateDropShadow(-5, -5, 5, 5, SKColors.White,
		SKDropShadowImageFilterShadowMode.DrawShadowOnly);
	canvas.DrawPath(path, p);
	
	p.ImageFilter = null;
	canvas.DrawPath(path, p);
	
	canvas.ClipPath(path, antialias: true);
	
	p.Style = SKPaintStyle.Stroke;
	p.ImageFilter = SKImageFilter.CreateDropShadow(5, 5, 5, 5, SKColors.Black,
		SKDropShadowImageFilterShadowMode.DrawShadowOnly);
	canvas.DrawPath(path, p);
	
	p.ImageFilter = SKImageFilter.CreateDropShadow(-5, -5, 5, 5, SKColors.White,
		SKDropShadowImageFilterShadowMode.DrawShadowOnly);
	canvas.DrawPath(path, p);
	
	canvas.Restore();
}

*/