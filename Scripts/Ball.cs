using SFML.Graphics;
using SFML.System;

public enum CollisionType
{
    Vertical,
    Horizontal,
    None,
}

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
    private float XSpeed = 0.3f;
    private float YSpeed = 0.5f;

    public void StartMovement()
    {
        isCanMove = true;

        Direction = new Vector2f(XSpeed, YSpeed);
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

    public void CollisionProcessing(RectangleShape target)
    {
        switch (CheckCollision(target))
        {
            case CollisionType.Horizontal:
                ReverseDirectionX();
                break;

            case CollisionType.Vertical:
                ReverseDirectionX();
                ReverseDirectionY();
                break;

            default:
                break;
        }
    }

    private CollisionType CheckCollision(RectangleShape rectangle)
    {
        FloatRect rectangleRect = rectangle.GetGlobalBounds();

        float closestX = Math.Clamp(Shape.Position.X, rectangleRect.Left, rectangleRect.Left + rectangleRect.Width);
        float closestY = Math.Clamp(Shape.Position.Y, rectangleRect.Top, rectangleRect.Top + rectangleRect.Height);

        float distanceX = Shape.Position.X - closestX;
        float distanceY = Shape.Position.Y - closestY;
        float distanceSquared = distanceX * distanceX + distanceY * distanceY;

        if (distanceSquared < Shape.Radius * Shape.Radius)
        {
            if (closestX == Shape.Position.X)
            {
                return CollisionType.Vertical;
            }
            else if (closestY == Shape.Position.Y)
            {
                return CollisionType.Horizontal;
            }
        }

        return CollisionType.None;
    }

    public void DetectingBordersCollision(Vector2u vector2)
    {
        CalculatePointCollision(vector2);
    }

    public void CalculatePointCollision(Vector2u vector2)
    {
        float top = Shape.Position.Y - Shape.Radius;
        float bottom = Shape.Position.Y + Shape.Radius;

        if (bottom > vector2.Y || top < 0)
        {
            ReverseDirectionY();
        }
    }
}