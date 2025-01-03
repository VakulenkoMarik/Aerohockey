using SFML.Graphics;

public static class CustomMath
{
    public static bool Approximately(float a, float b)
    {
        const float epsilon = 1e-6f;
        const float floatEpsilon = float.Epsilon * 8;
        return Math.Abs(b - a) < Math.Max(epsilon * Math.Max(Math.Abs(a), Math.Abs(b)), floatEpsilon);
    }

    public static (float distanceSquared, float closestX, float closestY) ClosestPointAndDistance(FloatRect rectangleRect, CircleShape circleShape)
    {
        float closestX = Math.Clamp(circleShape.Position.X, rectangleRect.Left, rectangleRect.Left + rectangleRect.Width);
        float closestY = Math.Clamp(circleShape.Position.Y, rectangleRect.Top, rectangleRect.Top + rectangleRect.Height);

        float distanceX = circleShape.Position.X - closestX;
        float distanceY = circleShape.Position.Y - closestY;
        float distanceSquared = distanceX * distanceX + distanceY * distanceY;

        return (distanceSquared, closestX, closestY);
    }
}