using SFML.Graphics;
using SFML.System;

public static class Configurations
{
    // Textures
    public static readonly string BackgroundPath = "Textures\\Football.jpg";
    public static readonly string RacketPath = "Textures\\Racket.jpg";
    public static readonly string BallPath = "Textures\\Ball.png";

    // Fonts
    private static readonly string OpenSansPath = "Fonts\\OpenSans.ttf";

    // Audio
    public static readonly string BackgroundMusicPath = "Audio\\Music\\8-Bit_Music.mp3";
    public static readonly string BouncingSFXPath = "Audio\\SFX\\Bounce_SFX.mp3";
    public static readonly string CelebrationsSFXPath = "Audio\\SFX\\Celebrations.wav";


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