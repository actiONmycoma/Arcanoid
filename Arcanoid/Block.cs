using SFML.Graphics;

namespace Arcanoid
{
    internal class Block
    {
        public Sprite sprite;

        private int breakCount;
        private bool isVisible;

        public Block(Texture texture)
        {
            sprite = new Sprite(texture);
        }
    }
}
