using System;


namespace B20_Ex02
{
    struct MatrixIndex
    {
        public int i;
        public int j;
    }


    class TheGameProcess
    {

        public TheGameProcess(int i_Hight, int i_Width, string i_FirsPlayer, string i_SecondPlayer)
        {
            PlayerOne = i_FirsPlayer;
            PlayerTwo = i_SecondPlayer;
            BoardGame = new BoardGame(i_Hight, i_Width);
            m_NumberOfFirstPlayerPoints = 0;
            m_NumberOfSecondPlayerPoints = 0;
            m_SelectedIndexesInMatrix = new MatrixIndex[2];

            if (PlayerTwo == "Computer")
            {
                ComputerMemory = new char[i_Hight, i_Width];
            }

        }

        private char[,] ComputerMemory;
        private int m_WhoTurn = 0;
        private MatrixIndex[] m_SelectedIndexesInMatrix;
        private string m_PlayerOne;
        private string m_PlayerTwo;
        private BoardGame m_BoardGame;
        private int m_NumberOfFirstPlayerPoints;
        private int m_NumberOfSecondPlayerPoints;

        public string PlayerOne { get => m_PlayerOne; set => m_PlayerOne = value; }
        public string PlayerTwo { get => m_PlayerTwo; set => m_PlayerTwo = value; }
        public int WhoTurn { get => m_WhoTurn; set => m_WhoTurn = value; }
        public int NumberOfFirstPlayerPoints { get => m_NumberOfFirstPlayerPoints; set => m_NumberOfFirstPlayerPoints = value; }
        public int NumberOfSecondPlayerPoints { get => m_NumberOfSecondPlayerPoints; set => m_NumberOfSecondPlayerPoints = value; }
        public BoardGame BoardGame { get => m_BoardGame; set => m_BoardGame = value; }


        public void UpdateSelectedIndexesInMatrix(int i_index, int i_i, int i_j)
        {
            m_SelectedIndexesInMatrix[i_index].i = i_i;
            m_SelectedIndexesInMatrix[i_index].j = i_j;
        }

        public void UpdateNumberOfFirstPlayerPoints()
        {
            NumberOfFirstPlayerPoints++;
        }

        public void UpdateNumberOfSecondPlayerPoints()
        {
            NumberOfSecondPlayerPoints++;
        }

        public void Score()
        {
            Console.WriteLine(PlayerOne + " - " + NumberOfFirstPlayerPoints.ToString() + " points");
            Console.WriteLine(PlayerTwo + " - " + NumberOfSecondPlayerPoints.ToString() + " points");
            Console.WriteLine();
        }

        public void TurnNow()
        {
            if (WhoTurn == 1)
            {
                Console.WriteLine("now turn " + PlayerOne);
            }

            else
            {
                Console.WriteLine("now turn " + PlayerTwo);
            }
        }

        public void PlayGame()
        {
            Random RandomStartGame;
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine("The game is starting!!!");
            System.Threading.Thread.Sleep(1000);
            RandomStartGame = new Random();


            WhoTurn = RandomStartGame.Next(1, 3);
            while (NumberOfFirstPlayerPoints + NumberOfSecondPlayerPoints != BoardGame.Hight * BoardGame.Width / 2)
            {
                ClearAndRedraw();

                if (m_PlayerTwo.Equals("Computer") && WhoTurn == 2)
                {
                    ComputeCardCelection();
                }

                else
                {
                    PlayerCardCelection();
                }

                if (IsCompatible() == false)
                {
                    if (WhoTurn == 1)
                    {
                        WhoTurn = 2;
                    }

                    else
                    {
                        WhoTurn = 1;
                    }

                    if (m_PlayerTwo.Equals("Computer"))
                    {

                        for (int i = 0; i < 2; i++)
                        {
                            ComputerMemory[m_SelectedIndexesInMatrix[i].i, m_SelectedIndexesInMatrix[i].j] = BoardGame.GameBoardMatrix[m_SelectedIndexesInMatrix[i].i, m_SelectedIndexesInMatrix[i].j];
                        }

                    }
                }

                else
                {
                    if (WhoTurn == 1)
                    {
                        UpdateNumberOfFirstPlayerPoints();
                    }

                    else
                    {
                        UpdateNumberOfSecondPlayerPoints();
                    }

                }

            }

            WhoWin();
        }

