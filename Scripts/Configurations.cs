using SFML.Graphics;
using SFML.System;

public static class Configurations
{
    public static string BackgroundPath = PathValidation("Textures\\Football.jpg");

    private static readonly string openSansPath = "Fonts\\OpenSans.ttf";
    public static Font openSans = new Font(PathValidation(openSansPath));

    private static string PathValidation(string filePath)
    {
        return Path.Combine(Directory.GetCurrentDirectory(), filePath);
    }
    
    public static Text mainText = new Text()
    {
        Font = openSans,

        Scale = new Vector2f(2f, 2f),

        FillColor = Color.White,
        OutlineColor = Color.Black,
        OutlineThickness = 1f,
    };
}