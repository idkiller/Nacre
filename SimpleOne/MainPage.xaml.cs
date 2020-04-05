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

            var inlay = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            inlay.Children.Add(new Label { Text = "Nacre!" });

            var nacreView = new NacreView
            {
                WidthRequest = 100,
                HeightRequest = 80,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Nacre = new Nacre.Nacre
                {
                    Background = new IBackground[] { new BackgroundColor{
                        Color = Color.AntiqueWhite
                    }},
                    Shadow = new[] { new Shadow(10, 10, 2, Color.Black) },
                    Border = new Border(2, LineStyle.Solid, Color.Black)
                    /*
                    Border = new Border
                    {
                        Left = new BorderValues(5, LineStyle.Solid, Color.Brown),
                        Top = new BorderValues(10, LineStyle.Solid, Color.Yellow),
                        Right = new BorderValues(3, LineStyle.Solid, Color.DarkGray),
                        Bottom = new BorderValues(8, LineStyle.Solid, Color.DarkOrange),
                        TopLeft = new BorderRadius(new Percent(10)),
                        TopRight = new BorderRadius((Absolute)10, (Absolute)5),
                        BottomLeft = new BorderRadius((Relative)0.5)
                    }
                    */
                },
                Content = inlay
            };
            layout.Children.Add(nacreView);

            var tapGesture = new TapGestureRecognizer();
            var borderWidth = nacreView.Nacre.Border.Width;
            var shadowOffsetX = nacreView.Nacre.Shadow.First().OffsetX;
            var shadowOffsetY = nacreView.Nacre.Shadow.First().OffsetY;
            tapGesture.Tapped += (s, e) =>
            {
                var anim = new Animation((v) =>
                {
                    var nacre = nacreView.Nacre;
                    
                    var border = nacre.Border;
                    border.Width = borderWidth * v;

                    var shadow = nacre.Shadow.First();
                    shadow.OffsetX = shadowOffsetX * v;
                    shadow.OffsetY = shadowOffsetY * v;
                    
                    nacre.Border = border;
                    nacre.Shadow = new[] { shadow };

                    Console.WriteLine(nacre);

                    nacreView.Nacre = nacre;
                }, 1, 1.5, Easing.SpringOut);
                anim.Commit(nacreView, "nacreAnimation", 16, 250);
            };

            nacreView.GestureRecognizers.Add(tapGesture);
        }
    }
}