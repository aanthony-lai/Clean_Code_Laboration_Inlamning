using Clean_Code_Laboration.Data.Models;
using Clean_Code_Laboration.UI.Interfaces;

namespace Clean_Code_Laboration.UI.AbstractClasses
{
	public abstract class UserInterface
	{
		private readonly IConsoleInterface _console;

		public UserInterface(IConsoleInterface console)
		{
			_console = console;
		}

		protected IConsoleInterface Console => _console;

		public void Output(string message)
		{
			_console.Output(message);
		}

		public string Input()
		{
			return _console.Input();
		}

		public string PromptForPlayerName()
		{
			_console.Output("\nEnter your user name: ");
			var playerName = _console.Input();

			while (string.IsNullOrWhiteSpace(playerName))
			{
				_console.Output("\nUser name can't be empty, or only consists spaces.\n");
				_console.Output("\nEnter your user name: ");
				playerName = _console.Input();
			}
			return playerName;
		}

		public virtual void DisplayAllGames(List<string> gameList)
		{
			_console.Output("\nAll games:\n");
			for (int i = 0; i < gameList.Count; i++)
			{
				_console.Output($"* {gameList[i]}\n");
			}
		}

		public virtual string PromptForGameChoice()
		{
			_console.Output("\nSelect a game: ");
			var input = _console.Input();

			while(string.IsNullOrEmpty(input))
			{
				_console.Output("Your input can't be empty.");
				_console.Output("\nSelect a game: ");
				input = _console.Input();
			}
			return input;
		}

		public virtual int PromptForGameMode()
		{
			while (true)
			{
				try
				{
					_console.Output(
						"\nChoose your game mode: \n" +
						"(1) Practice Mode\n" +
						"(2) Regular Mode\n");
					_console.Output("\nSelect a game mode: ");

					return int.Parse(_console.Input());
				}
				catch (FormatException)
				{
					_console.Output("* Input must be a number.");
					_console.Output("* Input can't be empty.");
				}
			}
		}

		public virtual string PromptForGuess()
		{
			_console.Output("\nEnter your guess: ");

			return _console.Input();
		}

		public void DisplayResult(int guessAttempts)
		{
			_console.Output($"Correct, it took you {guessAttempts} guess/es\n");
		}

		public string PromptForContinue()
		{
			_console.Output("\nDo you want to play again? Y/N: ");
			return _console.Input();
		}

		public void DisplayTopList(List<Player> playerData)
		{
			Output("Player | no.Games | Average\n");
			foreach (var p in playerData)
			{
				Output(string.Format("{0,-9}|{1,5:D}|{2,9:F2}\n", p.Name, p.NumberOfGames, p.Average()));
			}
		}
	}
}
