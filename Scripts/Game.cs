using System.Text;
using SFML.Graphics;
using SFML.Window;

public class Game
{
    private RenderWindow window = new RenderWindow(new VideoMode(1600, 1000), "Game window");

    private StringBuilder scoreText = new StringBuilder("0 : 0");

    private GamePLayer gamePlayer1;
    private GamePLayer gamePlayer2;

    public void Start()
    {
        Init();

        GameCycle();
    }

    private void Init()
    {
        SetPlayers();

        window.Closed += WindowClosed;
    }

    private void SetPlayers()
    {
        gamePlayer1 = new GamePLayer(Keyboard.Key.S, Keyboard.Key.W);
        gamePlayer2 = new GamePLayer(Keyboard.Key.Down, Keyboard.Key.Up);
    }

    private void GameCycle()
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