using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace Nacre
{
    public class NacreView : ContentView
    {
        public static readonly BindableProperty BackgroundsProperty = BindableProperty.Create(nameof(Backgrounds), typeof(IEnumerable<Background>), typeof(NacreView),
            defaultValueCreator: bindable => new ObservableCollection<Background>());
        public static readonly BindableProperty ShadowsProperty = BindableProperty.Create(nameof(Shadows), typeof(IEnumerable<Shadow>), typeof(NacreView),
            defaultValueCreator: bindable => new ObservableCollection<Shadow>());
        public static readonly BindableProperty BorderProperty = BindableProperty.Create(nameof(Border), typeof(Border), typeof(NacreView));

        public IEnumerable<Background> Backgrounds { get => (IEnumerable<Background>)GetValue(BackgroundsProperty); set => SetValue(BackgroundsProperty, value);  }
        public IEnumerable<Shadow> Shadows { get => (IEnumerable<Shadow>)GetValue(ShadowsProperty); set => SetValue(ShadowsProperty, value);  }
        public Border Border { get => (Border)GetValue(BorderProperty); set => SetValue(BorderProperty, value); }
    }
}
