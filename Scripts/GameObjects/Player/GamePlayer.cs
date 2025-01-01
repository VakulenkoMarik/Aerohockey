using SFML.Graphics;
using SFML.System;
using SFML.Window;

public class GamePlayer
{
    public GamePlayer(Keyboard.Key keyToDown, Keyboard.Key keyToUp)
    {
        player = new(50, 300, Color.Red);
        player.SetInputKeys(keyToDown, keyToUp);
    }

    public Player player;
    public int roundsWin = 0;
}