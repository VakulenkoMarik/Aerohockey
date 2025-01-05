using SFML.Audio;

public class AudioController
{
    private Music backgroundMusic;
    public Sound bounceSFX { get; private set; }
    public Sound celebrationsSFX { get; private set; }

    public static AudioController Instance { get; private set; }

    public AudioController()
    {
        Instance = this;

        backgroundMusic = new Music(PathUtils.Get(Configurations.BackgroundMusicPath))
        {
            Volume = 20f,
            Loop = true,
        };

        bounceSFX = new();
        bounceSFX.Init(Configurations.BouncingSFXPath);

        celebrationsSFX = new();
        celebrationsSFX.Init(Configurations.CelebrationsSFXPath);
    }

    public void SetBackgroundMusic(string path)
    {
        backgroundMusic = new Music(PathUtils.Get(path));
    }

    public void PlayMusic(bool isPlay)
    {
        if (isPlay)
        {
            backgroundMusic.Play();
            return;
        }
        
        backgroundMusic.Stop();
    }
    
    public void Dispose()
    {
        backgroundMusic.Dispose();

        bounceSFX.TurnOff();
        celebrationsSFX.TurnOff();
    }
}

public static class SoundExtensions
{
    public static void Init(this Sound sound, string path)
    {
        SoundBuffer bounceSoundBuffer = new SoundBuffer(PathUtils.Get(path));
        sound.SoundBuffer = bounceSoundBuffer;
    }

    public static void TurnOff(this Sound sound)
    {
        sound.Dispose();
        sound.SoundBuffer.Dispose();
    }
}