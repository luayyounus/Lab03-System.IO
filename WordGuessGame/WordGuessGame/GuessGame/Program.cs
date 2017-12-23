using System;
using System.IO;

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
                gameOn = ShowGameMenu();
            }
        }

        /// <summary>
        /// This method shows a menu of options to the user to start the game, modify the database, exit the application.
        /// </summary>
        /// <returns>Returns true if the user finished the game or inputs incorrect option.</returns>
        private static bool ShowGameMenu()
        {
            Console.WriteLine("\n Select an option from the Menu" +
                              "\n 1) Start!" +
                              "\n 2) Admin Access" +
                              "\n 3) Exit");

            string userInput = Console.ReadLine();

            // File name that includes the guess words list to read from and write to later using streams.
            string wordsFilePath = "GuessingList.txt";

            // Switching over the user choice.
            switch (userInput)
            {
                case "1":
                    // Checing if file exists to start the get a random guess word and start the game.
                    if (!File.Exists(wordsFilePath))
                    {
                        Console.WriteLine(" No words available. Access the admin page to add words.");
                        return true;
                    }

                    //string randomWord = GetRandomGuessWord(wordsFilePath);
                    //StartGame(randomWord);
                    break;
                case "2":
                    //AdminAccess(wordsFilePath);
                    break;
                case "3":
                    return false;
            }
            return true;
        }
    }
}
