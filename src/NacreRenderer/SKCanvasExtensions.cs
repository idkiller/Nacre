using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nacre;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

namespace Nacre.Renderer
{
    public static class SKCanvasExtensions
    {
        static Dictionary<Xamarin.Forms.ImageSource, SKBitmap> BitmapCache = new Dictionary<Xamarin.Forms.ImageSource, SKBitmap>();

        public static void DrawBackground(this SKCanvas canvas, SKRect rect, SKPath path, Background bg)
        {
            switch (bg.Source)
            {
                case SolidColor color:
                    DrawBackground(canvas, rect, path, color);
                    break;
                case ImageSource image:
                    DrawBackground(canvas, rect, path, image, bg);
                    break;
                case LinearGradient linearGradient:
                    DrawBackground(canvas, rect, path, linearGradient);
                    break;
                case RadialGradient radialGradient:
                    DrawBackground(canvas, rect, path, radialGradient);
                    break;
            }
        }

        private static void DrawBackground(this SKCanvas canvas, SKRect rect, SKPath path, RadialGradient radialGradient)
        {
            throw new NotImplementedException();
        }

        private static void DrawBackground(SKCanvas canvas, SKRect rect, SKPath path, LinearGradient linear)
        {
            using (var paint = new SKPaint())
            {
                var rad = Math.PI / 180 * linear.Angle;
                var cx = rect.MidX;
                var cy = rect.MidY;
                var tan = (float)Math.Tan(rad);
                var cos = (float)Math.Cos(rad);

                float x1 = 0, y1 = 0, x2 = 0, y2 = 0, px = 0, py = 0;
                if (tan == 0)
                {
                    x1 = rect.Left;
                    y1 = cy;
                    x2 = rect.Right;
                    y2 = cy;
                }
                else if (cos == 0)
                {
                    x1 = cx;
                    y1 = rect.Top;
                    x2 = cx;
                    y2 = rect.Bottom;
                }
                else
                {
                    if (tan > 0 && cos > 0)
                    {
                        px = rect.Right;
                        py = rect.Bottom;
                    }
                    else if (tan > 0 && cos < 0)
                    {
                        px = rect.Left;
                        py = rect.Top;
                    }
                    else if (tan < 0 && cos > 0)
                    {
                        px = rect.Right;
                        py = rect.Top;
                    }
                    else if (tan < 0 && cos < 0)
                    {
                        px = rect.Left;
                        py = rect.Bottom;
                    }
                    var p = (-tan * px + py + tan * cx - cy) / (tan * tan + 1);
                    x1 = p * tan + px;
                    y1 = py - p;
                    x2 = 2 * cx - x1;
                    y2 = 2 * cy - y1;
                }

                paint.Style = SKPaintStyle.Fill;
                paint.Shader = SKShader.CreateLinearGradient(
                    new SKPoint(x1, y1),
                    new SKPoint(x2, y2),
                    linear.Colors.Select(color => color.ToSK()).ToArray(),
                    linear.ColorPosition.ToArray(), SKShaderTileMode.Repeat);
                canvas.DrawPath(path, paint);
            }
        }

        private static void DrawBackground(SKCanvas canvas, SKRect rect, SKPath path, ImageSource source, Background bg)
        {
            var img = source.Image;
            if (img == null) return;

            if (!BitmapCache.TryGetValue(img, out var bitmap))
            {
                return;
            }

            SKSize imgSize = new SKSize();
            SKSize size = bg.Size.HasValue ? rect.Size : new SKSize((float)bg.Size.Value.Width, (float)bg.Size.Value.Height);
            switch (bg.Policy)
            {
                case BackgroundSizePolicy.Auto:
                    imgSize.Width = bitmap.Width;
                    imgSize.Height = bitmap.Height;
                    break;
                case BackgroundSizePolicy.Contain:
                    if (size.Width > size.Height)
                    {
                        imgSize.Width = size.Width / size.Height * bitmap.Height;
                        imgSize.Height = bitmap.Height;
                    }
                    else
                    {
                        imgSize.Width = bitmap.Width;
                        imgSize.Height = size.Height / size.Width * bitmap.Width;
                    }
                    break;
                case BackgroundSizePolicy.Cover:
                    if (size.Width < size.Height)
                    {
                        imgSize.Width = size.Width / size.Height * bitmap.Height;
                        imgSize.Height = bitmap.Height;
                    }
                    else
                    {
                        imgSize.Width = bitmap.Width;
                        imgSize.Height = size.Height / size.Width * bitmap.Width;
                    }
                    break;
            }

            using (var paint = new SKPaint())
            {
            }
        }

        private static void DrawBackground(SKCanvas canvas, SKRect rect, SKPath path, SolidColor color)
        {
            using (var paint = new SKPaint())
            {
                paint.Style = SKPaintStyle.Fill;
                paint.Color = color.Color.ToSK();
                canvas.DrawRect(rect, paint);
            }
        }

        public static void DrawBackground(this SKCanvas canvas, SKRect rect, bool inset, double offsetX, double offsetY, double blurRadius, double spreadRadius, Xamarin.Forms.Color color)
        {
            Console.WriteLine($"shadow rect = {rect}, {offsetX}, {offsetY}");

            using (var paint = new SKPaint())
            {
                paint.Color = SKColors.Blue;
                paint.ImageFilter = SKImageFilter.CreateDropShadow(
                    (float)offsetX,
                    (float)offsetY,
                    (float)blurRadius,
                    (float)blurRadius,
                    color.ToSK(),
                    SKDropShadowImageFilterShadowMode.DrawShadowAndForeground);

                rect.Inflate((float)spreadRadius, (float)spreadRadius);
                canvas.DrawRect(rect, paint);
            }
        }

        public static void DrawBackground(this SKCanvas canvas, SKRect rect, double width, LineStyle style, Xamarin.Forms.Color color, BorderRadius round)
        {
            if (width == 0) return;
            using (var paint = new SKPaint())
            {
                paint.Style = SKPaintStyle.Stroke;
                paint.StrokeWidth = (float)width;
                paint.Color = color.ToSK();
                //paint.PathEffect = border.Style.ToSK();
                var half = (float)(width / 2);
                rect.Left -= half;
                rect.Top -= half;
                rect.Right += half;
                rect.Bottom += half;

                float x = round.Horizontal != null ? (float)(round.Horizontal is IRelativeNumber rx ? rx.RelateTo(rect.Width) : round.Horizontal.Value) : 0;
                float y = round.Vertical != null ? (float)(round.Vertical is IRelativeNumber ry ? ry.RelateTo(rect.Height) : round.Vertical.Value) : 0;

                canvas.DrawRoundRect(rect, new SKSize(x, y), paint);
            }
        }

        public static SKColor ToSK(this Xamarin.Forms.Color color)
        {
            return new SKColor(
                        (byte)(color.R * 255),
                        (byte)(color.G * 255),
                        (byte)(color.B * 255),
                        (byte)(color.A * 255));
        }

        public static SKPathEffect ToSK(this LineStyle style)
        {
            switch (style)
            {
                case LineStyle.Solid:
                    return null;
                default:
                    return null;
            }
        }

        static SKBitmap LoadBitmap(Xamarin.Forms.ImageSource src)
        {
            switch (src)
            {
                case FileImageSource file:
                    break;
                case StreamImageSource stream:
                    break;
                case UriImageSource uri:
                    break;
            }
            return null;
        }

        static SKBitmap LoadBitmap(Xamarin.Forms.FileImageSource src)
        {
            if (string.IsNullOrEmpty(src.File))
            {
                var imgPath = ResourcePath.GetPath(src.File);
            }
        }
    }
}
