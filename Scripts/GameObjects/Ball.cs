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
    public Ball(float radius)
    {
        Radius = radius;

        float xSpeed = speed;
        float ySpeed = xSpeed - xSpeed / 10;

        Direction = new Vector2f(xSpeed, ySpeed);

        Texture ballTexture = new Texture(PathUtils.Get(Configurations.BallPath));

        Sprite = new Sprite()
        {
            Texture = new Texture(ballTexture),
            Origin = new Vector2f(ballTexture.Size.X / 2, ballTexture.Size.Y / 2),
            Scale = new Vector2f(radius / (ballTexture.Size.X / 2), radius / (ballTexture.Size.Y / 2))
        };
    }

    private Random random = new Random();
    public Sprite Sprite { get; private set; }

    public Vector2f Direction { get; private set; }

    public bool IsCanMove { get; set; }
    public float Radius { get; private set; }
    private float speed = 900f;

    public void Move()
    {
        if (!IsCanMove)
        {
            return;
        }

        float deltaX = Sprite.Position.X + Direction.X * Time.deltaTime;
        float deltaY = Sprite.Position.Y + Direction.Y * Time.deltaTime;

        Sprite.Position = new Vector2f(deltaX, deltaY);
        Sprite.Rotation += Direction.X / 100;
    }
    
    public void DropIntoPosition(Vector2f pos)
    {
        Sprite.Position = pos;
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

    public void CollisionProcessing(Player target)
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

    private CollisionType CheckCollision(Player target)
    {
        if (!PossibleCollision(target))
        {
            return CollisionType.None;
        }

        Sprite targetShape = target.RacketSprite;
        FloatRect rectangleRect = targetShape.GetGlobalBounds();

        var (distanceSquared, closestX, closestY) = CustomMath.ClosestPointAndDistance(rectangleRect, Sprite.Position);

        if (distanceSquared < Radius * Radius)
        {
            if (CustomMath.Approximately(closestX, Sprite.Position.X))
            {
                return CollisionType.Horizontal;
            }
            else if (CustomMath.Approximately(closestY, Sprite.Position.Y))
            {
                return CollisionType.Vertical;
            }
        }

        return CollisionType.None;
    }

    private bool PossibleCollision(Player target)
    {
        if (target.IsFirstPlayer && Direction.X > 0)
        {
            return false;
        }
        else if (!target.IsFirstPlayer && Direction.X < 0)
        {
            return false;
        }

        return true;
    }

    public void BordersCollisionProcessing(Vector2u vector2)
    {
        float top = Sprite.Position.Y - Radius;
        float bottom = Sprite.Position.Y + Radius;

        if (bottom > vector2.Y || top < 0)
        {
            ReverseDirectionY();
        }
    }
}