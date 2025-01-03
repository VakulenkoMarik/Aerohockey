using SFML.Graphics;
using SFML.System;
using SFML.Window;

public class Player
{
    public Player(bool isFirstPlayer)
    {
        IsFirstPlayer = isFirstPlayer;

        float racketWidth = 50;
        float racketHeight = 300;
        Color racketColor = Color.Red;

        RacketShape = new RectangleShape()
        {
            Size = new Vector2f(racketWidth, racketHeight),
            Origin = new Vector2f(racketWidth / 2, racketHeight / 2),
            FillColor = racketColor,
        };

        defaultValues = new()
        {
            MoveSpeed = 2000
        };
    }

    public bool IsFirstPlayer { get; init; }

    public RectangleShape RacketShape { get; private set; }

    private PlayerDefaultValues defaultValues;
    
    public Keyboard.Key KeyToDown {get; private set;}
    public Keyboard.Key KeyToUp {get; private set;}

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
        float deltaY = defaultValues.MoveSpeed * directionMultiplayer * Time.deltaTime; 
        float newYPosition = RacketShape.Position.Y + deltaY;

        if (newYPosition < 0 || newYPosition > Configurations.WindowHeight)
        {
            return;
        }

        MoveRacket(newYPosition);

        directionMultiplayer = 0;
    }

    public void MoveRacket(float newYPosition)
    {
        RacketShape.Position = new Vector2f(RacketShape.Position.X, newYPosition);
    }
}

public struct PlayerDefaultValues
{
    public float MoveSpeed { get; init; }
}