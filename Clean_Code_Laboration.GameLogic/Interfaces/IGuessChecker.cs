using Clean_Code_Laboration.GameLogic.Models;

namespace Clean_Code_Laboration.GameLogic.Interfaces
{
	public interface IGuessChecker
	{
		GuessResult CheckGuess(string goal, string guess);
	}
}
