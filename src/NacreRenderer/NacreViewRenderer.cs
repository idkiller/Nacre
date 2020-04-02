using Nacre;
using Nacre.Renderer;
using SkiaSharp;
using SkiaSharp.Views.Tizen;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Platform.Tizen.Native;

[assembly: ExportRenderer(typeof(NacreView), typeof(NacreViewRenderer))]

namespace Nacre.Renderer
{
    class NacreViewRenderer : LayoutRenderer
    {
        SKCanvasView canvas;
        NacreView Self => Element as NacreView;

        protected override void OnElementChanged(ElementChangedEventArgs<Layout> e)
        {
            base.OnElementChanged(e);

            initialize();
        }

        void initialize()
        {
            canvas = new SKCanvasView(Control);
            canvas.Show();

            Control.PackEnd(canvas);
        }
    }
}
