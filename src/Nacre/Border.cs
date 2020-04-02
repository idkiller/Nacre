namespace Nacre
{
    public struct Border
    {
        public BorderValues Left { get; set; }
        public BorderValues Top { get; set; }
        public BorderValues Right { get; set; }
        public BorderValues Bottom { get; set; }

        public BorderRadius BottomLeft { get; set; }
        public BorderRadius BottomRight { get; set; }
        public BorderRadius TopLeft { get; set; }
        public BorderRadius TopRight { get; set; }
    }
}