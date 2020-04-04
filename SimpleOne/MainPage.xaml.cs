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

            layout.Children.Add(new NacreView
            {
                WidthRequest = 80,
                HeightRequest = 50,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Nacre = new Nacre.Nacre
                {
                    Shadow = new[] { new Shadow(10, 10, 5, Color.Red) },
                    Border = new Border(10, new Color(1, 1, 0))
                },
                Content = new BoxView { BackgroundColor = Color.Green }
            });
        }
    }
}