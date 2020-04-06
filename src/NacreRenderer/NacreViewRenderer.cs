using Nacre;
using SkiaSharp;
using SkiaSharp.Views;
using SkiaSharp.Views.Tizen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Platform.Tizen.Native;

[assembly: ExportRenderer(typeof(NacreView), typeof(Nacre.Renderer.NacreViewRenderer))]

namespace Nacre.Renderer
{
    class NacreViewRenderer : LayoutRenderer
    {
        HashSet<string> NacrePropertyNames = new HashSet<string>
        {
            NacreView.BorderColorProperty.PropertyName,
            NacreView.BorderWidthProperty.PropertyName,
            NacreView.BorderStyleProperty.PropertyName,
            NacreView.BoxRadiusProperty.PropertyName,
            NacreView.BackgroundSourceProperty.PropertyName,
            NacreView.BackgroundOriginProperty.PropertyName,
            NacreView.BackgroundPositionProperty.PropertyName,
            NacreView.BackgroundRepeatProperty.PropertyName,
            NacreView.BackgroundSizeProperty.PropertyName,
            NacreView.ShadowInsetProperty.PropertyName,
            NacreView.ShadowOffsetXProperty.PropertyName,
            NacreView.ShadowOffsetYProperty.PropertyName,
            NacreView.ShadowBlurRadiusProperty.PropertyName,
            NacreView.ShadowSpreadRadiusProperty.PropertyName,
            NacreView.ShadowColorProperty.PropertyName,
        };
        SKCanvasView canvasView;
        NacreView Self => Element as NacreView;

        ElmSharp.Rectangle debugBox;

        ElmSharp.Rect formsGeometry;

        SKRect rectWithBorder;
        SKRect rectWithoutBorder;

        public override ElmSharp.Rect GetNativeContentGeometry()
        {
            return formsGeometry;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Element.Batched && NacrePropertyNames.Contains(e.PropertyName))
            {
                return;
            }
            base.OnElementPropertyChanged(sender, e);
            if (NacrePropertyNames.Contains(e.PropertyName))
            {
                OnNacreChanged();
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Layout> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                Element.BatchCommitted += OnBatchCommited;
                initialize();
            }
        }

        protected override void UpdateLayout()
        {
            base.UpdateLayout();
            formsGeometry = Control.Geometry;
            UpdateCanvasSize(formsGeometry.Left, formsGeometry.Top, formsGeometry.Width, formsGeometry.Height);
        }

        void OnBatchCommited(object sender, EventArg<VisualElement> e)
        {
            UpdateCanvasSize(formsGeometry.Left, formsGeometry.Top, formsGeometry.Width, formsGeometry.Height);
        }

