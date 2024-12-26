using SFML.Graphics;
using SFML.System;

public class Ball
{
    public Ball(float radius, Color fillColor)
    {
        Shape = new CircleShape()
        {
            Radius = radius,
            FillColor = fillColor,
            Origin = new Vector2f(radius, radius),
        };
    }

    private Random random = new Random();
    public CircleShape Shape { get; private set; }

    public Vector2f Direction { get; private set; }

    private bool isCanMove;

    public void StartMovement()
    {
        isCanMove = true;

        Direction = new Vector2f(0.5f, 1f);
    }

    public void Move()
    {
        if (!isCanMove)
        {
            return;
        }

        float deltaX = Shape.Position.X + Direction.X;
        float deltaY = Shape.Position.Y + Direction.Y;

        Shape.Position = new Vector2f(deltaX, deltaY);
    }
    
    public void DropIntoPosition(Vector2f pos)
    {
        Shape.Position = pos;
        isCanMove = false;
    }

    private void ReverseDirectionY()
    {
        Direction = new Vector2f(Direction.X, -Direction.Y);
    }

    private void ReverseDirectionX()
    {
        float coeffOfChange = GenerateCoefficient();

        if (Direction.X > 0)
        {
            coeffOfChange *= -1;
        }

        Direction = new Vector2f(coeffOfChange, Direction.Y);
    }

    private float GenerateCoefficient()
    {
        return (float)(random.Next(5, 11)) / 10f;
    }

    public void DetectingFrameCollisions(Vector2u windowSize)
    {
        float top = Shape.Position.Y - Shape.Radius;
        float bottom = Shape.Position.Y + Shape.Radius;
        float left = Shape.Position.X - Shape.Radius;
        float right = Shape.Position.X + Shape.Radius;

        if (bottom > windowSize.Y || top < 0)
        {
            ReverseDirectionY();
        }

        if (right > windowSize.X || left < 0)
        {
            ReverseDirectionX();
        }
    }
}