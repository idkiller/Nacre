using Nacre;
using SkiaSharp;
using SkiaSharp.Views.Tizen;
using System;
using System.ComponentModel;
using Xamarin.Forms;
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

        SKRect rectWithBorder;
        SKRect rectWithoutBorder;

        public NacreViewRenderer()
        {
            RegisterPropertyHandler(NacreView.NacreProperty, OnNacreChanged);
        }


        protected override void OnElementChanged(ElementChangedEventArgs<Layout> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                initialize();
            }
        }

        protected override void UpdateLayout()
        {
            base.UpdateLayout();
            formsGeometry = Control.Geometry;
            UpdateCanvasSize(formsGeometry.Left, formsGeometry.Top, formsGeometry.Width, formsGeometry.Height);
        }

        public override ElmSharp.Rect GetNativeContentGeometry()
        {
            return formsGeometry;
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
            UpdateCanvasSize(formsGeometry.Left, formsGeometry.Top, formsGeometry.Width, formsGeometry.Height);
        }

        void OnPaintCanvas(object sender, SKPaintSurfaceEventArgs e)
        {
            var info = e.Info;
            var canvas = e.Surface.Canvas;

            if (Self.Nacre.Shadow != null)
            {
                foreach (var shadow in Self.Nacre.Shadow)
                {
                    canvas.DrawShadow(rectWithBorder, shadow);
                }
            }

            if (Self.Nacre.Background != null)
            {
                foreach (var bg in Self.Nacre.Background)
                {
                    canvas.DrawBackground(rectWithoutBorder, bg);
                }
            }

            var border = Self.Nacre.Border;
            canvas.DrawBorder(rectWithBorder, Self.Nacre.Border);

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
