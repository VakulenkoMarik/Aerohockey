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
        float xSpeed = speed;
        float ySpeed = xSpeed - xSpeed / 10;

        Shape = new CircleShape()
        {
            Radius = radius,
            FillColor = fillColor,
            Origin = new Vector2f(radius, radius),
        };

        Direction = new Vector2f(xSpeed, ySpeed);
    }

    private Random random = new Random();
    public CircleShape Shape { get; private set; }

    public Vector2f Direction { get; private set; }

    public bool IsCanMove { get; set; }
    private float speed = 8f;

    public void Move()
    {
        if (!IsCanMove)
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
        IsCanMove = false;
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
        int minValue = (int)(speed * 5);
        int maxValue = (int)(speed * 15);

        return (float)(random.Next(minValue, maxValue)) / 10f;
    }

    public void CollisionProcessing(RectangleShape target)
    {
        switch (CheckCollision(target))
        {
            case CollisionType.Horizontal:
                ReverseDirectionY();
                ReverseDirectionX();
                break;

            case CollisionType.Vertical:
                ReverseDirectionX();
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
            if (CustomMath.Approximately(closestX, Shape.Position.X))
            {
                return CollisionType.Horizontal;
            }
            else if (CustomMath.Approximately(closestY, Shape.Position.Y))
            {
                return CollisionType.Vertical;
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