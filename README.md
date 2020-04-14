# Nacre

View to create beautiful UI

![](screenshot.png)

Code
```cs
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
```

