using System;

namespace Nacre
{
    public interface INumber
    {
        double Value { get; set; }
    }

    public interface IRelativeNumber : INumber
    {
        double RelateTo(double scalar);
    }

    public struct Absolute : INumber
    {
        public double Value { get; set; }

        public Absolute(double v) => Value = v;

        public static implicit operator double(Absolute a) => a.Value;
        public static explicit operator Absolute(double d) => new Absolute(d);

        public override string ToString()
        {
            return $"{Value}";
        }
    }

    public struct Relative : IRelativeNumber
    {
        public double Value { get; set; }

        public Relative(double v) => Value = v;
        public Relative(Percent p) => Value = p.Value / 100;

        public double RelateTo(double scalar)
        {
            return scalar * Value;
        }

        public static explicit operator Relative(double d) => new Relative(d);
        public static explicit operator Relative(Percent p) => new Relative(p);

        public override string ToString()
        {
            return $"{Value}r";
        }
    }

    public struct Percent : IRelativeNumber
    {
        public static Percent Zero = new Percent { Value = 0 };
        public static Percent Half = new Percent { Value = 50 };
        public static Percent Hundred = new Percent { Value = 100 };
        
        public double Value { get; set; }

        public Percent(double v) => Value = v;
        public Percent(Relative r) => Value = r.Value * 100;

        public double RelateTo(double scalar)
        {
            return scalar * Value / 100;
        }

        public static explicit operator Percent(Relative r) => new Percent(r);
        public static explicit operator Percent(double d) => new Percent(d);

        public override string ToString()
        {
            return $"{Value}%";
        }
    }

    public static class NumberExtensions
    {
        public static INumber ToNumber(string str)
        {
            int pid = str.LastIndexOf('%');
            if (pid >= 0)
            {
                str = str.Substring(0, str.Length - 1);
            }
            
            if (!double.TryParse(str, out var n))
            {
                throw new FormatException($"Invalid format : {str}");
            }

            if (pid < 0)
                return new Absolute(n);
            else
                return new Percent(n);
        }
    }
}