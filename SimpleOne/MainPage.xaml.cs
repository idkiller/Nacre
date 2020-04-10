using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.Wearable.CircularUI.Forms;

using Nacre;

namespace SimpleOne
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : CirclePage
    {
        public MainPage()
        {
            InitializeComponent();

            try
            {
                initNacre();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }

        void initNacre()
        {
            var nacreView = new NacreView
            {
                Backgrounds = new [] { new Background { Source = new SolidColor { Color = Color.Salmon } } },
                Shadows = new [] {
                    new Shadow { OffsetX = 10, OffsetY = 10, BlurRadius = 5, Color = Color.Red },
                    new Shadow { OffsetX = -10, OffsetY = -10, BlurRadius = 5, Color = Color.Blue }
                },
                Border = new Border(5, LineStyle.Solid, Color.Green) 
                {
                    LeftColor = Color.Yellow,
                    TopColor = Color.Green,
                    RightColor = Color.Blue,
                    BottomColor = Color.Black,
                    TopLeft = new Size(20, 20),
                    TopRight = new Size(50, 40),
                    BottomLeft = new Size(90, 50),
                    BottomRight = new Size(40, 40)
                },
                Content = new Label { Text = "Nacre!", TextColor = Color.White, FontSize = 10, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand}
            };
            AbsoluteLayout.SetLayoutBounds(nacreView, new Rectangle(80, 80, 180, 100));
            AbsoluteLayout.SetLayoutFlags(nacreView, AbsoluteLayoutFlags.None);
            layout.Children.Add(nacreView);
            var npsm = new NacreView
            {
                Backgrounds = new [] { new Background { Source = new SolidColor { Color = Color.FromRgb(253, 200, 89) }}},
                Shadows = new [] {
                    new Shadow { OffsetX = 5, OffsetY = 5, BlurRadius = 10, Color = Color.Black},
                    new Shadow { OffsetX = -5, OffsetY = -5, BlurRadius = 10, Color = Color.White},
                    new Shadow { Inset = true, OffsetX = 5, OffsetY = 5, BlurRadius = 5, Color = Color.Black},
                    new Shadow { Inset = true, OffsetX = -5, OffsetY = -5, BlurRadius = 5, Color = Color.White},
                },
                Border = new Border(new Size(25, 25)),
                Content = new Label { Text = "NPSM!", TextColor = Color.White, FontSize = 4, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand}
            };
            AbsoluteLayout.SetLayoutBounds(npsm, new Rectangle(60, 200, 50, 50));
            AbsoluteLayout.SetLayoutFlags(npsm, AbsoluteLayoutFlags.None);
            layout.Children.Add(npsm);             

            var gradient = new NacreView
            {
                Backgrounds = new [] {
                    new Background { Source = new LinearGradient(127)
                    {
                        {Color.FromRgba(0, 255, 0, 200)},
                        {Color.FromRgba(0, 255, 0, 0), 0.771f},
                    }},
                    new Background { Source = new LinearGradient(336)
                    {
                        {Color.FromRgba(0, 0, 255, 200)},
                        {Color.FromRgba(0, 0, 255, 0), 0.771f},
                    }},
                    new Background { Source = new LinearGradient(217)
                    {
                        {Color.FromRgba(255, 0, 0, 200)},
                        {Color.FromRgba(255, 0, 0, 0), 0.771f},
                    }}
                },
                Border = new Border(new Size(10, 10)),
                Shadows = new [] {
                    new Shadow { OffsetX = 3, OffsetY = 3, BlurRadius = 3, Color = Color.Black}
                },
                Content = new Label { Text = "Gradient!", TextColor = Color.White, FontSize = 5, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand}
            };
            AbsoluteLayout.SetLayoutBounds(gradient, new Rectangle(140, 220, 120, 80));
            AbsoluteLayout.SetLayoutFlags(gradient, AbsoluteLayoutFlags.None);
            layout.Children.Add(gradient);

            var tapGesture1 = new TapGestureRecognizer();
            tapGesture1.Tapped += (s, e) =>
            {
                var w = nacreView.Border.TopWidth;
                new Animation {
                    { 0, 0.5, new Animation (v => nacreView.Border = nacreView.Border.ChangeWidth(v), 5, 10)},
                    { 0.5, 1, new Animation (v => nacreView.Border = nacreView.Border.ChangeWidth(v), 10, 5)}
                }.Commit (nacreView, "nacreAnimations", 16, 250);
            };
            nacreView.GestureRecognizers.Add(tapGesture1);

            var tapGesture2 = new TapGestureRecognizer();
            tapGesture2.Tapped += (s, e) =>
            {
                new Animation {
                    { 0, 0.5, new Animation (v => npsm.Shadows = new [] {
                        new Shadow { OffsetX = 5, OffsetY = 5, BlurRadius = 2 * v, Color = Color.Black},
                        new Shadow { OffsetX = -5, OffsetY = -5, BlurRadius = 2 * v, Color = Color.White},
                        new Shadow { Inset = true, OffsetX = 5, OffsetY = 5, BlurRadius = v, Color = Color.Black},
                        new Shadow { Inset = true, OffsetX = -5, OffsetY = -5, BlurRadius = v, Color = Color.White},
                    }, 5, 10)},
                    { 0.5, 1, new Animation (v => npsm.Shadows = new [] {
                        new Shadow { OffsetX = 5, OffsetY = 5, BlurRadius = 2 * v, Color = Color.Black},
                        new Shadow { OffsetX = -5, OffsetY = -5, BlurRadius = 2 * v, Color = Color.White},
                        new Shadow { Inset = true, OffsetX = 5, OffsetY = 5, BlurRadius = v, Color = Color.Black},
                        new Shadow { Inset = true, OffsetX = -5, OffsetY = -5, BlurRadius = v, Color = Color.White},
                    }, 10, 5)}
                }.Commit(npsm, "npsmTouch", 16, 250);
            };
            npsm.GestureRecognizers.Add(tapGesture2);
        }
    }
}