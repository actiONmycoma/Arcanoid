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

        public bool CheckCollision(Sprite sprite, string tag)
        {
            if (this.sprite.GetGlobalBounds().Intersects(sprite.GetGlobalBounds()) == true)
            {
                if (tag == "stick")
                {
                    direction.Y = -1;

                    float f = ((this.sprite.Position.X + this.sprite.Texture.Size.X * 0.5f) -
                        (sprite.Position.X + sprite.Texture.Size.X * 0.5f)) / sprite.Texture.Size.X;

                    direction.X = f * 2;

                }

                if (tag == "block")
                {
                    direction.Y *= -1;

                }


                return true;
            }

            return false;
        }
    }
}
