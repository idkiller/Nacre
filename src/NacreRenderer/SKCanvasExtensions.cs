using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nacre;
using SkiaSharp;

namespace Nacre.Renderer
{
    public static class SKCanvasExtensions
    {
        public static void DrawBackground(this SKCanvas canvas, SKRect rect, Background bg)
        {
            switch (bg.Source)
            {
                case SolidColor color:
                    DrawBackgroundColor(canvas, rect, color);
                    break;
                case ImageSource image:
                    DrawBackgroundImage(canvas, rect, image);
                    break;
                case LinearGradient linearGradient:
                    DrawLinearGradient(canvas, rect, linearGradient);
                    break;
                case RadialGradient radialGradient:
                    DrawRadialGradient(canvas, rect, radialGradient);
                    break;
            }
        }

        private static void DrawRadialGradient(SKCanvas canvas, SKRect rect, RadialGradient radialGradient)
        {
            throw new NotImplementedException();
        }

        private static void DrawLinearGradient(SKCanvas canvas, SKRect rect, LinearGradient linearGradient)
        {
            throw new NotImplementedException();
        }

        private static void DrawBackgroundImage(SKCanvas canvas, SKRect rect, ImageSource image)
        {
            throw new NotImplementedException();
        }

        private static void DrawBackgroundColor(SKCanvas canvas, SKRect rect, SolidColor color)
        {
            using (var paint = new SKPaint())
            {
                paint.Style = SKPaintStyle.Fill;
                paint.Color = color.Color.ToSK();
                canvas.DrawRect(rect, paint);
            }
        }

        public static void DrawShadow(this SKCanvas canvas, SKRect rect, bool inset, double offsetX, double offsetY, double blurRadius, double spreadRadius, Xamarin.Forms.Color color)
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

        public static void DrawBorder(this SKCanvas canvas, SKRect rect, double width, LineStyle style, Xamarin.Forms.Color color, BorderRadius round)
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
    }
}
