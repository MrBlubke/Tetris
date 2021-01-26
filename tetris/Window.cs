﻿using System;

namespace tetris
{
    internal class Window
    {
        public int[,] Grid { get; set; }
        public int[,] GridStatic { get; set; }
        public int[,] PreviewGrid { get; set; }
        public int Height { get; }
        public int Width { get; }
        private const int pGridWidth = 12;
        private const int pGridHeight = 7;

        public Window(int width, int height)
        {
            Grid = new int[width, height];
            GridStatic = new int[width, height];
            PreviewGrid = new int[pGridWidth, pGridHeight];
            Height = height;
            Width = width;
        }

        public void ShowScore(int originX, int originY, int score)
        {
            Console.SetCursorPosition(originX, originY);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"SCORE: {score}");
            Console.ResetColor();
        }

        public void ShowMainWindow(int originX, int originY)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            // string block = "\u2585";
            string block = "\u2588";

            for (int y = 0; y < Height; y++)
            {
                Console.SetCursorPosition(originX, originY);
                for (int x = 0; x < Width; x++)
                {
                    DrawGridBlock(block, x, y);
                }
                originY++;
            }
            Console.CursorVisible = false;
        }

        private void DrawGridBlock(string block, int x, int y)
        {
            int kleur = Grid[x, y];
            switch (kleur)
            {
                case 0:
                    Console.Write(" ");
                    break;

                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    Console.ForegroundColor = colors.Colors[kleur - 2];
                    Console.Write(block);
                    Console.ResetColor();
                    break;

                default:
                    Console.Write(block);
                    break;
            }
        }

        public void ShowPreviewWindow(int originX, int originY, Block prevBlock)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            string block = "\u2588"; //2585

            int[,] previewBlock = prevBlock.GetBlockCurrentStatus();

            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    PreviewGrid[x + 4, y + 1] = previewBlock[x, y];
                }
            }

            for (int y = 0; y < pGridHeight; y++)
            {
                Console.SetCursorPosition(originX, originY);
                for (int x = 0; x < pGridWidth; x++)
                {
                    if (PreviewGrid[x, y] > 0)
                    {
                        Console.Write(block);
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                originY++;
            }
            Console.CursorVisible = false;
        }

        public void ClearPreviewWindow()
        {
            for (int x = 0; x < pGridWidth; x++)
            {
                PreviewGrid[x, 0] = 1;
            }
            for (int y = 1; y < pGridHeight - 1; y++)
            {
                PreviewGrid[0, y] = 1;
                PreviewGrid[11, y] = 1;
            }

            for (int x = 0; x < pGridWidth; x++)
            {
                PreviewGrid[x, 6] = 1;
            }
        }

        public void SetFrameAroundGrid()
        {
            for (int x = 0; x < Width; x++)
            {
                GridStatic[x, 0] = 1;
            }
            for (int y = 1; y < Height - 1; y++)
            {
                GridStatic[0, y] = 1;
                GridStatic[Width - 1, y] = 1;
            }

            for (int x = 0; x < Width; x++)
            {
                GridStatic[x, Height - 1] = 1;
            }
        }

        public void RefreshGrid()
        {
            Grid = (int[,])GridStatic.Clone();
        }

        public void UpdateStaticGrid()
        {
            GridStatic = (int[,])Grid.Clone();
        }
    }
}