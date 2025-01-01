using SFML.Graphics;
using SFML.System;

public static class Configurations
{
    public static readonly string BackgroundPath = "Textures\\Football.jpg";
    private static readonly string OpenSansPath = "Fonts\\OpenSans.ttf";

    public static readonly uint WindowHeight = 1000;
    public static readonly uint WindowWidth = 1500;

    public readonly static Font OpenSans = new Font(PathUtils.Get(OpenSansPath));
    
    public static Text MainText = new Text()
    {
        Font = OpenSans,

        Scale = new Vector2f(2f, 2f),

        FillColor = Color.White,
        OutlineColor = Color.Black,
        OutlineThickness = 1f,
    };
}