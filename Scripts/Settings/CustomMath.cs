using SFML.Graphics;
using SFML.System;

public static class CustomMath
{
    public static bool Approximately(float a, float b)
    {
        const float epsilon = 1e-6f;
        const float floatEpsilon = float.Epsilon * 8;
        return Math.Abs(b - a) < Math.Max(epsilon * Math.Max(Math.Abs(a), Math.Abs(b)), floatEpsilon);
    }

    public static (float distanceSquared, float closestX, float closestY) ClosestPointAndDistance(FloatRect rectangleRect, Vector2f circlePosition)
    {
        float closestX = Math.Clamp(circlePosition.X, rectangleRect.Left, rectangleRect.Left + rectangleRect.Width);
        float closestY = Math.Clamp(circlePosition.Y, rectangleRect.Top, rectangleRect.Top + rectangleRect.Height);

        float distanceX = circlePosition.X - closestX;
        float distanceY = circlePosition.Y - closestY;
        float distanceSquared = distanceX * distanceX + distanceY * distanceY;

        return (distanceSquared, closestX, closestY);
    }
}