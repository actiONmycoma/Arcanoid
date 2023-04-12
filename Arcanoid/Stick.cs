using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Arcanoid
{
    internal class Stick
    {
        public Sprite sprite;

        public Stick(Texture texture)
        {
            sprite = new Sprite(texture);
        }

        public void Move(RenderWindow window)
        {
            if (Mouse.GetPosition(window).X - sprite.TextureRect.Width * 0.5f < 0) return;
            if (Mouse.GetPosition(window).X + sprite.TextureRect.Width * 0.5f > window.Size.X) return;

            sprite.Position = new Vector2f(Mouse.GetPosition(window).X -
                        sprite.TextureRect.Width * 0.5f, sprite.Position.Y);
        }
    }
}
