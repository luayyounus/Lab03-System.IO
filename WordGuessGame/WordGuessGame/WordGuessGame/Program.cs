using System;
using System.ComponentModel.Design;

namespace WordGuessGame
{
    class Game
    {
        /// <summary>
        /// This method shows a menu of options to the user to start the game, modify the database, exit the application.
        /// </summary>
        /// <returns>Returns true if the user inputs incorrect option</returns>
        static bool GameMenu()
        {
            Console.WriteLine("\n---------------------- Select an option from the Menu ------------------------" +
                              "\n---------------------- 1) Start!" +
                              "\n---------------------- 2) Admin Access" +
                              "\n---------------------- 3) Exit");
            string userInput = Console.ReadLine();
            if (userInput == "1")
            {
                // Start();
            }
            if (userInput == "2")
            {
                // AdminAccess();
            }
            if (userInput == "3")
            {
                return false;
            }
            Console.WriteLine("Please Select a correct number from the menu");
            return true;
        }

        /// <summary>
        /// This method presents the interface to the user with a welcome message and invoking the menu options.
        /// </summary>
        static void StartGuessGame()
        {
            Console.WriteLine("\n----------------------- Welcome to Word Guess Game! --------------------------" +
                              "\n In this game you have to guess the right word by entering a single character at a time" +
                              "\n When you try ten characters before guessing the mystery phrase, you lose the round.");
            bool gameOn = true;
            while (gameOn)
            {
                //gameOn = GameMenu();
            }
            Console.Read();
        }

        /// <summary>
        /// The main point entry for the application program.cs ... This starts the game by calling the Start method.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            StartGuessGame();
        }
    }
}
