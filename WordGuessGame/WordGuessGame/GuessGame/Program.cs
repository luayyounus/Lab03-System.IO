using System;

namespace GuessGame
{
    class Program
    {
        /// <summary>
        /// The main point entry for the application program.cs ... This initialize the game for the user to start.
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            InitGame();
        }

        /// <summary>
        /// This method includes the welcome message and the game instructions. It also invokes the Game Menu.
        /// </summary>
        private static void InitGame()
        {
            Console.WriteLine("\n----------------------- Welcome to Word Guess Game! --------------------------" +
                              "\n In this game you have to guess the right word by entering a single character at a time" +
                              "\n When you try ten characters before guessing the mystery phrase, you lose the round.");
            bool gameOn = true;
            while (gameOn)
            {
                //gameOn = ShowGameMenu();
            }
        }
    }
}
