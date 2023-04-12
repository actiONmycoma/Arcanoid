using SFML.Graphics;
using SFML.System;

namespace Arcanoid
{
    internal class Ball
    {
        public Sprite sprite;

        private float speed;
        private Vector2f direction;

        public Ball(Texture texture)
        {
            this.sprite = new Sprite(texture);
        }

        public void Start(float speed, Vector2f direction)
        {
            if (this.speed != 0) return;

            this.speed = speed;
            this.direction = direction;
        }

        public void ChangeSpeed(float speed)
        {
            this.speed = speed;
        }

        public void ChangeDirection(Vector2f vector)
        {
            this.direction = vector;
        }

        public void Move(Vector2i boundsPosition, Vector2i boundsSize)
        {

            sprite.Position += speed * direction;

            if (sprite.Position.X < boundsPosition.X ||
                sprite.Position.X > boundsSize.X - sprite.Texture.Size.X)
            {
                direction.X *= -1;
            }

            if (sprite.Position.Y < boundsPosition.Y)
            {
                direction.Y *= -1;
            }

        }

        public bool CheckCollision(Stick stick)
        {
            return this.sprite.GetGlobalBounds().Intersects(stick.sprite.GetGlobalBounds());
        }

        public bool CheckCollision(Block block)
        {
            return this.sprite.GetGlobalBounds().Intersects(block.sprite.GetGlobalBounds()) 
                && block.GetVisible();
        }

        public float GetSpeed()
        {
            return this.speed;
        }

        public Vector2f GetDirection()
        {
            return this.direction;
        }
    }
}
