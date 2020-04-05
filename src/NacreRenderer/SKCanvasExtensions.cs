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
        public static void DrawBackground(this SKCanvas canvas, SKRect rect, IBackground bg)
        {
            switch (bg)
            {
                case BackgroundColor color:
                    DrawBackgroundColor(canvas, rect, color);
                    break;
                case BackgroundImage image:
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

        private static void DrawBackgroundImage(SKCanvas canvas, SKRect rect, BackgroundImage image)
        {
            throw new NotImplementedException();
        }

        private static void DrawBackgroundColor(SKCanvas canvas, SKRect rect, BackgroundColor color)
        {
            using (var paint = new SKPaint())
            {
                paint.Style = SKPaintStyle.Fill;
                paint.Color = color.Color.ToSK();
                canvas.DrawRect(rect, paint);
            }
        }

        public static void DrawShadow(this SKCanvas canvas, SKRect rect, Shadow shadow)
        {
            using (var paint = new SKPaint())
            {
                paint.Color = SKColors.Blue;
                paint.ImageFilter = SKImageFilter.CreateDropShadow(
                    (float)shadow.OffsetX,
                    (float)shadow.OffsetY,
                    (float)shadow.BlurRadius,
                    (float)shadow.BlurRadius,
                    shadow.Color.ToSK(),
                    SKDropShadowImageFilterShadowMode.DrawShadowAndForeground);

                rect.Inflate((float)shadow.SpreadRadius, (float)shadow.SpreadRadius);

                canvas.DrawRect(rect, paint);
            }
        }

        public static void DrawBorder(this SKCanvas canvas, SKRect rect, Border border)
        {
            DrawBorder(canvas,
                rect.Left + (float)(border.Left.Width / 2),
                rect.Top,
                rect.Left + (float)(border.Left.Width / 2),
                rect.Bottom,
                border.Left);
            DrawBorder(canvas,
                rect.Left,
                rect.Top + (float)(border.Top.Width / 2),
                rect.Right,
                rect.Top + (float)(border.Top.Width / 2),
                border.Top);
            DrawBorder(canvas,
                rect.Right - (float)(border.Right.Width / 2),
                rect.Top,
                rect.Right - (float)(border.Right.Width / 2),
                rect.Bottom,
                border.Right);
            DrawBorder(canvas,
                rect.Left,
                rect.Bottom - (float)(border.Bottom.Width / 2),
                rect.Right,
                rect.Bottom - (float)(border.Bottom.Width / 2),
                border.Bottom);
        }

        static void DrawBorder(SKCanvas canvas, float x0, float y0, float x1, float y1, BorderValues border)
        {
            if (border.Width == 0) return;
            using (var paint = new SKPaint())
            {
                paint.Style = SKPaintStyle.Stroke;
                paint.StrokeWidth = (float)border.Width;
                paint.Color = border.Color.ToSK();
                //paint.PathEffect = border.Style.ToSK();
                canvas.DrawLine(x0, y0, x1, y1, paint);
            }
        }

        static SKColor ToSK(this Xamarin.Forms.Color color)
        {
            return new SKColor(
                        (byte)(color.R * 255),
                        (byte)(color.G * 255),
                        (byte)(color.B * 255),
                        (byte)(color.A * 255));
        }

        static SKPathEffect ToSK(this LineStyle style)
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