        void initialize()
        {
            debugBox = new ElmSharp.Rectangle(Forms.NativeParent)
            {
                Color = ElmSharp.Color.White
            };
            debugBox.Show();
            Control.PackEnd(debugBox);

            canvasView = new SKCanvasView(Forms.NativeParent);
            canvasView.PaintSurface += OnPaintCanvas;
            canvasView.Show();
            Control.PackEnd(canvasView);

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                var content = (Element as ContentView).Content;
                var rect = Platform.GetRenderer(content).GetNativeContentGeometry();
                System.Console.WriteLine($"debug = {debugBox.Geometry}");
                System.Console.WriteLine($"contetn = {rect}");

                foreach (var c in Control.Children)
                {
                    Console.WriteLine($"  -- {c.GetType().FullName}");
                }
                return false;
            });
        }

        void OnNacreChanged()
        {
            UpdateCanvasSize(formsGeometry.Left, formsGeometry.Top, formsGeometry.Width, formsGeometry.Height);
        }

        void OnPaintCanvas(object sender, SKPaintSurfaceEventArgs e)
        {
            try
            {
                var info = e.Info;
                var canvas = e.Surface.Canvas;

                using (var paint = new SKPaint())
                {
                    var rect = rectWithBorder;
                    var shadowRect = rectWithBorder;
                    paint.ImageFilter = SKImageFilter.CreateDropShadow(
                        (float)Self.ShadowOffsetX,
                        (float)Self.ShadowOffsetY,
                        (float)Self.ShadowBlurRadius,
                        (float)Self.ShadowBlurRadius,
                        Self.ShadowColor.ToSK(),
                        SKDropShadowImageFilterShadowMode.DrawShadowAndForeground);

                    shadowRect.Inflate((float)Self.ShadowSpreadRadius, (float)Self.ShadowSpreadRadius);

                    /*
                    var half = (float)(Self.BorderWidth / 2);
                    shadowRect.Left += half;
                    shadowRect.Top += half;
                    shadowRect.Right -= half;
                    shadowRect.Bottom -= half;
                    */

                    float x = Self.BoxRadius.Horizontal != null ? (float)(Self.BoxRadius.Horizontal is IRelativeNumber rx ? rx.RelateTo(rectWithoutBorder.Width) : Self.BoxRadius.Horizontal.Value) : 0;
                    float y = Self.BoxRadius.Vertical != null ? (float)(Self.BoxRadius.Vertical is IRelativeNumber ry ? ry.RelateTo(rectWithoutBorder.Height) : Self.BoxRadius.Vertical.Value) : 0;

                    canvas.DrawRoundRect(shadowRect, new SKSize(x, y), paint);

                    paint.ImageFilter = null;

                    switch (Self.BackgroundSource)
                    {
                        case SolidColor color:
                            paint.Style = SKPaintStyle.Fill;
                            paint.Color = color.Color.ToSK();
                            break;
                        case ImageSource image:
                            break;
                        case LinearGradient linearGradient:
                            break;
                        case RadialGradient radialGradient:
                            break;
                    }

                    canvas.DrawRoundRect(rect, new SKSize(x, y), paint);

                    /*
                    rect.Left -= half;
                    rect.Top -= half;
                    rect.Right += half;
                    rect.Bottom += half;
                    */

                    paint.Style = SKPaintStyle.Stroke;
                    paint.StrokeWidth = (float)Self.BorderWidth;
                    paint.Color = Self.BorderColor.ToSK();
                    //paint.PathEffect = Self.BorderStyle.ToSK();

                    canvas.DrawRoundRect(rect, new SKSize(x, y), paint);
                }

                /*
                canvas.DrawShadow(rectWithBorder, Self.ShadowInset, Self.ShadowOffsetX, Self.ShadowOffsetY, Self.ShadowBlurRadius, Self.ShadowSpreadRadius, Self.ShadowColor);
                canvas.DrawBackground(rectWithoutBorder, new Background
                {
                    Source = Self.BackgroundSource,
                    Origin = Self.BackgroundOrigin,
                    Position = Self.BackgroundPosition,
                    Repeat = Self.BackgroundRepeat,
                    Size = Self.BackgroundSize
                });

                canvas.DrawBorder(rectWithoutBorder, Self.BorderWidth, Self.BorderStyle, Self.BorderColor, Self.BoxRadius);
                */

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        void UpdateCanvasSize(double x, double y, double width, double height)
        {
            double left = 0;
            double top = 0;
            double right = width;
            double bottom = height;

            if (!Self.ShadowInset)
            {
                var sl = Self.ShadowOffsetX - Self.ShadowBlurRadius - Self.ShadowSpreadRadius;
                var sr = Self.ShadowOffsetX + Self.ShadowBlurRadius + Self.ShadowSpreadRadius + width;
                var st = Self.ShadowOffsetY - Self.ShadowBlurRadius - Self.ShadowSpreadRadius;
                var sb = Self.ShadowOffsetY + Self.ShadowBlurRadius + Self.ShadowSpreadRadius + height;
                if (sl < left) left = sl;
                if (st < top) top = st;
                if (sr > right) right = sr;
                if (sb > bottom) bottom = sb;
            }
            rectWithBorder = new SKRect((float)(-left + Self.BorderWidth / 2),
                (float)(-top + Self.BorderWidth / 2),
                (float)(-left + width + Self.BorderWidth),
                (float)(-top + height + Self.BorderWidth));

            left += Self.BorderWidth;
            top += Self.BorderWidth;
            right += Self.BorderWidth;
            bottom += Self.BorderWidth;

            rectWithoutBorder = new SKRect((float)-left, (float)-top, (float)(-left + width), (float)(-top + height));

            System.Console.WriteLine($"-- {width}, {height} / {left}, {top}, {right}, {bottom}");
            var geometry = new Rectangle(x + left, y + top, right - left, bottom - top).ToPixel();
            Control.Geometry = geometry;
            if (canvasView != null)
            {
                canvasView.Geometry = geometry;
                System.Console.WriteLine($"-- size : {canvasView.Geometry}");
                System.Console.WriteLine($"-- canvas size : {canvasView.CanvasSize}");
                canvasView.Invalidate();
            }
            
            if (debugBox != null)
            {
                debugBox.Geometry = canvasView.Geometry;
            }
        }

    }
}
