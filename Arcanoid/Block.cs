﻿using SFML.Graphics;
using SFML.System;
using System;


namespace Arcanoid
{
    internal class Block
    {
        public Sprite sprite;

        private int breakCount;

        public Block(Texture texture)
        {
            sprite = new Sprite(texture);
        }
    }
}
