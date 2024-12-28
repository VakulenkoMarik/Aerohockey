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
    private Text score = new();

    public Player? Winer { get; private set; }
    
    private float distanceFromTheGoal = 100;
    private bool isEndRound = false;

    public void Start(GamePLayer player1, GamePLayer player2, string scoreText, RenderWindow window)
    {
        Init(player1, player2, scoreText, window);

        RoundCycle();
    }

    private void Init(GamePLayer p1, GamePLayer p2, string text, RenderWindow window)
    {
        this.window = window;
        window.KeyPressed += OnKeyPressed;
        window.Closed += WindowClosed;

        ScoreCustomisation(text);

        ball = new Ball(50f, Color.Blue);

        SetPlayers(p1, p2);

        BallToTheStart();
    }

    private void ScoreCustomisation(string text)
    {
        score = Configurations.mainText;
        score.DisplayedString = text;

        score.Origin = new Vector2f(score.GetLocalBounds().Width / 2f, score.GetLocalBounds().Height / 2f);
        score.Position = new Vector2f(window.Size.X / 2f, 40f);
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
        ball.Move();

        CollisionsDetecting();

        CheckGoal();
    }

    private void CollisionsDetecting()
    {
        ball.DetectingBordersCollision(window.Size);

        ball.CollisionProcessing(player1.RacketShape);
        ball.CollisionProcessing(player2.RacketShape);
    }

    private void CheckGoal()
    {
        if (ball.Shape.Position.X <= 0)
        {
            Winer = player2;
            isEndRound = true;
        }
        else if (ball.Shape.Position.X >= window.Size.X)
        {
            Winer = player1;
            isEndRound = true;
        }
    }

    private void DrawObjects()
    {
        window.Draw(ball.Shape);

        RectangleShape racket1 = player1.RacketShape;
        window.Draw(racket1);

        RectangleShape racket2 = player2.RacketShape;
        window.Draw(racket2);

        window.Draw(score);
    }

    private void OnKeyPressed(object sender, KeyEventArgs e)
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
        RenderWindow w = (RenderWindow)sender;
        w.Close();
    }

    private bool IsEndRound()
    {
        return isEndRound;
    }
}