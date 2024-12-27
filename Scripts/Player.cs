using SFML.Graphics;
using SFML.System;
using SFML.Window;

public class Player
{
    public Player(float x, float y, Color fillColor)
    {
        RacketShape = new RectangleShape()
        {
            Size = new Vector2f(x, y),
            FillColor = fillColor,
            Origin = new Vector2f(x / 2, y / 2),
        };
    }

    public Keyboard.Key keyToDown {get; private set;}
    public Keyboard.Key keyToUp {get; private set;}

    public RectangleShape RacketShape { get; private set; }

    public void SetRacketsCoordinates(float x, float y)
    {
        RacketShape.Position = new Vector2f(x, y);
    }

    public void SetInputKeys(Keyboard.Key keyToDown, Keyboard.Key keyToUp)
    {
        this.keyToDown = keyToDown;
        this.keyToUp = keyToUp;
    }

    public void HandleInput(Keyboard.Key key, float heightOfScreen)
    {
        if (key == keyToUp)
        {
            TryMoveRacket(-50, heightOfScreen);
        }
        else if (key == keyToDown)
        {
            TryMoveRacket(50, heightOfScreen);
        }
    }

    public void TryMoveRacket(float distance, float maxHeight)
    {
        float NewYPosition = RacketShape.Position.Y + distance;

        if (NewYPosition < 0 || NewYPosition > maxHeight)
        {
            return;
        }

        RacketShape.Position = new Vector2f(RacketShape.Position.X, NewYPosition);
    }
}