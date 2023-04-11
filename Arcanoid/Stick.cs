using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
