using System;
using System.IO;
using System.Linq;
using System.Text;

namespace WordGuessGame
{
    class Game
    {
        /// <summary>
        /// This method returns a random name from the guess words list.
        /// </summary>
        /// <param name="filePath">Name of file in system.</param>
        /// <returns>Returns random name.</returns>
        private static string RandomGuessWord(string filePath)
        {
            //Reading and counting number of lines in file
            var wordFile = File.ReadAllLines(filePath);
            int lineCount = wordFile.Length;

            //Randomizing a number of maximum lines count from the filepath
            Random random = new Random();
            int randomNumber = random.Next(lineCount);

            //Getting a word from a specific line
            string randomGuessWord = wordFile.Skip(randomNumber).Take(1).First();
            return randomGuessWord;
        }

        /// <summary>
        /// //Show progress of current game every time the user makes a new letter guess.
        /// </summary>
        /// <param name="progress">Character list of Letters and dashes based on user progress.</param>
        /// <param name="history">Previous wrong entries by the user.</param>
        private static void ShowProgress(char[] progress, char[] history)
        {
            // Presenting the user progress letters/dashes
            Console.Write("\n Current Progress:   ");
            foreach (char c in progress)
            {
                Console.Write($"{c} ");
            }

            // Checking if the history is empty to hide and show history log
            if (history[0] != '\0')
            {
                Console.Write("   History: ");
                foreach (var c in history)
                {
                    Console.Write($"{c} ");
                }
            }
            Console.WriteLine();
        }


        /// <summary>
        /// View list of guess words entered by the admin for the guessing Game.
        /// </summary>
        /// <param name="filePath"></param>
        private static void ViewListOfWords(string filePath)
        {
            try
            {
                using (StreamReader sr = File.OpenText(filePath))
                {
                    string word = "";
                    
                    // Read every line in file and present it on the console screen
                    while ((word = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(word);
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// This method creates the passed in file path or adds to it if the file exists. Then it asks the user to input a word to be added to the list.
        /// </summary>
        /// <param name="filePath"></param>
        private static void AddWordToList(string filePath)
        {
            Console.WriteLine(" Type in the word you want to add to your guessing list");
            string word = Console.ReadLine().ToUpper();

            if (!File.Exists(filePath))
                File.Create(filePath);

            try
            {
                // Adding a new word to the guess word list by appending it on a new line
                using (StreamWriter wordList = File.AppendText(filePath))
                {
                    try
                    {
                        wordList.WriteLine(word);
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
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Delete word from guess list.
        /// </summary>
        /// <param name="filePath"></param>
        private static void DeleteWordFromList(string filePath)
        {
            Console.WriteLine(" Type in the word you want to delete ");

            string word = Console.ReadLine().ToUpper();

            // Creating a temp file that we use to replace the original file after deleting
            string tempFile = Path.GetTempFileName();

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                using (StreamWriter sw = new StreamWriter(tempFile))
                {
                    string line;
                    
                    // Matching line string with user input for deleting
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line != word)
                            sw.WriteLine(line);
                    }
                }
                
                // Delete the file used with Reader
                File.Delete(filePath);

                // Replacing the new file with previous Read file 
                File.Move(tempFile, filePath);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Admin access to allow Josie to Add/Delete/View guess words.
        /// </summary>
        private static void AdminAccess(string filePath)
        {
            bool adminIsDone = false;
            while (!adminIsDone)
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
                        ViewListOfWords(filePath);
                        break;
                    case "2":
                        AddWordToList(filePath);
                        break;
                    case "3":
                        DeleteWordFromList(filePath);
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
        private static bool GameMenu()
        {
            Console.WriteLine("\n Select an option from the Menu" +
                              "\n 1) Start!" +
                              "\n 2) Admin Access" +
                              "\n 3) Exit");
            string userInput = Console.ReadLine();

            string path = "GuessingList.txt";

            switch (userInput)
            {
                case "1":
                    Start(path);
                    break;
                case "2":
                    AdminAccess(path);
                    break;
                case "3":
                    return false;
            }
            return true;
        }

        /// <summary>
        /// This method presents the interface to the user with a welcome message and invoking the menu options.
        /// </summary>
        private static void StartGuessGame()
        {
            Console.WriteLine("\n----------------------- Welcome to Word Guess Game! --------------------------" +
                              "\n In this game you have to guess the right word by entering a single character at a time" +
                              "\n When you try ten characters before guessing the mystery phrase, you lose the round.");
            bool gameOn = true;
            while (gameOn)
            {
                gameOn = GameMenu();
            }
        }

        /// <summary>
        /// The main point entry for the application program.cs ... This starts the game by calling the Start method.
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            StartGuessGame();
        }
    }
}
