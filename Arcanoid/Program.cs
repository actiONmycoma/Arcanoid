using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System;

namespace Arcanoid
{
    internal class Program
    {
        private static RenderWindow window;

        private static Texture blockTexture;
        private static Texture ballTexture;
        private static Texture stickTexture;

        private static Sprite stick;
        private static Sprite[] blocks;

        private static Ball ball;

        static void Main(string[] args)
        {
            window = new RenderWindow(new VideoMode(800, 600), "Arcanoid");
            window.SetFramerateLimit(60);
            window.Closed += WindowClosed;

            ballTexture = new Texture("Ball.png");
            blockTexture = new Texture("Block.png");
            stickTexture = new Texture("Stick.png");

            stick = new Sprite(stickTexture);
            ball = new Ball(ballTexture);

            stick.Position = new Vector2f(400 - stick.TextureRect.Width * 0.5f, 550);
            ball.sprite.Position = new Vector2f(400, 400);



            while (window.IsOpen == true)
            {
                window.Clear();

                window.DispatchEvents();
                                

                if (Mouse.IsButtonPressed(Mouse.Button.Left) == true)
                {
                    ball.start(5, new Vector2f(0, -1));
                }
                                

                ball.Move(new Vector2i(0,0),new Vector2i((int)window.Size.X, (int)window.Size.Y));
                ball.CheckCollision(stick);

                stick.Position = new Vector2f(Mouse.GetPosition(window).X - stick.TextureRect.Width * 0.5f,
                    stick.Position.Y);

                

                window.Draw(stick);
                window.Draw(ball.sprite);

                window.Display();
            }

        }

        private static void WindowClosed(object sender, EventArgs e)
        {
            window.Close();
        }
    }
}
