using Nacre;
using SkiaSharp;
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
            NacreView.BorderLeftColorProperty.PropertyName,
            NacreView.BorderLeftWidthProperty.PropertyName,
            NacreView.BorderLeftStyleProperty.PropertyName,
            NacreView.BorderTopColorProperty.PropertyName,
            NacreView.BorderTopWidthProperty.PropertyName,
            NacreView.BorderTopStyleProperty.PropertyName,
            NacreView.BorderRightColorProperty.PropertyName,
            NacreView.BorderRightWidthProperty.PropertyName,
            NacreView.BorderRightStyleProperty.PropertyName,
            NacreView.BorderBottomColorProperty.PropertyName,
            NacreView.BorderBottomWidthProperty.PropertyName,
            NacreView.BorderBottomStyleProperty.PropertyName
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

        void OnNacreChanged(bool initialize)
        {
            if (initialize) return;
            UpdateCanvasSize(formsGeometry.Left, formsGeometry.Top, formsGeometry.Width, formsGeometry.Height);
        }

        void OnPaintCanvas(object sender, SKPaintSurfaceEventArgs e)
        {
            var info = e.Info;
            var canvas = e.Surface.Canvas;

            if (Self.Shadows != null)
            {
                foreach (var shadow in Self.Shadows)
                {
                    canvas.DrawShadow(rectWithBorder, shadow);
                }
            }

            if (Self.Backgrounds != null)
            {
                foreach (var bg in Self.Backgrounds)
                {
                    canvas.DrawBackground(rectWithoutBorder, bg);
                }
            }

            canvas.DrawBorder(rectWithBorder, Self);

        }

        void UpdateCanvasSize(double x, double y, double width, double height)
        {
            double left = 0;
            double top = 0;
            double right = width;
            double bottom = height;
            if (Self.Nacre.Shadow != null)
            {
                foreach (var shadow in Self.Nacre.Shadow)
                {
                    if (!shadow.Inset)
                    {
                        var sl = shadow.OffsetX - shadow.BlurRadius - shadow.SpreadRadius;
                        var sr = shadow.OffsetX + shadow.BlurRadius + shadow.SpreadRadius + width;
                        var st = shadow.OffsetY - shadow.BlurRadius - shadow.SpreadRadius;
                        var sb = shadow.OffsetY + shadow.BlurRadius + shadow.SpreadRadius + height;
                        if (sl < left) left = sl;
                        if (st < top) top = st;
                        if (sr > right) right = sr;
                        if (sb > bottom) bottom = sb;
                    }
                }
            }

            var border = Self.Nacre.Border;

            rectWithBorder = new SKRect((float)-left, (float)-top,
                (float)(-left + width + border.Left.Width + border.Right.Width),
                (float)(-top + height + border.Top.Width + border.Bottom.Width));

            left -= border.Left.Width;
            top -= border.Top.Width;
            right += border.Right.Width;
            bottom += border.Bottom.Width;

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
