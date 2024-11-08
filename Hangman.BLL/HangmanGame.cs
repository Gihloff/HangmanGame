namespace Hangman.BLL
{

    public class HangmanGame
    {
        private readonly IWordSource WordSource;
        private string WordGuess;
        private char[] GuessedWord;
        private int Strikes;
        private string IncorrectGuesses;

        public HangmanGame(IWordSource wordSource)
        {
            WordSource = wordSource;
            Strikes = 5;
            IncorrectGuesses = string.Empty;
        }

        public void GameStart()
        {
            WordGuess = WordSource.GetWord();
            GuessedWord = new string('_', WordGuess.Length).ToCharArray();

            Console.WriteLine($"A random word has been selected. It is {WordGuess.Length} letters long.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();

            while (Strikes > 0)
            {
                DisplayGameState();
                string guess = GetGuess();

                if (guess.Length == 1)
                {
                    ProcessLetterGuess(guess[0]);
                }
                else
                {
                    ProcessWordGuess(guess);
                }


                if (IsWordGuessed())
                {
                    Console.WriteLine("Congratulations! You guessed the word!");
                    break;
                }
                if (Strikes == 0)
                {
                    Console.WriteLine($"Sorry, you lost! The word was: {WordGuess}");
                    break;
                }
            }
        }

        private void DisplayGameState()
        {
            // Console.Clear();
            Console.WriteLine($"Strikes Remaining: {Strikes}");
            Console.WriteLine();
            Console.WriteLine($"Word: {new string(GuessedWord)}");
            Console.WriteLine();
            Console.WriteLine("Incorrect Guesses: " + IncorrectGuesses);
        }

        private string GetGuess()
        {
            Console.Write("Enter Guess: ");
            string guess = Console.ReadLine()?.ToLower() ?? string.Empty;

            Console.WriteLine();

            return guess;
        }

        public void ProcessLetterGuess(char guess)
        {
            if (IncorrectGuesses.Contains(guess.ToString()))
            {
                Console.WriteLine($"You already guessed '{guess}'");
                Console.ReadKey();
                return;
            }

            bool found = false;

            for (int i = 0; i < WordGuess.Length; i++)
            {
                if (WordGuess[i] == guess)
                {
                    GuessedWord[i] = guess;
                    found = true;
                }
            }

            if (!found)
            {
                IncorrectGuesses += guess;
                Strikes--;
                Console.WriteLine($"Sorry, '{guess}' was not found!");
            }
            else
            {
                Console.WriteLine($"Correct! We found {CountLetterOccurrences(guess)} of '{guess}'!");
            }

            // DisplayGameState();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            Console.Clear();
        }

        private void ProcessWordGuess(string guess)
        {
            if (guess == WordGuess)
            {
                Array.Copy(WordGuess.ToCharArray(), GuessedWord, WordGuess.Length);
                Console.WriteLine("You guessed the word correctly! You win!");
            }
            else
            {
                Strikes--;
                Console.WriteLine($"Sorry, '{guess}' is not correct.");
            }

            // DisplayGameState();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            Console.Clear();
        }

        public bool IsWordGuessed()
        {
            return new string(GuessedWord) == WordGuess;
        }

        private int CountLetterOccurrences(char letter)
        {
            int count = 0;
            foreach (char c in WordGuess)
            {
                if (c == letter) count++;
            }
            return count;
        }
    }
}