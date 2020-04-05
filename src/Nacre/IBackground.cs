using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Nacre
{
    public interface IBackground
    {
        BackgroundOrigin Origin { get; set; }
        Position Position { get; set; }
        Repeat Repeat { get; set; }
        global::Nacre.Size Size { get; set; }
    }

    public struct BackgroundColor : IBackground
    {
        public Color Color { get; set; }
        public BackgroundOrigin Origin { get; set; }
        public Position Position { get; set; }
        public Repeat Repeat { get; set; }
        public Size Size { get; set; }

        public override string ToString()
        {
            return $"BackgroundColor : #{Color.ToHex()}, {Enum.GetName(typeof(BackgroundOrigin), Origin)}, {Position}, {Repeat}, {Size}";
        }
    }

    public struct BackgroundImage : IBackground
    {
        public ImageSource Image { get; set; }
        public BackgroundOrigin Origin { get; set; }
        public Position Position { get; set; }
        public Repeat Repeat { get; set; }
        public Size Size { get; set; }
    }

    public struct LinearGradient : IBackground
    {
        public Angle Angle { get; set; }
        public IEnumerable<ColorPosition> Colors { get; set; }

        public BackgroundOrigin Origin { get; set; }
        public Position Position { get; set; }
        public Repeat Repeat { get; set; }
        public Size Size { get; set; }
    }

    public struct RadialGradient : IBackground
    {
        public RadialGradientShape Shape { get; set; }
        public Position Center { get; set; }
        public IEnumerable<ColorPosition> Colors { get; set; }

        public BackgroundOrigin Origin { get; set; }
        public Position Position { get; set; }
        public Repeat Repeat { get; set; }
        public Size Size { get; set; }
    }

    public enum RadialGradientShape
    {
        Circle,
        Ellipse
    }
}