namespace Nacre
{
    public struct Size
    {
        public AutoValue X { get; set; }
        public AutoValue Y { get; set; }

        public BackgroundSizePolicy Policy { get; set; }

        public Size(AutoValue x, AutoValue y, BackgroundSizePolicy policy)
        {
            X = x;
            Y = y;
            Policy = policy;
        }

        public Size(AutoValue x, AutoValue y) : this(x, y, BackgroundSizePolicy.Contain)
        {
        }

        public Size(AutoValue a) : this(a, AutoValue.Auto)
        {
        }
    }

    public struct AutoValue
    {
        public static AutoValue Auto = new AutoValue { Value = 0, isAuto = true };
        public double Value { get; set; }
        public bool isAuto { get; set; }

        public static implicit operator double(AutoValue a) => a.isAuto ? double.NaN : a.Value;
        public static explicit operator AutoValue(double d) => new AutoValue { Value = d, isAuto = false };
    }

    public enum BackgroundSizePolicy
    {
        Contain,
        Cover
    }
}