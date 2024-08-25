using Clean_Code_Laboration.GameLogic.Enums;
using Clean_Code_Laboration.GameLogic.Games;
using Clean_Code_Laboration.GameLogic.Interfaces;
using Moq;

namespace Clean_Code_Laboration.Tests.GameLogic.Implementations
{
    [TestClass]
    public class MooGameTests
    {
        private MooGame _mooGame;
        private readonly Mock<IGuessChecker> _guessCheckerMock = new Mock<IGuessChecker>();
        private readonly Mock<IGoalGenerator> _goalGeneratorMock = new Mock<IGoalGenerator>();

        [TestInitialize]
        public void Setup()
        {
            _mooGame = new MooGame(_guessCheckerMock.Object, _goalGeneratorMock.Object);
        }

        [TestMethod]
        [DataRow("1234")]
        public void Play_ShouldGenerateGoal(string goal)
        {
            _goalGeneratorMock
                .Setup(gg => gg.GenerateGoal())
                .Returns(goal);

            _mooGame.Play();

            Assert.IsNotNull(_mooGame.GetGoal());

            _goalGeneratorMock.Verify(gg => gg.GenerateGoal(), Times.Once);
        }

        [TestMethod]
        [DataRow("1234")]
        [DataRow("6789")]
        [DataRow("3456")]
        public void GetGoal_ShouldReturnTheGoal(string expectedGoal)
        {
            _goalGeneratorMock
                .Setup(gg => gg.GenerateGoal())
                .Returns(expectedGoal);

            _mooGame.Play();

            Assert.AreEqual(expectedGoal, _mooGame.GetGoal());
        }

        [TestMethod]
        [DataRow("1234", "1234")]
        [DataRow("1234", "5678")]
        public void CheckGuess_ShouldCheckUsersGuess(string goal, string guess)
        {
            _goalGeneratorMock.Setup(gg => gg.GenerateGoal()).Returns(goal);
            _guessCheckerMock.Setup(gc => gc.CheckGuess(_mooGame.GetGoal(), guess));

            _mooGame.Play();

            _mooGame.CheckGuess(guess);

            _guessCheckerMock.Verify(gc => gc.CheckGuess(_mooGame.GetGoal(), guess), Times.Once);
        }

        [TestMethod]
        [DataRow(1)]
        public void SetGameMode_ShouldSetGameModeToPractice_WhenChoiceIsOne(int choice)
        {
            _mooGame.SetGameMode(choice);

            Assert.AreEqual(GameMode.Practice, _mooGame.GameMode);
        }

        [TestMethod]
        [DataRow(2)]
        public void SetGameMode_ShouldSetGameModeToRegular_WhenChoiceIsTwo(int choice)
        {
            _mooGame.SetGameMode(choice);

            Assert.AreEqual(GameMode.Regular, _mooGame.GameMode);
        }

        [TestMethod]
        [DataRow(3)]
        [DataRow(100)]
        public void SetGameMode_ShouldThrowException_WhenChoiceIsInvalid(int invalidChoice)
        {
            var exception = Assert.ThrowsException<InvalidOperationException>(() => _mooGame.SetGameMode(invalidChoice));

            Assert.AreEqual("You've picked an invalid game mode.", exception.Message);
        }
    }
}
