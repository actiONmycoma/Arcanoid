using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System;

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

        public void start(float speed, Vector2f direction)
        {
            if (this.speed != 0) return;

            this.speed = speed;
            this.direction = direction;
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

        public bool CheckCollision(Sprite sprite)
        {
            if (this.sprite.GetGlobalBounds().Intersects(sprite.GetGlobalBounds()) == true)
            {
                direction.Y *= -1;
                direction.X *= -1;

                return true;
            }

            return false;
        }
    }
}
