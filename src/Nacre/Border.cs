using Xamarin.Forms;

namespace Nacre
{
    public struct Border
    {
        public BorderValues Left { get; set; }
        public BorderValues Top { get; set; }
        public BorderValues Right { get; set; }
        public BorderValues Bottom { get; set; }

        public BorderRadius? BottomLeft { get; set; }
        public BorderRadius? BottomRight { get; set; }
        public BorderRadius? TopLeft { get; set; }
        public BorderRadius? TopRight { get; set; }

        public Border(double width, LineStyle style, Color color)
        {
            Left = new BorderValues(width, style, color);
            Top = new BorderValues(width, style, color);
            Right = new BorderValues(width, style, color);
            Bottom = new BorderValues(width, style, color);

            BottomLeft = null;
            BottomRight = null;
            TopLeft = null;
            TopRight = null;
        }

        public Border(double width, LineStyle style) : this(width, style, Color.Default)
        {
        }

        public Border(double width, Color color) : this(width, LineStyle.Solid, color)
        {
        }

        public Border(double width) : this(width, LineStyle.Solid, Color.Default)
        {
        }
    }
}