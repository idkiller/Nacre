namespace Nacre
{
    public struct Angle
    {
        public static Angle ToTop = new Angle(0);
        public static Angle ToBottom = new Angle(180);
        public static Angle ToLeft = new Angle(270);
        public static Angle ToRight = new Angle(90);
        public static Angle ToTopLeft = new Angle(315);
        public static Angle ToTopRight = new Angle(45);
        public static Angle ToBottomLeft = new Angle(225);
        public static Angle ToBottomRight = new Angle(135);

        public double Degree { get; set; }
        public Angle(double degree)
        {
            Degree = degree;
        }

        public static implicit operator double(Angle a) => a.Degree;
        public static explicit operator Angle(double d) => new Angle(d);
        public static explicit operator Angle(int d) => new Angle(d);
    }
}