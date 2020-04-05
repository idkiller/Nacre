namespace Nacre
{
    public struct Position
    {
        public PositionValue Y { get; set; }
        public PositionValue X { get; set; }

        public Position(PositionValue x, PositionValue y)
        {
            Y = y;
            X = x;
        }

        public Position(PositionValue all) : this(all, all)
        {
        }

        public Position(IRelativeNumber xRelative, double xOffset, IRelativeNumber yRelative, double yOffset)
            : this (new PositionValue(xRelative, xOffset), new PositionValue(yRelative, yOffset))
        {
        }

        public Position(IRelativeNumber relative, double offset) : this(new PositionValue(relative, offset))
        {
        }

        public Position(IRelativeNumber relative) : this(new PositionValue(relative))
        {
        }

        public override string ToString()
        {
            return $"Position {{{X}, {Y}}}";
        }
    }

    public struct PositionValue
    {
        public INumber Value { get; set; }

        public double Offset { get; set; }

        public PositionValue(INumber number , double offset)
        {
            Value = number;
            Offset = offset;
        }

        public PositionValue(PositionKeyword relative, double offset) : this(relative.ToRelative(), offset)
        {
        }

        public PositionValue(IRelativeNumber relative) : this(relative, 0)
        {
        }

        public PositionValue(PositionKeyword relative) : this(relative, 0)
        {
        }

        public override string ToString()
        {
            return $"({(Value == null ? "undefined" : Value.ToString())} + {Offset})";
        }
    }

    public enum PositionKeyword
    {
        Top = 0, Bottom = 1,
        Left = 0, Right = 1,
        Center = 2
    }

    public static class PositionKeywordExtensions
    {
        public static Relative ToRelative(this PositionKeyword k)
        {
            switch ((int)k)
            {
                case 0:
                    return new Relative(0);
                case 1:
                    return new Relative(1);
                case 2:
                    return new Relative(0.5);
                default:
                    return new Relative(0);
            }
        }
    }
}