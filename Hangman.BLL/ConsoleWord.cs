namespace Hangman.BLL
{
    public class ConsoleWord : IWordSource
    {
        public string GetWord()
        {
            Console.Clear();
            Console.WriteLine("Enter the first word to guess: ");
            string Word = Console.ReadLine();
            Console.Clear();
            return Word?.ToLower() ?? string.Empty;
        }
    }
}