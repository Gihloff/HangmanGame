using Xunit;
using Hangman.BLL;
using System.Diagnostics.Contracts;
using System.Reflection;

namespace Hangman.Tests
{
    public class HangmanGameTests
    {
        [Fact]
        public void TestWordGuessed()
        {
            var wordSource = new ConsoleWord();
            var game = new HangmanGame(wordSource);

            game.GameStart();

            bool isGuessed = game.IsWordGuessed();

            Assert.False(isGuessed);
        }

        [Fact]
        public void TestCorrectLetterGuess()
        {
            var wordSource = new ConsoleWord();
            var game = new HangmanGame(wordSource);

            game.GameStart();
            game.ProcessLetterGuess('a');
            bool isGuessed = game.IsWordGuessed();

            Assert.True(isGuessed);
        }

        [Fact]
        public void TestIncorrectLetterGuess()
        {
            var wordSource = new ConsoleWord();
            var game = new HangmanGame(wordSource);

            game.GameStart();
            game.ProcessLetterGuess('z');
            bool isGuessed = game.IsWordGuessed();

            Assert.False(isGuessed);
        }
    }
}