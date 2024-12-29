using SFML.Graphics;
using SFML.System;

public static class Configurations
{
    public static readonly string BackgroundPath = "Textures\\Football.jpg";
    private static readonly string openSansPath = "Fonts\\OpenSans.ttf";

    public readonly static Font openSans = new Font(PathUtils.Get(openSansPath));
    
    public static Text mainText = new Text()
    {
        Font = openSans,

        Scale = new Vector2f(2f, 2f),

        FillColor = Color.White,
        OutlineColor = Color.Black,
        OutlineThickness = 1f,
    };
}