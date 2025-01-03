using SFML.Window;

public class GamePlayer
{
    public GamePlayer(Keyboard.Key keyToDown, Keyboard.Key keyToUp, bool isFirstPlayer = false)
    {
        Player = new(isFirstPlayer);
        Player.SetInputKeys(keyToDown, keyToUp);
    }

    public Player Player { get; init; }
    public int RoundsWin { get; set; } = 0;
}