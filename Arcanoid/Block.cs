using SFML.Graphics;

namespace Arcanoid
{
    internal class Block
    {
        public Sprite sprite;

        private int breakCount;
        private bool isVisible;

        public Block(int breakCount)
        {
            this.breakCount = breakCount;
            isVisible = true;
        }
        public void SetSprite(Texture texture)
        {
            sprite = new Sprite(texture);
        }

        public void ChangeVisibility() 
        {
            if (isVisible) 
                isVisible = false;
            else
                isVisible = true;
        }

        public int GetBreakCount()
        {
            return breakCount;
        }
    }
}
