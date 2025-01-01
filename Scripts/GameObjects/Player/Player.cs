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

        defaultValues = new()
        {
            MoveSpeed = 50
        };
    }

    private PlayerDefaultValues defaultValues;
    
    public Keyboard.Key KeyToDown {get; private set;}
    public Keyboard.Key KeyToUp {get; private set;}

    public RectangleShape RacketShape { get; private set; }

    private float directionMultiplayer = 0;

    public void SetRacketsCoordinates(float x, float y)
    {
        RacketShape.Position = new Vector2f(x, y);
    }

    public void SetInputKeys(Keyboard.Key keyToDown, Keyboard.Key keyToUp)
    {
        KeyToDown = keyToDown;
        KeyToUp = keyToUp;
    }

    public void HandleInput(Keyboard.Key key)
    {
        if (key == KeyToUp)
        {
            directionMultiplayer = -1;
        }
        else if (key == KeyToDown)
        {
            directionMultiplayer = 1;
        }
    }

    public void TryMoveRacket()
    {
        float deltaY = defaultValues.MoveSpeed * directionMultiplayer; 
        float newYPosition = RacketShape.Position.Y + deltaY;

        if (newYPosition < 0 || newYPosition > Configurations.WindowHeight)
        {
            return;
        }

        RacketShape.Position = new Vector2f(RacketShape.Position.X, newYPosition);

        directionMultiplayer = 0;
    }
}

public struct PlayerDefaultValues
{
    public float MoveSpeed { get; init; }
}