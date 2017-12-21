using System;
using System.ComponentModel.Design;
using System.IO;

namespace WordGuessGame
{
    class Game
    {
        /// <summary>
        /// This method creates the passed in file path or adds to it if the file exists. Then it asks the user to input a word to be added to the list.
        /// </summary>
        /// <param name="filePath"></param>
        static void AddWordToList(string filePath)
        {
            Console.WriteLine($" Type in the word you want to add to your guessing list: ");
            string word = Console.ReadLine();
            
            using (StreamWriter wordList = new StreamWriter(filePath))
            {
                try
                {
                    wordList.Write(word);
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
                finally
                {
                    Console.WriteLine($" --> Added {word} to the guess words list <--");

                }
            }
        }

        /// <summary>
        /// Admin access to allow Josie to Add/Delete/View guess words.
        /// </summary>
        static void AdminAccess()
        {
            string path = "GuessingList.txt";
            bool adminIsDone = false;
            while(!adminIsDone)
            {
                Console.WriteLine("\n Select an option from the Menu" +
                                  "\n 1) View List of Words" +
                                  "\n 2) Add word" +
                                  "\n 3) Delete word" +
                                  "\n 4) Go back to Main Menu");
                string userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        //ViewListOfWords();
                        break;
                    case "2":
                        AddWordToList(path);
                        break;
                    case "3":
                        //DeleteWordFromList();
                        break;
                    case "4":
                        adminIsDone = true;
                        break;
                }
            }
        }

        /// <summary>
        /// This method shows a menu of options to the user to start the game, modify the database, exit the application.
        /// </summary>
        /// <returns>Returns true if the user inputs incorrect option</returns>
        static bool GameMenu()
        {
            Console.WriteLine("\n Select an option from the Menu" +
                              "\n 1) Start!" +
                              "\n 2) Admin Access" +
                              "\n 3) Exit");
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    //Start();
                    break;
                case "2":
                    AdminAccess();
                    break;
                case "3":
                    return false;
            }
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
                gameOn = GameMenu();
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
