using System.Text;
using SFML.Graphics;
using SFML.Window;

public class Game
{
    private RenderWindow window = new RenderWindow(new VideoMode(
        Configurations.WindowWidth, Configurations.WindowHeight), "Game window");

    private StringBuilder scoreText = new StringBuilder("0 : 0");

    private GamePlayer gamePlayer1;
    private GamePlayer gamePlayer2;

    public void Start()
    {
        Init();

        StartGame();
    }

    private void Init()
    {
        SetPlayers();

        window.Closed += WindowClosed;
    }

    private void SetPlayers()
    {
        gamePlayer1 = new GamePlayer(Keyboard.Key.S, Keyboard.Key.W);
        gamePlayer2 = new GamePlayer(Keyboard.Key.Down, Keyboard.Key.Up);
    }

    private void StartGame()
    {
        while (window.IsOpen)
        {
            Round round = new Round();
            round.Start(gamePlayer1, gamePlayer2, scoreText.ToString(), window);

            AfterRoundProcess(round.Winer);
        }
    }

    private void WindowClosed(object sender, EventArgs e)
    {
        RenderWindow w = (RenderWindow)sender;
        w.Close();
    }

    private void AfterRoundProcess(Player? winer)
    {
        if (winer == null)
        {
            return;
        }

        if (winer == gamePlayer1.player)
        {
            gamePlayer1.roundsWin++;
        }
        else if (winer == gamePlayer2.player)
        {
            gamePlayer2.roundsWin++;
        }

        scoreText.Clear();

        scoreText.Append($"{gamePlayer1.roundsWin} : {gamePlayer2.roundsWin}");
    }
}