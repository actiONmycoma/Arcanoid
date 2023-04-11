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

            SetNewGameStartPosition();

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

                if (Mouse.IsButtonPressed(Mouse.Button.Left) == true) ball.Start(5, new Vector2f(0, -1));

                ball.Move(new Vector2i(0, (int)levelText.CharacterSize), new Vector2i((int)window.Size.X, (int)window.Size.Y));

                ball.CheckCollision(stick);

                for (int i = 0; i < blocks.GetLength(0); i++)
                {
                    for (int j = 0; j < blocks.GetLength(1); j++)
                    {
                        if (ball.CheckCollision(blocks[i, j])) blocks[i, j].ChangeVisibility();
                    }
                }

                stick.sprite.Position = new Vector2f(Mouse.GetPosition(window).X -
                    stick.sprite.TextureRect.Width * 0.5f, stick.sprite.Position.Y);//на заметке

                if (ball.sprite.Position.Y > 600)
                {
                    lifeCount--;
                }


                window.Draw(levelText);

                for (int i = 0; i < lifeCount; i++)
                {
                    window.Draw(lifeSprites[i]);
                }

                for (int i = 0; i < blocks.GetLength(0); i++)
                {
                    for (int j = 0; j < blocks.GetLength(1); j++)
                    {
                        if (blocks[i, j].GetVisible()) window.Draw(blocks[i, j].sprite);
                    }
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
            blocks = new Block[10, 10];

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

        private static void SetBlocksSprite()
        {
            for (int i = 0; i < blocks.GetLength(0); i++)
            {
                for (int j = 0; j < blocks.GetLength(1); j++)
                {
                    int breakCount = blocks[i, j].GetBreakCount();

                    if (breakCount == 1) blocks[i, j].SetSprite(blockTexture);
                    if (breakCount == 2) blocks[i, j].SetSprite(strongBlockTexture);

                    Vector2f vector = new Vector2f(10 + blocks[i, j].sprite.Texture.Size.X * (j + 1) + 20 * j,
                         70 + i * blocks[i, j].sprite.Texture.Size.Y + i * 20);

                    blocks[i, j].sprite.Position = vector;
                }
            }
        }

        private static void SetNewGameStartPosition()
        {
            stick.sprite.Position = new Vector2f(400 - stick.sprite.TextureRect.Width * 0.5f, 550);
            ball.sprite.Position = new Vector2f(400, 400);
        }

    }
}
