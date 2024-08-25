using Clean_Code_Laboration.GameLogic.Implementations;

namespace Clean_Code_Laboration.Tests.GameLogic.Implementations
{
    [TestClass]
    public class MooGuessCheckerTests
    {
        private MooGuessChecker? _mooGuessChecker;

        [TestInitialize]
        public void Setup()
        {
            _mooGuessChecker = new MooGuessChecker();
        }

        [TestMethod]
        [DataRow("1234", "1234")]
        [DataRow("6789", "6789")]
        public void CheckGuess_ShouldReturnCorrectResult_WhenGuessIsCorrect(string goal, string guess)
        {
            var guessResult = _mooGuessChecker.CheckGuess(goal, guess);

            Assert.IsNotNull(guessResult);
            Assert.IsNull(guessResult.Message);
            Assert.IsTrue(guessResult.IsCorrect);
        }

        [TestMethod]
        [DataRow("1234", "1235", "BBB,")]
        [DataRow("1234", "2134", "BB,CC")]
        [DataRow("1234", "1423", "B,CCC")]
        [DataRow("1234", "4321", ",CCCC")]
        public void CheckGuess_ShouldReturnIncorrectResult_WhenGuessIsWrong(string goal, string guess, string expectedResult)
        {
            var guessResult = _mooGuessChecker.CheckGuess(goal, guess);

            Assert.IsNotNull(guessResult);
            Assert.IsFalse(guessResult.IsCorrect);
            Assert.IsNotNull(guessResult.Message);
            Assert.AreEqual(expectedResult, guessResult.Message);
        }

        [TestMethod]
        [DataRow("1234", "12345")]
        [DataRow("6789", "HelloWorld")]
        [DataRow("4567", "456")]
        [DataRow("1234", "1134")]
        [DataRow("1234", "")]
        public void CheckGuess_ShouldReturnInvalidResult_WhenGuessIsNotValid(string goal, string guess)
        {
            var guessResult = _mooGuessChecker.CheckGuess(goal, guess);

            Assert.IsNotNull(guessResult);
            Assert.AreEqual("Invalid guess. Your guess must consists of four UNIQUE numbers.", guessResult.Message);
            Assert.IsFalse(guessResult.IsValid);
        }
    }
}
