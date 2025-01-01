public static class Time
{
    private const int TargetFPS = 60;
    public const float UntilUpdateTime = 1f / TargetFPS;

    public static float deltaTime { get; private set; }

    public static void Update(float newDeltaTime)
    {
        deltaTime = newDeltaTime;
    }
}