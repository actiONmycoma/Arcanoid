using SFML.Graphics;
using SFML.Window;
using System;

namespace Arcanoid
{
    internal class Program
    {
        private static RenderWindow window;

        static void Main(string[] args)
        {
            window = new RenderWindow(new VideoMode(800, 600), "Arcanoid");
            window.SetFramerateLimit(60);
            window.Closed += WindowClosed;


            while (window.IsOpen == true)
            {
                window.Clear();

                window.DispatchEvents();

                window.Display();
            }

        }

        private static void WindowClosed(object sender, EventArgs e)
        {
            window.Close();
        }
    }
}
