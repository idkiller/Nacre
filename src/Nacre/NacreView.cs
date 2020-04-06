using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace Nacre
{
    public class NacreView : ContentView
    {
     
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(NacreView));
        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(NacreView));
        public static readonly BindableProperty BorderStyleProperty = BindableProperty.Create(nameof(BorderStyle), typeof(LineStyle), typeof(NacreView));
        public static readonly BindableProperty BorderLeftColorProperty = BindableProperty.Create(nameof(BorderLeftColor), typeof(Color), typeof(NacreView));
        public static readonly BindableProperty BorderLeftWidthProperty = BindableProperty.Create(nameof(BorderLeftWidth), typeof(double), typeof(NacreView));
        public static readonly BindableProperty BorderLeftStyleProperty = BindableProperty.Create(nameof(BorderLeftStyle), typeof(LineStyle), typeof(NacreView));
        public static readonly BindableProperty BorderTopColorProperty = BindableProperty.Create(nameof(BorderTopColor), typeof(Color), typeof(NacreView));
        public static readonly BindableProperty BorderTopWidthProperty = BindableProperty.Create(nameof(BorderTopWidth), typeof(double), typeof(NacreView));
        public static readonly BindableProperty BorderTopStyleProperty = BindableProperty.Create(nameof(BorderTopStyle), typeof(LineStyle), typeof(NacreView));
        public static readonly BindableProperty BorderRightColorProperty = BindableProperty.Create(nameof(BorderRightColor), typeof(Color), typeof(NacreView));
        public static readonly BindableProperty BorderRightWidthProperty = BindableProperty.Create(nameof(BorderRightWidth), typeof(double), typeof(NacreView));
        public static readonly BindableProperty BorderRightStyleProperty = BindableProperty.Create(nameof(BorderRightStyle), typeof(LineStyle), typeof(NacreView));
        public static readonly BindableProperty BorderBottomColorProperty = BindableProperty.Create(nameof(BorderBottomColor), typeof(Color), typeof(NacreView));
        public static readonly BindableProperty BorderBottomWidthProperty = BindableProperty.Create(nameof(BorderBottomWidth), typeof(double), typeof(NacreView));
        public static readonly BindableProperty BorderBottomStyleProperty = BindableProperty.Create(nameof(BorderBottomStyle), typeof(LineStyle), typeof(NacreView));
        public static readonly BindablePropertyKey BackgroundsPropertyKey = BindableProperty.CreateReadOnly(nameof(Backgrounds), typeof(NacreView), typeof(IList<IBackground>), default(IList<IBackground>),
            defaultValueCreator: bindable => new ObservableCollection<IBackground>());
        public static readonly BindableProperty BackgroundsProperty = BackgroundsPropertyKey.BindableProperty;
        public static readonly BindablePropertyKey ShadowsPropertyKey = BindableProperty.CreateReadOnly(nameof(Shadows), typeof(IEnumerable<Shadow>), typeof(NacreView), typeof(IList<Shadow>),
            defaultValueCreator: bindable => new ObservableCollection<Shadow>());
        public static readonly BindableProperty ShadowsProperty = ShadowsPropertyKey.BindableProperty;


        public Color BorderColor { get => (Color)GetValue(BorderColorProperty); set => SetValue(BorderColorProperty, value); }
        public double BorderWidth { get => (double)GetValue(BorderWidthProperty); set => SetValue(BorderWidthProperty, value); }
        public LineStyle BorderStyle { get => (LineStyle)GetValue(BorderStyleProperty); set => SetValue(BorderStyleProperty, value); }
        public Color BorderLeftColor { get => (Color)GetValue(BorderLeftColorProperty); set => SetValue(BorderLeftColorProperty, value); }
        public double BorderLeftWidth { get => (double)GetValue(BorderLeftWidthProperty); set => SetValue(BorderLeftWidthProperty, value); }
        public LineStyle BorderLeftStyle { get => (LineStyle)GetValue(BorderLeftStyleProperty); set => SetValue(BorderLeftStyleProperty, value); }
        public Color BorderTopColor { get => (Color)GetValue(BorderTopColorProperty); set => SetValue(BorderTopColorProperty, value); }
        public double BorderTopWidth { get => (double)GetValue(BorderTopWidthProperty); set => SetValue(BorderTopWidthProperty, value); }
        public LineStyle BorderTopStyle { get => (LineStyle)GetValue(BorderTopStyleProperty); set => SetValue(BorderTopStyleProperty, value); }
        public Color BorderRightColor { get => (Color)GetValue(BorderRightColorProperty); set => SetValue(BorderRightColorProperty, value); }
        public double BorderRightWidth { get => (double)GetValue(BorderRightWidthProperty); set => SetValue(BorderRightWidthProperty, value); }
        public LineStyle BorderRightStyle { get => (LineStyle)GetValue(BorderRightStyleProperty); set => SetValue(BorderRightStyleProperty, value); }
        public Color BorderBottomColor { get => (Color)GetValue(BorderBottomColorProperty); set => SetValue(BorderBottomColorProperty, value); }
        public double BorderBottomWidth { get => (double)GetValue(BorderBottomWidthProperty); set => SetValue(BorderBottomWidthProperty, value); }
        public LineStyle BorderBottomStyle { get => (LineStyle)GetValue(BorderBottomStyleProperty); set => SetValue(BorderBottomStyleProperty, value); }


        public IList<Background> Backgrounds {get => (IList<Background>)GetValue(BackgroundsProperty);}
        public BackgroundOrigin BackgroundOrigin { get => Backgrounds.Count > 0 ? Backgrounds[0].Origin : default(BackgroundOrigin); set => Backgrounds[0] = new Background(Backgrounds[0]){ Origin = value }; }
        public Position BackgroundPosition { get => Backgrounds.Count > 0 ? Backgrounds[0].Position : default(Position); set => Backgrounds[0] = new Background(Backgrounds[0]) { Position = value }; }
        public Repeat BackgroundRepeat { get => Backgrounds.Count > 0 ? Backgrounds[0].Repeat : default(Repeat); set => Backgrounds[0] = new Background(Backgrounds[0]) { Repeat = value }; }
        public global::Nacre.Size BackgroundSize { get => Backgrounds.Count > 0 ? Backgrounds[0].Size : default(global::Nacre.Size); set => Backgrounds[0] = new Background(Backgrounds[0]) { Size = value }; }

        public IList<Shadow> Shadows { get => (IList<Shadow>)GetValue(ShadowsProperty);}
        public bool ShadowInset { get => Shadows.Count > 0 ? Shadows[0].Inset : false; set => new Shadow(Shadows[0]) { Inset = value }; }
        public double ShadowOffsetX { get => Shadows.Count > 0 ? Shadows[0].OffsetX : 0.0; set => new Shadow(Shadows[0]) { OffsetX = value }; }
        public double ShadowOffsetY { get => Shadows.Count > 0 ? Shadows[0].OffsetY : 0.0; set => new Shadow(Shadows[0]) { OffsetY = value }; }
        public double ShadowBlurRadius {  get => Shadows.Count > 0 ? Shadows[0].BlurRadius : 0.0; set => new Shadow(Shadows[0]) { BlurRadius = value }; }
        public double ShadowSpreadRadius { get => Shadows.Count > 0 ? Shadows[0].SpreadRadius : 0.0; set => new Shadow(Shadows[0]) { SpreadRadius = value }; }
        public Color ShadowColor { get => Shadows.Count > 0 ? Shadows[0].Color : default(Color); set => new Shadow(Shadows[0]) { Color = value }; }
    }
}
