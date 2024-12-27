using SFML.Graphics;
using SFML.System;
using SFML.Window;

public class Game
{
    private RenderWindow window = new RenderWindow(new VideoMode(1600, 900), "Game window");

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
            round.Start(gamePlayer1, gamePlayer2, window);
        }
    }

    private void WindowClosed(object sender, EventArgs e)
    {
        RenderWindow w = (RenderWindow)sender;
        w.Close();
    }
}