using SFML.Graphics;
using SFML.System;

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
            this.isVisible = true;
        }

        public void SetSprite(Texture texture)
        {
            this.sprite = new Sprite(texture);
        }

        public void ChangeVisibility() 
        {
            if (this.isVisible) 
                this.isVisible = false;
            else
                this.isVisible = true;
        }

        public void Update(Texture texture)
        {
            Vector2f position = this.sprite.Position;
            this.sprite = new Sprite(texture);
            this.sprite.Position = position;
            this.breakCount--;
        }

        public int GetBreakCount()
        {
            return this.breakCount;
        }

        public bool GetVisible()
        {
           return this.isVisible;
        }
    }
}
