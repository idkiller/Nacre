using System;

namespace Nacre
{
    public struct Repeat
    {
        public RepeatKeyword X { get; set; }
        public RepeatKeyword Y { get; set; }

        public Repeat (RepeatKeyword x, RepeatKeyword y)
        {
            X = x;
            Y = y;
        }

        public Repeat(RepeatKeyword all) : this(all, all)
        {
        }

        public override string ToString()
        {
            return $"Repeat {{{Enum.GetName(typeof(RepeatKeyword), X)}, {Enum.GetName(typeof(RepeatKeyword), Y)}}}";
        }
    }

    public enum RepeatKeyword
    {
        Repeat,
        Space,
        Round,
        NoRepeat
    }
}