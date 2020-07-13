using System;

namespace B20_Ex02

{
    public class BoardGame
    {

        public BoardGame(int i_Hight, int i_Width)
        {
            Hight = i_Hight;
            Width = i_Width;

            MatrixForGameMoves = new char[Width, Hight];
            GameBoardMatrix = new char[Width, Hight];
            FillMatrix();

        }

        private int m_Hight;
        private int m_Width;
        private char[,] m_GameBoardMatrix;
        private char[,] m_MatrixForGameMoves;

        public int Hight { get => m_Hight; set => m_Hight = value; }
        public int Width { get => m_Width; set => m_Width = value; }
        public char[,] GameBoardMatrix { get => m_GameBoardMatrix; set => m_GameBoardMatrix = value; }
        public char[,] MatrixForGameMoves { get => m_MatrixForGameMoves; set => m_MatrixForGameMoves = value; }

        public void UpdateIndexMatrixForGameMovesToZero(int i, int j)
        {
            MatrixForGameMoves[i, j] = '\0';
        }

        public void PrintBord()
        {
            char CharForWidth = 'A';
            int i, j;

            Console.Write("  ");

            for (i = 1; i <= Width; i++)
            {
                Console.Write("  " + CharForWidth++.ToString() + " ");
            }

            for (i = 1; i <= Hight; i++)
            {
                Console.WriteLine(" ");
                Console.Write("  ");

                for (j = 0; j < Width * 4; j++)
                {
                    Console.Write("=");
                }

                Console.WriteLine("=");

                Console.Write(i.ToString());

                for (j = 0; j < Width; j++)
                {
                    if (MatrixForGameMoves[j, i - 1] == '\0')
                    {
                        Console.Write(" |  ");
                    }

                    else
                    {

                        Console.Write(" | " + MatrixForGameMoves[j, i - 1].ToString());
                    }
                }

                Console.Write(" |");

            }

            Console.WriteLine(" ");
            Console.Write("  ");

            for (j = 0; j < Width * 4; j++)
            {
                Console.Write("=");
            }

            Console.Write("=");
        }

        public void FillMatrix()
        {

            int Row, Col;
            char LetterToFillMatrix = 'A';
            Random rnd = new Random();

            for (int j = 0; j < Hight * Width / 2; j++)
            {

                for (int i = 0; i < 2; i++)
                {
                    Row = rnd.Next(Width);
                    Col = rnd.Next(Hight);

                    while (GameBoardMatrix[Row, Col].Equals('\0') == false)
                    {
                        Row = rnd.Next(Width);
                        Col = rnd.Next(Hight);
                    }

                    GameBoardMatrix[Row, Col] = LetterToFillMatrix;
                }

                LetterToFillMatrix++;
            }

        }

    }
}