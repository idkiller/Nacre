using Xamarin.Forms;

namespace Nacre
{
    public struct ColorPosition
    {
        public Color Color { get; set; }
        public INumber From { get; set; }
        public INumber To { get; set; }

        public ColorPosition(Color color, INumber from, INumber to)
        {
            Color = color;
            From = from;
            To = to;
        }

        public ColorPosition(Color color, INumber to) : this(color, null, to)
        {
        }

        public ColorPosition(Color color) : this(color, null)
        {
        }
    }
}