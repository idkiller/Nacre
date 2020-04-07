using System;

namespace Nacre
{
    /*
    public struct Size
    {
        public AutoValue X { get; set; }
        public AutoValue Y { get; set; }

        public Size(AutoValue x, AutoValue y)
        {
            X = x;
            Y = y;
        }
        public Size(AutoValue a) : this(a, AutoValue.Auto)
        {
        }

        public override string ToString()
        {
            return $"Size {{{X}, {Y}}}";
        }
    }

    public struct AutoValue
    {
        public static AutoValue Auto = new AutoValue { Value = 0, IsAuto = true };
        public double Value { get; set; }
        public bool IsAuto { get; set; }

        public static implicit operator double(AutoValue a) => a.IsAuto ? double.NaN : a.Value;
        public static explicit operator AutoValue(double d) => new AutoValue { Value = d, IsAuto = false };

        public override string ToString()
        {
            return IsAuto ? "Auto" : Value.ToString();
        }
    }
    */
}