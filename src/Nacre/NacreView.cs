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
        public static readonly BindableProperty BoxRadiusProperty = BindableProperty.Create(nameof(BoxRadius), typeof(BorderRadius), typeof(NacreView));

        public static readonly BindableProperty BackgroundSourceProperty = BindableProperty.Create(nameof(BackgroundSource), typeof(ISource), typeof(NacreView));
        public static readonly BindableProperty BackgroundOriginProperty = BindableProperty.Create(nameof(BackgroundOrigin), typeof(BackgroundOrigin), typeof(NacreView));
        public static readonly BindableProperty BackgroundPositionProperty = BindableProperty.Create(nameof(BackgroundPosition), typeof(Position), typeof(NacreView));
        public static readonly BindableProperty BackgroundRepeatProperty = BindableProperty.Create(nameof(BackgroundRepeat), typeof(Repeat), typeof(NacreView));
        public static readonly BindableProperty BackgroundSizeProperty = BindableProperty.Create(nameof(BackgroundSize), typeof(global::Nacre.Size), typeof(NacreView));

        public static readonly BindableProperty ShadowInsetProperty = BindableProperty.Create(nameof(ShadowInset), typeof(bool), typeof(NacreView));
        public static readonly BindableProperty ShadowOffsetXProperty = BindableProperty.Create(nameof(ShadowOffsetX), typeof(double), typeof(NacreView));
        public static readonly BindableProperty ShadowOffsetYProperty = BindableProperty.Create(nameof(ShadowOffsetY), typeof(double), typeof(NacreView));
        public static readonly BindableProperty ShadowBlurRadiusProperty = BindableProperty.Create(nameof(ShadowBlurRadius), typeof(double), typeof(NacreView));
        public static readonly BindableProperty ShadowSpreadRadiusProperty = BindableProperty.Create(nameof(ShadowSpreadRadius), typeof(double), typeof(NacreView));
        public static readonly BindableProperty ShadowColorProperty = BindableProperty.Create(nameof(ShadowColor), typeof(Color), typeof(NacreView));

        #region BorderProperties
        public Color BorderColor { get => (Color)GetValue(BorderColorProperty); set => SetValue(BorderColorProperty, value); }
        public double BorderWidth { get => (double)GetValue(BorderWidthProperty); set => SetValue(BorderWidthProperty, value); }
        public LineStyle BorderStyle { get => (LineStyle)GetValue(BorderStyleProperty); set => SetValue(BorderStyleProperty, value); }
        public BorderRadius BoxRadius { get => (BorderRadius)GetValue(BoxRadiusProperty); set => SetValue(BoxRadiusProperty, value); }
        #endregion BorderProperties

        #region BackgroundProperties
        public ISource BackgroundSource { get => (ISource)GetValue(BackgroundSourceProperty); set => SetValue(BackgroundSourceProperty, value); }
        public BackgroundOrigin BackgroundOrigin { get => (BackgroundOrigin)GetValue(BackgroundOriginProperty); set => SetValue(BackgroundOriginProperty, value); }
        public Position BackgroundPosition { get => (Position)GetValue(BackgroundPositionProperty); set => SetValue(BackgroundPositionProperty, value); }
        public Repeat BackgroundRepeat { get => (Repeat)GetValue(BackgroundRepeatProperty); set => SetValue(BackgroundRepeatProperty, value); }
        public global::Nacre.Size BackgroundSize { get => (global::Nacre.Size)GetValue(BackgroundSizeProperty); set => SetValue(BackgroundSizeProperty, value); }
        #endregion BackgroundProperties

        #region ShadowProperties
        public bool ShadowInset { get => (bool)GetValue(ShadowInsetProperty); set => SetValue(ShadowInsetProperty, value); }
        public double ShadowOffsetX { get => (double)GetValue(ShadowOffsetXProperty); set => SetValue(ShadowOffsetXProperty, value); }
        public double ShadowOffsetY { get => (double)GetValue(ShadowOffsetYProperty); set => SetValue(ShadowOffsetYProperty, value); }
        public double ShadowBlurRadius { get => (double)GetValue(ShadowBlurRadiusProperty); set => SetValue(ShadowBlurRadiusProperty, value); }
        public double ShadowSpreadRadius { get => (double)GetValue(ShadowSpreadRadiusProperty); set => SetValue(ShadowSpreadRadiusProperty, value); }
        public Color ShadowColor { get => (Color)GetValue(ShadowColorProperty); set => SetValue(ShadowColorProperty, value); }
        #endregion ShadowProperties

    }
}
