using System;
using Hangman.BLL;

namespace Hangman.UI
{
    class Program
    {
        static void Main()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Hangman!");
            Console.WriteLine();

            Console.Write("Enter your name: ");
            string PlayerName = Console.ReadLine();
            Console.WriteLine();

            bool PlayAgain = true;
            int Wins = 0;
            int Losses = 0;

            while (PlayAgain)
            {
                IWordSource wordSource = GetWordSourceChoice();

                HangmanGame game = new HangmanGame(wordSource);

                game.GameStart();

                if (game.IsWordGuessed())
                {
                    Wins++;
                }
                else
                {
                    Losses++;
                }

                Console.WriteLine($"{PlayerName}'s record: {Wins}W-{Losses}L");

                Console.Write("Play another game (y/n): ");
                PlayAgain = Console.ReadLine()?.ToLower() == "y";
            }
        }

        private static IWordSource GetWordSourceChoice()
        {
            Console.WriteLine("How would you like to choose your words?");
            Console.WriteLine();
            Console.WriteLine("1. Enter the word in the console window.");
            Console.WriteLine("2. Pick a random word from the dictionary for me.");
            Console.WriteLine();
            Console.WriteLine("Enter choice: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                return new ConsoleWord();
            }
            else
            {
                return new DictionaryWord();
            }
        }
    }
}