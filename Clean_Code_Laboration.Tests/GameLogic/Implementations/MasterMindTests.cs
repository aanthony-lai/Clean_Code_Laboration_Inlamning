using Clean_Code_Laboration.GameLogic.Enums;
using Clean_Code_Laboration.GameLogic.Games;
using Clean_Code_Laboration.GameLogic.Interfaces;
using Moq;

namespace Clean_Code_Laboration.Tests.GameLogic.Implementations
{
    [TestClass]
    public class MasterMindTests
    {
        private MasterMind? _masterMind;
        private readonly Mock<IGuessChecker> _guessCheckerMock = new Mock<IGuessChecker>();
        private readonly Mock<IGoalGenerator> _goalGeneratorMock = new Mock<IGoalGenerator>();

        [TestInitialize]
        public void Setup()
        {
            _masterMind = new MasterMind(_guessCheckerMock.Object, _goalGeneratorMock.Object);
        }

        [TestMethod]
        [DataRow("1234")]
        public void Play_ShouldGenerateGoal(string goal)
        {
            _goalGeneratorMock
                .Setup(gg => gg.GenerateGoal())
                .Returns(goal);

            _masterMind!.Play();

            Assert.IsNotNull(_masterMind.GetGoal());

            _goalGeneratorMock.Verify(gg => gg.GenerateGoal(), Times.Once);
        }

        [TestMethod]
        [DataRow("1234")]
        [DataRow("3456")]
        [DataRow("1256")]
        public void GetGoal_ShouldReturnTheGoal(string expectedGoal)
        {
            _goalGeneratorMock
                .Setup(gg => gg.GenerateGoal())
                .Returns(expectedGoal);

            _masterMind!.Play();

            Assert.AreEqual(expectedGoal, _masterMind.GetGoal());
        }

        [TestMethod]
        [DataRow("1234", "1234")]
        [DataRow("1234", "3456")]
        public void CheckGuess_ShouldCheckUsersGuess(string goal, string guess)
        {
            _goalGeneratorMock.Setup(gg => gg.GenerateGoal()).Returns(goal);
            _guessCheckerMock.Setup(gc => gc.CheckGuess(_masterMind!.GetGoal(), guess));

            _masterMind!.Play();

            _masterMind!.CheckGuess(guess);

            _guessCheckerMock.Verify(gc => gc.CheckGuess(_masterMind!.GetGoal(), guess), Times.Once);
        }

        [TestMethod]
        [DataRow(1)]
        public void SetGameMode_ShouldSetGameModeToPractice_WhenChoiceIsOne(int choice)
        {
            _masterMind!.SetGameMode(choice);

            Assert.AreEqual(GameMode.Practice, _masterMind!.GameMode);
        }

        [TestMethod]
        [DataRow(2)]
        public void SetGameMode_ShouldSetGameModeToRegular_WhenChoiceIsTwo(int choice)
        {
            _masterMind!.SetGameMode(choice);

            Assert.AreEqual(GameMode.Regular, _masterMind!.GameMode);
        }

        [TestMethod]
        [DataRow(3)]
        [DataRow(100)]
        public void SetGameMode_ShouldThrowException_WhenChoiceIsInvalid(int invalidChoice)
        {
            var exception = Assert.ThrowsException<InvalidOperationException>(() => _masterMind!.SetGameMode(invalidChoice));

            Assert.AreEqual("You've picked an invalid game mode.", exception.Message);
        }
    }
}
