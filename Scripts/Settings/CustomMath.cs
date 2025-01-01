public static class CustomMath
{
    public static bool Approximately(float a, float b)
    {
        const float epsilon = 1e-6f;
        const float floatEpsilon = float.Epsilon * 8;
        return Math.Abs(b - a) < Math.Max(epsilon * Math.Max(Math.Abs(a), Math.Abs(b)), floatEpsilon);
    }
}