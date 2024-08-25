using Clean_Code_Laboration.GameLogic.Enums;
using Clean_Code_Laboration.GameLogic.Models;
using Clean_Code_Laboration.UI.AbstractClasses;

namespace Clean_Code_Laboration.GameLogic.Interfaces
{
	public interface IGame
	{
		void Play();
		public void SetGameMode(int choice);
		string GetGoal();
		int GuessAttempts { get; set; }
		GameMode GameMode { get; }
		GuessResult CheckGuess(string guess);
	}
}
 