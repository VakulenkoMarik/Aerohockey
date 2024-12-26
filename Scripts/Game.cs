using SFML.Graphics;
using SFML.System;
using SFML.Window;

public class Game
{
    private RenderWindow window = new RenderWindow(new VideoMode(1600, 900), "Game window");
    private Color backgoundColor = Color.Green;

    private Ball ball;

    public void Start()
    {
        Init();

        GameCycle();
    }

    private void Init()
    {
        window.Closed += WindowClosed;
        window.KeyPressed += StartBallMovementKey;

        ball = new Ball(50f, Color.Blue);

        BallToTheStart();
    }

    private void BallToTheStart()
    {
        float middleOfScreenByX = window.Size.X / 2;
        float middleOfScreenByY = window.Size.Y / 2;

        Vector2f position = new Vector2f(middleOfScreenByX, middleOfScreenByY);

        ball.DropIntoPosition(position);
    }

    private void GameCycle()
    {
        while (window.IsOpen)
        {
            window.DispatchEvents();

            window.Clear(backgoundColor);

            Logic();

            DrawObjects();
            
            window.Display();
        }
    }

    private void Logic()
    {
        BallMovementProcessing();
    }

    private void BallMovementProcessing()
    {
        ball.Move();

        ball.DetectingFrameCollisions(window.Size);
    }

    private void DrawObjects()
    {
        window.Draw(ball.Shape);
    }

    private void WindowClosed(object sender, EventArgs e)
    {
        RenderWindow w = (RenderWindow)sender;
        w.Close();
    }

    private void StartBallMovementKey(object sender, SFML.Window.KeyEventArgs e)
    {
        if (e.Code == Keyboard.Key.Space)
        {
            ball.StartMovement();
        }
    }
}