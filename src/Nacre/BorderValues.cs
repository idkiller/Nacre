using System;
using Xamarin.Forms;

namespace Nacre
{
    public struct BorderValues
    {
        public Color Color { get; set; }
        public double Width { get; set; }
        public LineStyle Style { get; set; }

        public override string ToString()
        {
            return $"#{Color.ToHex()}, {Width.ToString()}, {Enum.GetName(typeof(LineStyle), Style)}";
        }

        public BorderValues(double width, LineStyle style, Color color)
        {
            Width = width;
            Style = style;
            Color = color;
        }

        public BorderValues(BorderValues values) : this(values.Width, values.Style, values.Color)
        {
        }

        public BorderValues(double width, Color color) : this(width, LineStyle.Solid, color)
        {
        }

        public BorderValues(double width, LineStyle style) : this(width, style, Color.Default)
        {
        }

        public BorderValues(BorderWidthKeyword width, LineStyle style, Color color) : this(new Absolute((double)width), style, color)
        {
        }

        public BorderValues(LineStyle style, Color color) : this(BorderWidthKeyword.Medium, style, color)
        {
        }

        public BorderValues(double width) : this(width, LineStyle.Solid)
        {
        }

        public BorderValues(LineStyle style) : this(style, Color.Default)
        {
        }
    }

    public enum BorderWidthKeyword
    {
        Thin = 1,
        Medium = 3,
        Thick = 5 
    }
}