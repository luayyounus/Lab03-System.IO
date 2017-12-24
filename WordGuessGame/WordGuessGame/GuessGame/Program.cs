﻿using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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

        /// <summary>
        /// Show progress of current game every time the user enters a new guess letter.
        /// </summary>
        /// <param name="progress"> Character list of Letters and dashes based on user progress.</param>
        /// <param name="history" >Previous wrong entries by the user.</param>
        /// <param name="attemptsLeft"> Attempts left before losing the game.</param>
        private static void ShowProgress(string progress, string history, int attemptsLeft)
        {
            // Presenting the user progress letters/dashes
            Console.Write("\n Current Progress: \t");
            foreach (char c in progress)
            {
                Console.Write($"{c} ");
            }

            Console.Write($"\t Attempts Left: {attemptsLeft} \t");

            // Checking whether the history is empty or not, meaning the user has made incorrect entries
            if (!String.IsNullOrEmpty(history))
            {
                Console.Write($"Incorrect Guesses: ");

                // Writing the history letters to the console.
                foreach (var c in history)
                {
                    Console.Write($"{c} ");
                }
            }
            Console.WriteLine();
        }

        /// <summary>
        /// This method evaluate users input, keep track of their progress and incorrect entries. It also checks for invalid keyboard key entries.
        /// </summary>
        /// <param name="randomWord"></param>
        private static void StartGame(string randomWord)
        {
            Console.WriteLine("\n Guess the following word \n ");

            // Show progress of the user with underscores; Giving hint of guess word letters count.
            string progress = new String('_', randomWord.Length);

            // Incorrect entries initalized.
            string history = "";

            // Max number of wrong attempts before the user loses the game.
            int attempts = 10;

            // Showing the first progress which represents the starting underscores '_'.
            ShowProgress(progress, history, attempts);

            while (attempts != 0)
            {
                // Reading user's keyboard key to check against the guess word.
                ConsoleKeyInfo key = Console.ReadKey();
                char userInput = char.ToUpper(key.KeyChar);

                // Regular expressino to limit the user to Alphabetic letters only.
                Regex rgx = new Regex("^[A-Za-z]$");

                if (rgx.IsMatch(userInput.ToString()))
                {
                    // Checking user entry against the guess word to update both progress and history.
                    string[] tempResult = MatchGuess(userInput, randomWord, progress, history);

                    // Result of progress that is updated when user guesses right.
                    progress = tempResult[0];

                    // Checking if history changes, then update the attempts remaining.
                    if (tempResult[1] != history)
                    {
                        attempts--;
                        history = tempResult[1];
                    }

                    // Showing progress everytime the user enters a new character.
                    ShowProgress(progress, history, attempts);

                    // Checking if the user guessed the word to end the game!
                    if (randomWord == progress)
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("\n Please enter an alphabet.");
                }
            }
            Console.WriteLine(attempts == 0 ? "\n\n You Lost...." : "\n\n You Won!");
        }

        /// <summary>
        /// Match the user input with the guess word to update progress and history.
        /// </summary>
        /// <param name="userInput"> User character entry to match the guess word. </param>
        /// <param name="guessWord"> The hidden guess word.</param>
        /// <param name="progress"> Checking the progress and updating it.</param>
        /// <param name="history"> Keeping track of incorrect entries. </param>
        /// <returns>Returns Array that includes Progress at index 1, History at index 2.</returns>
        private static string[] MatchGuess(char userInput, string guessWord, string progress, string history)
        {
            // Making progress and guess word arrays to match indexes and replace underscore '_' with correct characters.
            char[] progressArray = progress.ToCharArray();
            char[] guessWordArray = guessWord.ToCharArray();

            // Array to return that holds both progress and history
            string[] progressAndHistory = { progress, history };

            if (guessWord.Contains(userInput))
            {
                for (int i = 0; i < guessWordArray.Length; i++)
                {
                    if (progress.Contains(userInput) || history.Contains(userInput))
                        break;

                    if (userInput == guessWordArray[i])
                    {
                        // Replace '_' with correct letter
                        progressArray[i] = guessWordArray[i];
                    }
                }
                progressAndHistory[0] = new string(progressArray);
            }
            else
            {
                // Update history of incorrect entries
                if (!history.Contains(userInput))
                {
                    StringBuilder historyStringBuilder = new StringBuilder(history);
                    historyStringBuilder.Append(userInput);
                    progressAndHistory[1] = historyStringBuilder.ToString();
                }
            }
            return progressAndHistory;
        }

        /// <summary>
        /// This method returns a random name from the guess words list file.
        /// </summary>
        /// <param name="filePath">Name of text file with the word lists.</param>
        /// <returns>Returns random guess word.</returns>
        private static string GetRandomGuessWord(string filePath)
        {
            // Reading and counting number of lines in file
            string[] wordFile = File.ReadAllLines(filePath);
            int lineCount = wordFile.Length;

            // Randomizing a number of maximum lines count from the filepath
            Random random = new Random();
            int randomNumber = random.Next(lineCount);

            // Getting a word from a random line in file.
            string randomGuessWord = wordFile[randomNumber];
            return randomGuessWord;
        }

        /// <summary>
        /// Admin access to allow the user to Add/Delete/View guess words.
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
        /// View list of guess words that exists in the words bank.
        /// </summary>
        /// <param name="filePath"> Name of text file with the word lists.</param>
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
                Console.WriteLine(" Please add new words to the game first.");
            }
        }

        /// <summary>
        /// This method creates the passed in file path or adds to it if the file exists. Then it asks the user to input a word to be added to the list.
        /// </summary>
        /// <param name="filePath"> Name of text file with the word lists.</param>
        private static void AddWordToList(string filePath)
        {
            Console.WriteLine(" Type in the word you want to add to your guessing list");
            string word = Console.ReadLine().ToUpper();

            if (!File.Exists(filePath))
            {
                using (FileStream fs = File.Create(filePath))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(word);
                    // Add word to the file.
                    fs.Write(info, 0, info.Length);
                }
            }
            else
            {
                try
                {
                    // Adding a new word to the guess word list by appending it on a new line.
                    using (StreamWriter wordList = File.AppendText(filePath))
                    {
                        wordList.WriteLine(word);
                    }
                    Console.WriteLine($" --> Added {word} to the guess words list <--");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
        }

        /// <summary>
        /// Delete word from guess list file.
        /// </summary>
        /// <param name="filePath"> Name of text file with the word lists.</param>
        private static void DeleteWordFromList(string filePath)
        {
            Console.WriteLine(" Type in the word you want to delete ");
            string word = Console.ReadLine().ToUpper();

            // Creating a temp file that we use to replace the original file after deleting
            string tempFile = Path.GetTempFileName();

            using (StreamReader sr = new StreamReader(filePath))
            using (StreamWriter sw = new StreamWriter(tempFile))
            {
                string line;

                // Matching line string with user input for deletion
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
    }
}
