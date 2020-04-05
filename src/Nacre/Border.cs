using System.Text;
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

        public double Width {
            set
            {
                Left = new BorderValues(Left) { Width = value };
                Top = new BorderValues(Top) { Width = value };
                Right = new BorderValues(Right) { Width = value };
                Bottom = new BorderValues(Bottom) { Width = value };
            }
            get
            {
                return Left.Width;
            }
        }

        public Color Color
        {
            set
            {
                Left = new BorderValues(Left) { Color = value };
                Top = new BorderValues(Top) { Color = value };
                Right = new BorderValues(Right) { Color = value };
                Bottom = new BorderValues(Bottom) { Color = value };
            }
            get
            {
                return Left.Color;
            }
        }

        public LineStyle Style
        {
            set
            {
                Left = new BorderValues(Left) { Style = value };
                Top = new BorderValues(Top) { Style = value };
                Right = new BorderValues(Right) { Style = value };
                Bottom = new BorderValues(Bottom) { Style = value };
            }
            get
            {
                return Left.Style;
            }
        }

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

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"Border-Left : {Left}\n");
            sb.Append($"Border-Top : {Top}\n");
            sb.Append($"Border-Right : {Right}\n");
            sb.Append($"Border-Bottom : {Bottom}\n");
            sb.Append($"Border-Top-Left-Radius : {TopLeft}\n");
            sb.Append($"Border-Top-Right-Radius : {TopRight}\n");
            sb.Append($"Border-Bottom-Left-Radius : {BottomLeft}\n");
            sb.Append($"Border-Bottom-Right-Radius : {BottomRight}");
            return sb.ToString();
        }
    }
}