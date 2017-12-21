using System;
using System.ComponentModel.Design;

namespace WordGuessGame
{
    class Game
    {
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
