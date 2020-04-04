using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Nacre
{
    public class NacreView : ContentView
    {
        public static readonly BindableProperty NacreProperty = BindableProperty.Create(nameof(Nacre), typeof(Nacre), typeof(NacreView));

        public Nacre Nacre { get => (Nacre)GetValue(NacreProperty); set => SetValue(NacreProperty, value); }
    }
}
