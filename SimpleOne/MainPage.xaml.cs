﻿using System;
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
                BackgroundSource = new SolidColor { Color = Color.Salmon },
                ShadowOffsetX = 10,
                ShadowOffsetY = 10,
                ShadowBlurRadius = 2,
                ShadowColor = Color.Red,
                BorderWidth = 10,
                BorderStyle = LineStyle.Solid,
                BorderColor = Color.Green,
                BoxRadius = new BorderRadius(new Percent(20)),
                Content = inlay
            };
            layout.Children.Add(nacreView);

            var tapGesture = new TapGestureRecognizer();
            var borderWidth = nacreView.BorderWidth;
            var shadowOffsetX = nacreView.ShadowOffsetY;
            var shadowOffsetY = nacreView.ShadowOffsetY;
            tapGesture.Tapped += (s, e) =>
            {
                var anim = new Animation((v) =>
                {
                    nacreView.BatchBegin();
                    nacreView.BorderWidth = borderWidth * v;
                    nacreView.ShadowOffsetX = shadowOffsetX * v;
                    nacreView.ShadowOffsetY = shadowOffsetY * v;
                    nacreView.BatchCommit();
                }, 1, 1.5, Easing.SpringOut);
                anim.Commit(nacreView, "nacreAnimation", 16, 250);
            };

            nacreView.GestureRecognizers.Add(tapGesture);
        }
    }
}