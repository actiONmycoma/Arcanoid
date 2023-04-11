using SFML.Graphics;

namespace Arcanoid
{
    internal class Stick
    {
        public Sprite sprite;

        public Stick(Texture texture)
        {
            sprite = new Sprite(texture);
        }
    }
}
