using Clean_Code_Laboration.GameLogic.Enums;
using Clean_Code_Laboration.GameLogic.Interfaces;
using Clean_Code_Laboration.GameLogic.Models;

namespace Clean_Code_Laboration.GameLogic.Games
{
	public class MooGame : IGame
	{
		private readonly IGuessChecker _guessChecker;
		private readonly IGoalGenerator _goalGenerator;
		private string _goal;
		private int _guessAttempts;

		public GameMode GameMode { get; private set; }
		public int GuessAttempts 
		{
			get { return _guessAttempts; }
			set { _guessAttempts += 1; }
		}


		public MooGame(
			IGuessChecker guessChecker, 
			IGoalGenerator goalGenerator)
		{
			_guessChecker = guessChecker;
			_goalGenerator = goalGenerator;
		}

		public void Play()
		{
			_goal = _goalGenerator.GenerateGoal();
			_guessAttempts = 1;
		}

		public string GetGoal()
		{
			return _goal;
		}

		public GuessResult CheckGuess(string guess)
		{
			return _guessChecker.CheckGuess(_goal, guess);
		}

		public void SetGameMode(int choice)
		{
			switch (choice)
			{
				case 1:
					GameMode = GameMode.Practice;
					break;
				case 2:
					GameMode = GameMode.Regular;
					break;
				default:
					throw new InvalidOperationException("You've picked an invalid game mode.");
			}
		}
	}
}