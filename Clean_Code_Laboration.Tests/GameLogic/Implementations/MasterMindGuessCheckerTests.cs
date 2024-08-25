using Clean_Code_Laboration.GameLogic.Implementations;

namespace Clean_Code_Laboration.Tests.GameLogic.Implementations
{
	[TestClass]
	public class MasterMindGuessCheckerTests
	{
		private MasterMindGuessChecker? _masterMindGuessChecker;

		[TestInitialize]
		public void Setup()
		{
			_masterMindGuessChecker = new MasterMindGuessChecker();
		}

		[TestMethod]
		[DataRow("1234", "1234")]
		[DataRow("3456", "3456")]
		public void CheckGuess_ShouldReturnCorrectResult_WhenGuessIsCorrect(string goal, string guess)
		{
			var guessResult = _masterMindGuessChecker.CheckGuess(goal, guess);

			Assert.IsNotNull(guessResult);
			Assert.IsNull(guessResult.Message);
			Assert.IsTrue(guessResult.IsCorrect);
		}

		[TestMethod]
		[DataRow("1234", "1135", "{ [Blue] [X] [Red] [X] }")]
		[DataRow("1234", "1235", "{ [Blue] [Green] [Red] [X] }")]
		[DataRow("1234", "1345", "{ [Blue] [X] [X] [X] }")]
		public void CheckGuess_ShouldReturnIncorrectResult_WhenGuessIsWrong(string goal, string guess, string expectedResult)
		{
			var guessResult = _masterMindGuessChecker.CheckGuess(goal, guess);

			Assert.IsNotNull(guessResult);
			Assert.IsFalse(guessResult.IsCorrect);
			Assert.IsNotNull(guessResult.Message);
			Assert.AreEqual(expectedResult, guessResult.Message);
		}

		[TestMethod]
		[DataRow("1234", "12345")]
		[DataRow("6789", "HelloWorld")]
		[DataRow("4567", "456")]
		[DataRow("1234", "")]
		[DataRow("1234", "0123")]
		public void CheckGuess_ShouldReturnInvalidResult_WhenGuessIsNotValid(string goal, string guess)
		{
			var guessResult = _masterMindGuessChecker.CheckGuess(goal, guess);

			Assert.IsNotNull(guessResult);
			Assert.AreEqual("Invalid guess. Your guess must consists of four numbers, and within the span of 1-6.", guessResult.Message);
			Assert.IsFalse(guessResult.IsValid);
		}
	}
}