        public void PlayerCardCelection()
        {
            string CardCelection;
            char CharFromSelection;
            int NumberFromSelection = 0;
            int widhchosen = 0;
            bool IsValidChoice = false;
            int i;

            for (i = 0; i < 2; i++)
            {
                Console.WriteLine("");

                if (i == 0)
                {
                    Console.WriteLine("please choose a first card, letter and then number");
                }

                else
                {
                    Console.WriteLine("please choose a secend card, letter and then number");
                }

                CardCelection = Console.ReadLine();

                while (IsValidChoice == false)
                {
                    Console.WriteLine("");

                    if (CardCelection == "Q")
                    {
                        Environment.Exit(0);
                    }

                    else if (CardCelection.Length != 2)
                    {
                        Console.WriteLine("you need two charester, first letter and then number");
                    }

                    else if (char.TryParse(CardCelection.Substring(0, 1), out CharFromSelection) == true
                        && int.TryParse(CardCelection.Substring(1, CardCelection.Length - 1), out NumberFromSelection) == true)
                    {
                        if (CharFromSelection < 'A' || CharFromSelection > 'Z')
                        {
                            Console.WriteLine("Invalid input, you need first letter and then number");
                        }

                        else if (CharFromSelection - 'A' > BoardGame.Width || NumberFromSelection > BoardGame.Hight || NumberFromSelection < 1)
                        {
                            Console.WriteLine("You have exceeded the limits of the array");
                        }

                        else
                        {
                            widhchosen = CharFromSelection - 'A';

                            if (BoardGame.MatrixForGameMoves[widhchosen, NumberFromSelection - 1].Equals('\0'))
                            {
                                IsValidChoice = true;
                            }

                            else
                            {
                                Console.WriteLine("The selected cardis occupied");
                            }
                        }
                    }

                    else
                    {
                        Console.WriteLine("Invalid input, you need first letter and then number");
                    }

                    if (IsValidChoice == false)
                    {
                        Console.WriteLine("Please try again:");
                        CardCelection = Console.ReadLine();
                    }
                }

                IsValidChoice = false;
                BoardGame.MatrixForGameMoves[widhchosen, NumberFromSelection - 1] = BoardGame.GameBoardMatrix[widhchosen, NumberFromSelection - 1];
                UpdateSelectedIndexesInMatrix(i, widhchosen, NumberFromSelection - 1);

                ClearAndRedraw();

            }
        }

        public void WhoWin()
        {
            ClearAndRedraw();

            Console.WriteLine("\n");

            if (NumberOfFirstPlayerPoints > NumberOfSecondPlayerPoints)
            {
                Console.WriteLine("the winner is: " + PlayerOne + " With " + NumberOfFirstPlayerPoints + " points");
            }

            else if (NumberOfFirstPlayerPoints < NumberOfSecondPlayerPoints)
            {
                Console.WriteLine("the winner is: " + PlayerTwo + " With " + NumberOfSecondPlayerPoints + " points");
            }

            else
            {
                Console.WriteLine("A draw in the game, no winner");
            }
        }

        public bool IsCompatible()
        {
            bool IfTheCardsAreEual = true;

            if (BoardGame.GameBoardMatrix[m_SelectedIndexesInMatrix[0].i, m_SelectedIndexesInMatrix[0].j].Equals(BoardGame.GameBoardMatrix[m_SelectedIndexesInMatrix[1].i, m_SelectedIndexesInMatrix[1].j]) == false)
            {
                BoardGame.UpdateIndexMatrixForGameMovesToZero(m_SelectedIndexesInMatrix[0].i, m_SelectedIndexesInMatrix[0].j);
                BoardGame.UpdateIndexMatrixForGameMovesToZero(m_SelectedIndexesInMatrix[1].i, m_SelectedIndexesInMatrix[1].j);
                System.Threading.Thread.Sleep(2000);
                IfTheCardsAreEual = false;
            }

            return IfTheCardsAreEual;
        }

        public void ComputeCardCelection()
        {
            int IndexWidh = 0;
            int IndexHigh = 0, k, j, i;
            bool DoesTheComputerRemember = false;

            System.Threading.Thread.Sleep(2000);
            Random ComputerChoice;
            ComputerChoice = new Random();

            for (i = 0; i < 2; i++)
            {
                if (i == 1)
                {

                    for (k = 0; k < BoardGame.Hight; k++)
                    {

                        for (j = 0; j < BoardGame.Width; j++)
                        {

                            if ((BoardGame.GameBoardMatrix[IndexWidh, IndexHigh] == ComputerMemory[j, k]) && (j != IndexWidh || k != IndexHigh))
                            {

                                IndexWidh = j;
                                IndexHigh = k;
                                DoesTheComputerRemember = true;
                                break;

                            }

                        }

                        if (DoesTheComputerRemember == true)
                        {
                            break;
                        }

                    }
                }

                if (DoesTheComputerRemember == false)
                {
                    IndexWidh = ComputerChoice.Next(0, BoardGame.Width);
                    IndexHigh = ComputerChoice.Next(0, BoardGame.Hight);

                    while (BoardGame.MatrixForGameMoves[IndexWidh, IndexHigh].Equals('\0') == false)
                    {
                        IndexWidh = ComputerChoice.Next(0, BoardGame.Width);
                        IndexHigh = ComputerChoice.Next(0, BoardGame.Hight);
                    }
                }

                BoardGame.MatrixForGameMoves[IndexWidh, IndexHigh] = BoardGame.GameBoardMatrix[IndexWidh, IndexHigh];
                UpdateSelectedIndexesInMatrix(i, IndexWidh, IndexHigh);
                ClearAndRedraw();

            }

        }

        public void ClearAndRedraw()
        {

            Ex02.ConsoleUtils.Screen.Clear();
            Score();
            BoardGame.PrintBord();
            Console.WriteLine();
            TurnNow();

        }

    }

}

