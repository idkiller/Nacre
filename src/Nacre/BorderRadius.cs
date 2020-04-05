namespace Nacre
{
    public struct BorderRadius
    {
        public INumber Horizontal { get; set; }
        public INumber Vertical { get; set; }

        public BorderRadius(INumber horizontal, INumber vertical)
        {
            Horizontal = horizontal;
            Vertical = vertical;
        }

        public BorderRadius(INumber all) : this(all, all)
        {
        }

        public override string ToString()
        {
            return $"{(Horizontal == null ? "undefined" : Horizontal.ToString())}, {(Vertical == null ? "undefined" : Vertical.ToString())}";
        }
    }
}