using Xamarin.Forms;

namespace Nacre
{
    public struct Shadow
    {
        public bool Inset { get; set; }
        public double OffsetX { get; set; }
        public double OffsetY { get; set; }
        public double BlurRadius { get; set; }
        public double SpreadRadius { get; set; }
        public Color Color { get; set; }

        public Shadow(bool inset, double x, double y, double blur, double spread, Color color)
        {
            Inset = inset;
            OffsetX = x;
            OffsetY = y;
            BlurRadius = blur;
            SpreadRadius = spread;
            Color = color;
        }

        public Shadow(bool inset, double x, double y, double blur, double spread) : this(inset, x, y, blur, spread, Color.Default)
        {
        }

        public Shadow(bool inset, double x, double y, double blur) : this(inset, x, y, blur, 0)
        {
        }

        public Shadow(bool inset, double x, double y, double blur, Color color) : this(inset, x, y, blur, 0, color)
        {
        }

        public Shadow(bool inset, double x, double y, Color color) : this(inset, x, y, 0, 0, color)
        {
        }

        public Shadow(bool inset, double x, double y) : this(inset, x, y, 0)
        {
        }

        public Shadow(double x, double y, double blur, double spread, Color color) : this(false, x, y, blur, spread, color)
        {
        }

        public Shadow(double x, double y, double blur, double spread) : this(x, y, blur, spread, Color.Default)
        {
        }

        public Shadow(double x, double y, double blur) : this(x, y, blur, 0)
        {
        }

        public Shadow(double x, double y, double blur, Color color) : this(x, y, blur, 0, color)
        {
        }

        public Shadow(double x, double y, Color color) : this(x, y, 0, 0, color)
        {
        }

        public Shadow(double x, double y) : this(x, y, 0)
        {
        }
    }
}