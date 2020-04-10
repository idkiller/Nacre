using Xamarin.Forms;

namespace Nacre
{
    public struct ColorPosition
    {
        public Color Color { get; set; }
        public double From { get; set; }
        public double To { get; set; }

        public ColorPosition(Color color, double from, double to)
        {
            Color = color;
            From = from;
            To = to;
        }

        public ColorPosition(Color color, double to) : this(color, 0, to)
        {
        }

        public ColorPosition(Color color) : this(color, 1)
        {
        }
    }
}