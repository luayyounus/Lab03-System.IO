using System;
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
    }
}
