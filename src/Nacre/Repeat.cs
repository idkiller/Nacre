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
    }

    public enum RepeatKeyword
    {
        Repeat,
        Space,
        Round,
        NoRepeat
    }
}