using SFML.Graphics;
using SFML.System;
using SFML.Window;

public class Round
{
    private uint playAreaX = Configurations.WindowWidth;
    private uint playAreaY = Configurations.WindowHeight;

    private RenderWindow window;
    private Color backgoundColor = Color.Green;

    private Ball ball;
    private Player player1;
    private Player player2;
    private Text score = new();
    private Sprite background = new();

    public Player? Winer { get; private set; }
    
    private float distanceFromTheGoal = 100;
    private bool isEndRound = false;

    public void Start(GamePlayer player1, GamePlayer player2, string scoreText, RenderWindow window)
    {
        Init(player1, player2, scoreText, window);

        RoundCycle();
    }

    private void Init(GamePlayer p1, GamePlayer p2, string text, RenderWindow window)
    {
        InfrastructureInit(window);

        GameplayInit(p1, p2, text);
    }

    private void InfrastructureInit(RenderWindow window)
    {
        this.window = window;
        window.KeyPressed += OnKeyPressed;
    }

    private void GameplayInit(GamePlayer p1, GamePlayer p2, string text)
    {
        BackgroundCustomisation();
        ScoreCustomisation(text);

        ball = new Ball(50f, Color.Blue);
        BallToTheStart();

        SetPlayers(p1, p2);
    }

    private void BackgroundCustomisation()
    {
        background.Texture = new Texture(PathUtils.Get(Configurations.BackgroundPath));

        background.Scale = new Vector2f(
            (float)playAreaX / background.Texture.Size.X,
            (float)playAreaY / background.Texture.Size.Y
        );

        background.Origin = new Vector2f(
            background.Texture.Size.X / 2f,
            background.Texture.Size.Y / 2f
        );

        background.Position = new Vector2f(
            playAreaX / 2f,
            playAreaY / 2f
        );
    }

    private void ScoreCustomisation(string text)
    {
        score = Configurations.MainText;
        score.DisplayedString = text;

        score.Origin = new Vector2f(score.GetLocalBounds().Width / 2f, score.GetLocalBounds().Height / 2f);
        score.Position = new Vector2f(playAreaX / 2f, 40f);
    }

    private void SetPlayers(GamePlayer gamePlayer1, GamePlayer gamePlayer2)
    {
        player1 = gamePlayer1.player;
        player2 = gamePlayer2.player;

        float YPLayersStartPosition = playAreaY / 2;
        float p1StartX = distanceFromTheGoal;
        float p2StartX = playAreaX - distanceFromTheGoal;

        player1.SetRacketsCoordinates(p1StartX, YPLayersStartPosition);
        player2.SetRacketsCoordinates(p2StartX, YPLayersStartPosition);
    }

    private void BallToTheStart()
    {
        float middleOfScreenByX = playAreaX / 2;
        float middleOfScreenByY = playAreaY / 2;

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
        player1.TryMoveRacket();
        player2.TryMoveRacket();

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
        else if (ball.Shape.Position.X >= playAreaX)
        {
            Winer = player1;
            isEndRound = true;
        }
    }

    private void DrawObjects()
    {
        window.Draw(background);
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
            ball.IsCanMove = true;
        }

        player1.HandleInput(e.Code);
        player2.HandleInput(e.Code);
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