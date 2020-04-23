using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public class LinearGradient : ISource, IEnumerable
    {
        List<Color> colors = new List<Color>();
        List<float> positions = new List<float>();
        bool calc = false;

        public double Angle { get; set; } = 0;
        public IEnumerable<Color> Colors => colors;
        public IEnumerable<float> ColorPosition
        {
            get
            {
                if (calc) return positions;

                float pos = 0;
                int last = 0;
                for (int i=0; i<positions.Count; i++)
                {
                    if (positions[i] > pos && positions[i] <= 1)
                    {
                        for (int j = last; j < i; j++)
                        {
                            positions[j] = positions[i] / (float)i * (float)j + pos;
                        }
                        pos = positions[i];
                        last = i + 1;
                    }
                }
                float remain = (positions.Count-last);
                int l = last;
                for (; l < positions.Count - 1; l++)
                {
                    positions[l] = (1.0f - pos) / remain * (float)(l - last) + pos;
                }
                if (l == positions.Count - 1)
                {
                    positions[positions.Count - 1] = 1;
                }
                calc = true;

                return positions;
            }
        }

        public LinearGradient(double angle)
        {
            Angle = angle;
        }

        public void Add(Color color) => Add(color, 2);
        public void Add(Color color, float from)
        {
            colors.Add(color);
            positions.Add(from);
            calc = false;
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var color in Colors)
            {
                yield return color;
            }
        }
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
        public Point Position { get; set; }
        public Repeat Repeat { get; set; }
        public BackgroundSizePolicy Policy { get; set; }
        public Size? Size { get; set; }

        public Background(Background bg)
        {
            Source = bg.Source;
            Position = bg.Position;
            Repeat = bg.Repeat;
            Policy = bg.Policy;
            Size = bg.Size;
        }
    }

    public enum RadialGradientShape
    {
        Circle,
        Ellipse
    }

    public enum BackgroundSizePolicy
    {
        Contain,
        Cover,
        Auto
    }
}