using Xamarin.Forms;

namespace Nacre
{
    public interface IBackground
    {
        BackgroundOrigin Origin { get; set; }
        Position Position { get; set; }
        Repeat Repeat { get; set; }
        Nacre.Size Size { get; set; }
    }

    public struct BackgroundColor : IBackground
    {
        public Color Color { get; set; }
        public BackgroundOrigin Origin { get; set; }
        public Position Position { get; set; }
        public Repeat Repeat { get; set; }
        public Size Size { get; set; }
    }

    public struct BackgroundImage : IBackground
    {
        public ImageSource Image { get; set; }
        public BackgroundOrigin Origin { get; set; }
        public Position Position { get; set; }
        public Repeat Repeat { get; set; }
        public Size Size { get; set; }
    }
}