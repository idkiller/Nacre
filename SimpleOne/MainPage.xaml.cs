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
            var bb = new BoxView { Color = Color.Brown };
            AbsoluteLayout.SetLayoutBounds(bb, new Rectangle(80, 80, 180, 100));
            AbsoluteLayout.SetLayoutFlags(bb, AbsoluteLayoutFlags.None);
            layout.Children.Add(bb);

            
            var inlay = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            inlay.Children.Add(new Label { Text = "Nacre!" });

            var nacreView = new NacreView
            {
                Backgrounds = new [] { new Background { Source = new SolidColor { Color = Color.Salmon } } },
                Shadows = new [] { new Shadow { OffsetX = 10, OffsetY = 10, BlurRadius = 5, Color = Color.Red } },
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
                Content = inlay
            };
            AbsoluteLayout.SetLayoutBounds(nacreView, new Rectangle(80, 80, 180, 100));
            AbsoluteLayout.SetLayoutFlags(nacreView, AbsoluteLayoutFlags.None);
            layout.Children.Add(nacreView);

            var npsmContent = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            npsmContent.Children.Add(new Label
            {
                Text = "NPSM",
                FontSize=3,
                TextColor = Color.Black
            });
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
                Content = npsmContent
            };
            AbsoluteLayout.SetLayoutBounds(npsm, new Rectangle(60, 200, 50, 50));
            AbsoluteLayout.SetLayoutFlags(npsm, AbsoluteLayoutFlags.None);
            layout.Children.Add(npsm);

            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += (s, e) =>
            {
                var w = nacreView.Border.TopWidth;
                var anim = new Animation((v) =>
                {
                    nacreView.BatchBegin();
                    nacreView.Border = nacreView.Border.ChangeWidth(w * v);
                    nacreView.BatchCommit();
                }, 1, 1.5, Easing.SpringOut);
                anim.Commit(nacreView, "nacreAnimation", 16, 250);
            };

            nacreView.GestureRecognizers.Add(tapGesture);
        }
    }
}