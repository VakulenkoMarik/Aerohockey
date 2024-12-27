using SFML.Graphics;
using SFML.System;
using SFML.Window;

public class Round
{
    private RenderWindow window;
    private Color backgoundColor = Color.Green;

    private Ball ball;
    private Player player1;
    private Player player2;
    
    private float distanceFromTheGoal = 100;
    private bool isEndRound = false;

    public void Start(GamePLayer player1, GamePLayer player2, RenderWindow window)
    {
        Init(player1, player2, window);

        RoundCycle();
    }

    private void Init(GamePLayer p1, GamePLayer p2, RenderWindow window)
    {
        this.window = window;
        ball = new Ball(50f, Color.Blue);

        window.KeyPressed += OnKeyPressed;
        window.Closed += WindowClosed;

        SetPlayers(p1, p2);

        BallToTheStart();
    }

    private void SetPlayers(GamePLayer gamePlayer1, GamePLayer gamePlayer2)
    {
        player1 = gamePlayer1.player;
        player2 = gamePlayer2.player;

        float YPLayersStartPosition = window.Size.Y / 2;
        float p1StartX = distanceFromTheGoal;
        float p2StartX = window.Size.X - distanceFromTheGoal;

        player1.SetRacketsCoordinates(p1StartX, YPLayersStartPosition);
        player2.SetRacketsCoordinates(p2StartX, YPLayersStartPosition);
    }

    private void BallToTheStart()
    {
        float middleOfScreenByX = window.Size.X / 2;
        float middleOfScreenByY = window.Size.Y / 2;

        Vector2f position = new Vector2f(middleOfScreenByX, middleOfScreenByY);

        ball.DropIntoPosition(position);
    }

    private void RoundCycle()
    {
        while (!IsEndRound() && window.IsOpen)
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

        ball.DetectingBordersCollision(window.Size);
    }

    private void DrawObjects()
    {
        window.Draw(ball.Shape);

        RectangleShape racket1 = player1.RacketShape;
        window.Draw(racket1);


        RectangleShape racket2 = player2.RacketShape;
        window.Draw(racket2);
    }

    private void OnKeyPressed(object sender, SFML.Window.KeyEventArgs e)
    {
        if (e.Code == Keyboard.Key.Space)
        {
            ball.StartMovement();
        }

        float windowHeight = window.Size.Y;

        player1.HandleInput(e.Code, windowHeight);
        player2.HandleInput(e.Code, windowHeight);
    }

    private void WindowClosed(object sender, EventArgs e)
    {
        Console.WriteLine("Window is closing...");

        RenderWindow w = (RenderWindow)sender;
        w.Close();
    }

    private bool IsEndRound()
    {
        return isEndRound;
    }
}