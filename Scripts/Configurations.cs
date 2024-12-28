using SFML.Graphics;
using SFML.System;

public static class Configurations
{
    public static Font openSans = new Font("Fonts\\OpenSans-VariableFont_wdth,wght.ttf");

    public static Text mainText = new Text()
    {
        Font = openSans,

        Scale = new Vector2f(2f, 2f),

        FillColor = Color.White,
        OutlineColor = Color.Black,
        OutlineThickness = 1f,
    };
}