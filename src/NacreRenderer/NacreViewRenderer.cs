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

        double extraLeft, extraTop, extraRight, extraBottom;

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
            UpdateCanvasSize(Control.Geometry.Left, Control.Geometry.Top, Control.Geometry.Width, Control.Geometry.Height);
        }

        void OnPaintCanvas(object sender, SKPaintSurfaceEventArgs e)
        {
            var info = e.Info;
            var canvas = e.Surface.Canvas;

            var left = (canvasView.Geometry.Width - formsGeometry.Width) / 2;
            var top = (canvasView.Geometry.Height - formsGeometry.Height) / 2;

            var rect = new SKRect((float)extraLeft, (float)extraTop, (float)(formsGeometry.Width + extraLeft), (float)(formsGeometry.Height + extraBottom));

            if (Self.Nacre.Shadow != null)
            {
                foreach (var shadow in Self.Nacre.Shadow)
                {
                    canvas.DrawShadow(rect, shadow);
                }
            }

            var border = Self.Nacre.Border;
            canvas.DrawBorder(rect, Self.Nacre.Border);

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

            left -= border.Left.WIdth;
            top -= border.Top.WIdth;
            right += border.Right.WIdth;
            bottom += border.Bottom.WIdth;

            extraLeft = left;
            extraRight = right;
            extraTop = top;
            extraBottom = bottom;


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
