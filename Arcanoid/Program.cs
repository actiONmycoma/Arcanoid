using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System;
using System.Data;

namespace Arcanoid
{
    internal class Program
    {
        private static RenderWindow window;

        private static Texture ballTexture;
        private static Texture stickTexture;
        private static Texture blockTexture;
        private static Texture strongBlockTexture;


        private static Stick stick;
        private static Block[,] blocks;

        private static Ball ball;

        static void Main(string[] args)
        {
            window = new RenderWindow(new VideoMode(800, 600), "Arcanoid");
            window.SetFramerateLimit(60);
            window.Closed += WindowClosed;

            Font font = new Font("arialmt.ttf");

            ballTexture = new Texture("Ball.png");
            stickTexture = new Texture("Stick.png");
            blockTexture = new Texture("Block.png");
            strongBlockTexture = new Texture("Block2.png");


            stick = new Stick(stickTexture);
            ball = new Ball(ballTexture);

            InitBlocks();
            SetBlocksSprite();

            SetStartPosition();

            int lifeCount = 3;
            int level = 0;

            Text levelText = new Text($"Level {level}", font);
            Sprite[] lifeSprites = new Sprite[lifeCount];
            for (int i = 0; i < lifeSprites.Length; i++)
            {
                lifeSprites[i] = new Sprite(ballTexture);
                lifeSprites[i].Position = new Vector2f(window.Size.X - ballTexture.Size.X * (i + 1) - 5 * i, 2);
            }

            while (window.IsOpen == true)
            {
                window.Clear();

                window.DispatchEvents();

                if (Mouse.IsButtonPressed(Mouse.Button.Left) == true) ball.start(5, new Vector2f(0, -1));

                ball.Move(new Vector2i(0, (int)levelText.CharacterSize), new Vector2i((int)window.Size.X, (int)window.Size.Y));
                ball.CheckCollision(stick);

                stick.sprite.Position = new Vector2f(Mouse.GetPosition(window).X -
                    stick.sprite.TextureRect.Width * 0.5f, stick.sprite.Position.Y);

                if (ball.sprite.Position.Y > 600)
                {
                    lifeCount--;                    
                }


                window.Draw(levelText);

                for (int i = 0; i < lifeCount; i++)
                {
                    window.Draw(lifeSprites[i]);
                }

                window.Draw(stick.sprite);
                window.Draw(ball.sprite);

                window.Display();
            }

        }

        private static void WindowClosed(object sender, EventArgs e)
        {
            window.Close();
        }
        private static void InitBlocks(int level = 0)
        {
            blocks = new Block[10,10];

            int breakCount;

            for (int i = 0; i < blocks.GetLength(0); i++)
            {
                if (i < level)
                    breakCount = 2;
                else
                    breakCount = 1;
                
                for (int j = 0; j < blocks.GetLength(1); j++)
                {
                    blocks[i, j] = new Block(breakCount);                    
                }
            }
        }        

        private static void SetBlocksSprite(/*Texture block, Texture strongBlock*/)
        {
            for (int i = 0; i < blocks.GetLength(0); i++)
            {
                for (int j = 0; j < blocks.GetLength(1); j++)
                {
                    int breakCount = blocks[i, j].GetBreakCount();

                    if (breakCount == 1) blocks[i, j].SetSprite(blockTexture);
                    if (breakCount == 2) blocks[i, j].SetSprite(strongBlockTexture);
                }
            }
        }
        private static void SetStartPosition()
        {
            stick.sprite.Position = new Vector2f(400 - stick.sprite.TextureRect.Width * 0.5f, 550);
            ball.sprite.Position = new Vector2f(400, 400);            
        }
                
    }
}
