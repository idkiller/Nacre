using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Nacre
{
    public interface ISource
    {
    }

    public struct SolidColor : ISource
    {
        public Color Color { get; set; }
    }

    public struct ImageSource : ISource
    {
        public Xamarin.Forms.ImageSource Image { get; set; }
    }

    public struct LinearGradient : ISource
    {
        public Angle Angle { get; set; }
        public IEnumerable<ColorPosition> Colors { get; set; }
    }

    public struct RadialGradient : ISource
    {
        public RadialGradientShape Shape { get; set; }
        public Position Center { get; set; }
        public IEnumerable<ColorPosition> Colors { get; set; }
    }

    public struct Background
    {
        public ISource Source { get; set; }
        public BackgroundOrigin Origin { get; set; }
        public Position Position { get; set; }
        public Repeat Repeat { get; set; }
        public global::Nacre.Size Size { get; set; }

        public Background(Background bg)
        {
            Source = bg.Source;
            Origin = bg.Origin;
            Position = bg.Position;
            Repeat = bg.Repeat;
            Size = bg.Size;
        }
    }

    public enum RadialGradientShape
    {
        Circle,
        Ellipse
    }
}