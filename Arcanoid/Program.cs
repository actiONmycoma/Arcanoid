using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System;

namespace Arcanoid
{
    internal class Program
    {
        private static RenderWindow window;

        private static Texture ballTexture;
        private static Texture stickTexture;
        private static Texture blockTexture;
        private static Texture strongBlockTexture;

        private static Ball ball;
        private static Stick stick;
        private static Block[,] blocks;

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

                if (IsGame(lifeCount, level))
                {
                    if (level < 10 && IsLevelClear())
                    {
                        InitBlocks(++level);
                        SetBlocksSprite();

                        SetNewGameStartPosition();

                        levelText = new Text($"Level {level}", font);
                    }

                    if (ball.GetSpeed() == 0)
                    {
                        SetBallStartPosition();
                    }

                    if (Mouse.IsButtonPressed(Mouse.Button.Left)) ball.Start(5, new Vector2f(0, -1));

                    ball.Move(new Vector2i(0, (int)levelText.CharacterSize), new Vector2i((int)window.Size.X, (int)window.Size.Y));

                    stick.Move(window);

                    if (ball.CheckCollision(stick))
                    {
                        float f = ((ball.sprite.Position.X + ball.sprite.Texture.Size.X * 0.5f) -
                       (stick.sprite.Position.X + stick.sprite.Texture.Size.X * 0.5f)) / stick.sprite.Texture.Size.X;

                        ball.ChangeDirection(new Vector2f(f * 2, -1));
                    }

                    InteractionWithBlocks();

                    if (ball.sprite.Position.Y > 600)
                    {
                        lifeCount--;
                        ball.ChangeSpeed(0);
                        SetBallStartPosition();
                    }
                }


                DrawInterface(lifeSprites, levelText, lifeCount);

                DrawGameElements();

                if (!IsGame(lifeCount, level))
                {
                    DrawEndGame(lifeCount, font);
                }

                window.Display();
            }

        }

        private static void WindowClosed(object sender, EventArgs e)
        {
            window.Close();
        }

        private static void DrawInterface(Sprite[] lifeSprites, Text levelText, int lifeCount)
        {
            window.Draw(levelText);

            for (int i = 0; i < lifeCount; i++)
            {
                window.Draw(lifeSprites[i]);
            }
        }

        private static void DrawGameElements()
        {
            for (int i = 0; i < blocks.GetLength(0); i++)
            {
                for (int j = 0; j < blocks.GetLength(1); j++)
                {
                    if (blocks[i, j].GetVisible()) window.Draw(blocks[i, j].sprite);
                }
            }

            window.Draw(stick.sprite);
            window.Draw(ball.sprite);
        }

        private static void DrawEndGame(int lifeCount, Font font)
        {
            Text text;

            if (lifeCount < 0)
            {
                text = new Text("YOU LOSE", font, 100);
            }
            else
            {
                text = new Text("YOU WIN!", font, 100);
            }

            text.Position = new Vector2f(200, 200);

            window.Draw(text);

            text = new Text("click [x] to exit",font,50);
            text.Position = new Vector2f(270,320);

            window.Draw(text);
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

        private static void InteractionWithBlocks()
        {
            int row = 0;
            int col = 0;
            while (row < blocks.GetLength(0))
            {
                bool isBreakBlock = false;

                while (col < blocks.GetLength(1))
                {
                    if (ball.CheckCollision(blocks[row, col]))
                    {
                        if (blocks[row, col].GetBreakCount() == 1)
                        {
                            blocks[row, col].ChangeVisibility();
                        }
                        else
                        {
                            blocks[row, col].Update(blockTexture);
                        }

                        ball.ChangeDirection(new Vector2f(ball.GetDirection().X, ball.GetDirection().Y * -1));

                        isBreakBlock = true;
                        break;
                    }

                    col++;
                }

                if (isBreakBlock) break;

                row++;
                col = 0;
            }
        }

        private static void SetNewGameStartPosition()
        {
            stick.sprite.Position = new Vector2f(400 - stick.sprite.TextureRect.Width * 0.5f, 550);
            SetBallStartPosition();
        }

        private static void SetBallStartPosition()
        {
            Vector2f position = new Vector2f(stick.sprite.Position.X + stick.sprite.Texture.Size.X * 0.5f -
                ball.sprite.Texture.Size.X * 0.5f, stick.sprite.Position.Y - ball.sprite.Texture.Size.Y);

            ball.sprite.Position = position;
        }

        private static bool IsLevelClear()
        {
            for (int i = 0; i < blocks.GetLength(0); i++)
            {
                for (int j = 0; j < blocks.GetLength(1); j++)
                {
                    if (blocks[i, j].GetVisible() == true) return false;
                }
            }

            return true;
        }

        private static bool IsGame(int lifeCount, int level)
        {
            return lifeCount >= 0 && (level < 10 || !IsLevelClear());
        }
    }
}
