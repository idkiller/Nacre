using System.Text;
using Xamarin.Forms;

namespace Nacre
{
    public struct Border
    {
        public double LeftWidth { get; set; }
        public double TopWidth { get; set; }
        public double RightWidth { get; set; }
        public double BottomWidth { get; set; }

        public LineStyle LeftStyle { get; set; }
        public LineStyle TopStyle { get; set; }
        public LineStyle RightStyle { get; set; }
        public LineStyle BottomStyle { get; set; }

        public Color LeftColor { get; set; }
        public Color TopColor { get; set; }
        public Color RightColor { get; set; }
        public Color BottomColor { get; set; }

        public Xamarin.Forms.Size BottomLeft { get; set; }
        public Xamarin.Forms.Size BottomRight { get; set; }
        public Xamarin.Forms.Size TopLeft { get; set; }
        public Xamarin.Forms.Size TopRight { get; set; }

        public Border(double width, LineStyle style, Color color, Xamarin.Forms.Size radius)
        {
            LeftWidth = TopWidth = RightWidth = BottomWidth = width;
            LeftStyle = TopStyle = RightStyle = BottomStyle = style;
            LeftColor = TopColor = RightColor = BottomColor = color;
            BottomLeft = BottomRight = TopLeft = TopRight = radius;
        }

        public Border(double width, LineStyle style, Color color) : this(width, style, color, default(Xamarin.Forms.Size))
        {
        }
        public Border(Size radius) : this(0, default(LineStyle), default(Color), radius)
        {
        }

        public Border(Border border)
        {
            LeftWidth = border.LeftWidth;
            TopWidth = border.TopWidth;
            RightWidth = border.RightWidth;
            BottomWidth = border.BottomWidth;

            LeftStyle = border.LeftStyle;
            TopStyle = border.TopStyle;
            RightStyle = border.RightStyle;
            BottomStyle = border.BottomStyle;

            LeftColor = border.LeftColor;
            TopColor = border.TopColor;
            RightColor = border.RightColor;
            BottomColor = border.BottomColor;

            BottomLeft = border.BottomLeft;
            BottomRight = border.BottomRight;
            TopLeft = border.TopLeft;
            TopRight = border.TopRight;
        }

        public Border ChangeWidth(double top, double right, double bottom, double left)
        {
            var border = new Border(this);
            border.TopWidth = top;
            border.RightWidth = right;
            border.BottomWidth = bottom;
            border.LeftWidth = left;
            return border;
        }

        public Border ChangeWidth(double top, double horizontal, double bottom) => ChangeWidth(top, horizontal, bottom, horizontal);
        public Border ChangeWidth(double vertical, double horizontal) => ChangeWidth(vertical, horizontal, vertical, horizontal);
        public Border ChangeWidth(double width) => ChangeWidth(width, width, width, width);
    }
}