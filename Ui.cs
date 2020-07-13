using System;

namespace B20_Ex02
{
    class Ui
    {

        public void InputUserinformation()
        {

            string FirsPlayer = "", SecondPlayer = "";
            int ComputerOrPlayer = 0, Width = 0, Hight = 0, HighMat = 1;

            GameData(ref ComputerOrPlayer, ref HighMat, ref FirsPlayer, ref SecondPlayer);
            MemoryBoardSize(ref Hight, ref Width);

            if (ComputerOrPlayer == 1)
            {
                GameAgainstPlayer(Hight, Width, FirsPlayer, SecondPlayer);
            }

            else
            {
                GameAgainstPlayer(Hight, Width, FirsPlayer, "Computer");
            }

        }


        public static void GameAgainstPlayer(int i_Hight, int i_Width, string i_FirsPlayer, string i_SecondPlayer)
        {
            int Rematch = 0;
            string ReadRematch;

            while (Rematch == 0 || Rematch == 1)
            {
                TheGameProcess StartPlayAgainstPlayer = new TheGameProcess(i_Hight, i_Width, i_FirsPlayer, i_SecondPlayer);
                StartPlayAgainstPlayer.PlayGame();

                Console.WriteLine("Want to play again?\n1 - yes\n2 - no");
                ReadRematch = Console.ReadLine();

                if (ReadRematch.Equals("2") == true)
                {
                    Rematch = 2;
                }

                else if (ReadRematch.Equals("1") == true)
                {
                    Rematch = 1;
                }

                else
                {
                    Console.WriteLine("Invalid key, please try again");
                }

            }

        }


        public static void NamePlayar(ref String i_Player)
        {
            i_Player = Console.ReadLine();
        }


        public static void GameData(ref int io_ComputerOrPlayer, ref int io_highMat, ref string io_FirsPlayer, ref string io_SecondPlayer)
        {
            Console.WriteLine("enter your name");
            NamePlayar(ref io_FirsPlayer);
            Console.WriteLine("1 - For a game against a player\n2 - for play against a computer");

            while ((io_ComputerOrPlayer != 1) && (io_ComputerOrPlayer != 2))
            {
                int.TryParse(Console.ReadLine(), out io_ComputerOrPlayer);

                if ((io_ComputerOrPlayer != 1) && (io_ComputerOrPlayer != 2))
                {
                    Console.WriteLine("invalid key,you neeed enter 1/2");
                }

            }

            if (io_ComputerOrPlayer == 1)
            {
                Console.WriteLine("please enter name of player two");
                NamePlayar(ref io_SecondPlayer);
            }

        }


        public static void MemoryBoardSize(ref int io_Hight, ref int io_Width)
        {

            Console.WriteLine("please enter hight and width of the board");

            while (io_Hight == 0 && io_Width == 0)
            {

                if (int.TryParse(Console.ReadLine(), out io_Hight) == false)
                {
                    Console.WriteLine("Syntax error, re-enter number, please enter width and after hight ");
                }

                else if (int.TryParse(Console.ReadLine(), out io_Width) == false)
                {
                    Console.WriteLine("Syntax error, re-enter number,please enter width and after hight");
                }

                else if (io_Hight < 4 || io_Hight > 6 || io_Width < 4 || io_Width > 6)
                {
                    io_Hight = 0;
                    io_Width = 0;

                    Console.WriteLine("Error, numbers greater than 6 and less than 4 must not be entered,please enter width and after hight");

                }

                else if (io_Hight * io_Width % 2 == 1)
                {
                    io_Hight = 0;
                    io_Width = 0;

                    Console.WriteLine("Error, board size is not even, please re-enter number,please enter width and after hight");


                }


            }

        }

    }

}
