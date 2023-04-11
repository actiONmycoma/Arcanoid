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
            sprite = new Sprite(texture);
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

        public void CheckCollision(Stick stick)
        {
            if (this.sprite.GetGlobalBounds().Intersects(stick.sprite.GetGlobalBounds()) == true)
            {
                direction.Y = -1;

                float f = ((this.sprite.Position.X + this.sprite.Texture.Size.X * 0.5f) -
                    (stick.sprite.Position.X + stick.sprite.Texture.Size.X * 0.5f)) / stick.sprite.Texture.Size.X;

                direction.X = f * 2;

            }
        }

        public bool CheckCollision(Block block)
        {
            if (this.sprite.GetGlobalBounds().Intersects(block.sprite.GetGlobalBounds()) == true
                && block.GetVisible())
            {
                direction.Y *= -1;

                return true;
            }

            return false;
        }
    }
}
