using Clean_Code_Laboration.UI.Implementations;
using Clean_Code_Laboration.UI.Interfaces;
using Moq;
using System;

namespace Clean_Code_Laboration.Tests.UI.Implementations
{
    [TestClass]
    public class MooUITests
    {
        private MooUI _mooUI;
        private Mock<IConsoleInterface> _consoleMock = new Mock<IConsoleInterface>();

        [TestInitialize]
        public void Setup()
        {
            _mooUI = new MooUI(_consoleMock.Object);
        }

        [TestMethod]
        public void PromptForPlayerName_ShouldReturnValidName_WhenInputIsValid()
        {
            var invalidInput = " ";
            var validInput = "Anthony";

            _consoleMock
                .SetupSequence(c => c.Input())
                .Returns(invalidInput)
                .Returns(validInput);

            var input = _mooUI.PromptForPlayerName();

            Assert.AreEqual(validInput, input);

            _consoleMock.Verify(c => c.Output("\nEnter your user name: "), Times.Exactly(2));
            _consoleMock.Verify(c => c.Output("\nUser name can't be empty, or only consists spaces.\n"), Times.Once);
        }

        [TestMethod]
        public void DisplayAllGames_ShouldOutputAllGames()
        {
            var games = new List<string> { "Game1", "Game2", "Game3" };

            _mooUI.DisplayAllGames(games);

            _consoleMock.Verify(c => c.Output("\nAll games:\n"), Times.Once);
            _consoleMock.Verify(c => c.Output("* Game1\n"), Times.Once);
            _consoleMock.Verify(c => c.Output("* Game2\n"), Times.Once);
            _consoleMock.Verify(c => c.Output("* Game3\n"), Times.Once);
        }

        [TestMethod]
        [DataRow("Moo")]
        [DataRow("Master")]
        public void PromptForGameChoice_ShouldReturnValidChoice_WhenInputIsValid(string validInput)
        {
            _consoleMock
                .SetupSequence(c => c.Input())
                .Returns(validInput);

            var result = _mooUI.PromptForGameChoice();

            Assert.AreEqual(validInput, result);

            _consoleMock.Verify(c => c.Output("\nSelect a game: "), Times.Once);
        }

        [TestMethod]
        public void PromptForGameChoice_ShouldPromptUntilValidInput_WhenInputIsInvalid()
        {
            var emptyInput = string.Empty;
            var validInput = "Moo";

            _consoleMock
                .SetupSequence(c => c.Input())
                .Returns(emptyInput)
                .Returns(validInput);

            var result = _mooUI.PromptForGameChoice();

            Assert.AreEqual(validInput, result);

            _consoleMock.Verify(c => c.Output("\nSelect a game: "), Times.Exactly(2));
            _consoleMock.Verify(c => c.Output("Your input can't be empty."), Times.Once);
        }

        [TestMethod]
        [DataRow("1")]
        [DataRow("2")]
        public void PromptForGameMode_ShouldReturnSingleDigitNumber_WhenValidInput(string validInput)
        {
            _consoleMock
                .SetupSequence(c => c.Input())
                .Returns(validInput);

            var result = _mooUI.PromptForGameMode();

            Assert.IsInstanceOfType(result, typeof(int));

            _consoleMock.Verify(c => c.Output("\nChoose your game mode: \n(1) Practice Mode\n(2) Regular Mode\n"), Times.Once);
            _consoleMock.Verify(c => c.Output("\nSelect a game mode: "), Times.Once);
        }

        [TestMethod]
        [DataRow("", "1")]
        [DataRow("abc", "2")]
        public void PromptForGameMode_ShouldPromptUntilValidInput_WhenInputIsInvalid(string invalidInput, string validInput)
        {
            _consoleMock
                .SetupSequence(c => c.Input())
                .Returns(invalidInput)
                .Returns(validInput);

            var result = _mooUI.PromptForGameMode();

            Assert.IsInstanceOfType(result, typeof(int));

            _consoleMock.Verify(c => c.Output("\nChoose your game mode: \n(1) Practice Mode\n(2) Regular Mode\n"), Times.Exactly(2));
            _consoleMock.Verify(c => c.Output("\nSelect a game mode: "), Times.Exactly(2));

            _consoleMock.Verify(c => c.Output("* Input must be a number."), Times.Once);
            _consoleMock.Verify(c => c.Output("* Input can't be empty."), Times.Once);
        }

        [TestMethod]
        [DataRow("1234")]
        [DataRow("5678")]
        public void PromptForGuess_ShouldReturnUsersGuess(string input)
        {
            _consoleMock
                .SetupSequence(c => c.Input())
                .Returns(input);

            var result = _mooUI.PromptForGuess();

            Assert.AreEqual(input, result);

            _consoleMock.Verify(c => c.Output("\nEnter your guess: "), Times.Once);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(10)]
        [DataRow(100)]
        public void DisplayResult_ShouldDisplayResult_WhenPlayerHasWon(int guessAttempts)
        {
            _mooUI.DisplayResult(guessAttempts);

            _consoleMock.Verify(c => c.Output($"Correct, it took you {guessAttempts} guess/es\n"), Times.Once);
        }

        [TestMethod]
        [DataRow("Y")]
        [DataRow("N")]
        [DataRow("y")]
        [DataRow("n")]
        [DataRow("anything else")]
        public void PromptForContinue_PromptPlayerToPlayAgain(string input)
        {
            _consoleMock
                .SetupSequence(c => c.Input())
                .Returns(input);

            var result = _mooUI.PromptForContinue();

            Assert.AreEqual(input, result);

            _consoleMock.Verify(c => c.Output("\nDo you want to play again? Y/N: "), Times.Once);
        }
    }
}
