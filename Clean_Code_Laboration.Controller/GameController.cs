using Clean_Code_Laboration.Controller.Interfaces;
using Clean_Code_Laboration.Data.Interfaces;
using Clean_Code_Laboration.GameLogic.Enums;
using Clean_Code_Laboration.GameLogic.Interfaces;
using Clean_Code_Laboration.GameLogic.Models;
using Clean_Code_Laboration.UI.AbstractClasses;

namespace Clean_Code_Laboration.Controller
{
	public class GameController
	{
		private readonly IGameFactory _gameFactory;
		private readonly IGameRegistry _gameRegistry;
		private readonly IPlayerDataRepository _playerDataRepository;

		private UserInterface _userInterface;
		private IGame _game;

		public GameController(
			IGame game,
			IGameFactory gameFactory,
			UserInterface userInterface,
			IGameRegistry gameRegistry,
			IPlayerDataRepository playerDataRepository)
		{
			_game = game;
			_gameFactory = gameFactory;
			_userInterface = userInterface;
			_gameRegistry = gameRegistry;
			_playerDataRepository = playerDataRepository;
			_gameFactory.GameChangeEvent += (_, newUserInterface) => _userInterface = newUserInterface;
		}

		public void Play()
		{
			while (true)
			{
				var playerName = _userInterface.PromptForPlayerName();

				StartNewGame();

				DisplaySelectedGameMode();

				HandleGuess();

				SavePlayerData(playerName, _game.GuessAttempts);

				_userInterface.DisplayResult(_game.GuessAttempts);

				ShowTopList();

				if (!PlayAgain()) break;
			}
		}

		private void StartNewGame()
		{
			SelectGame();
			SelectGameMode();
			_game.Play();
			_userInterface.Output("\nNew game has started!\n");
		}

		private void SelectGame()
		{
			while (true)
			{
				try
				{
					var allGames = _gameRegistry.GetAllGames();
					_userInterface.DisplayAllGames(allGames);

					var choice = _userInterface.PromptForGameChoice();
					_game = _gameFactory.CreateGame(choice);

					_userInterface.Output($"\nYou've selected: {_game.GetType().Name}\n");

					break;
				}
				catch (InvalidOperationException e)
				{
					_userInterface.Output(e.Message);
				}
			}
		}

		private void SelectGameMode()
		{
			while (true)
			{
				try
				{
					var choice = _userInterface.PromptForGameMode();
					_game.SetGameMode(choice);
					break;
				}
				catch (InvalidOperationException e)
				{
					_userInterface.Output(e.Message);
				}
			}
		}

		private void DisplaySelectedGameMode()
		{
			if (_game.GameMode == GameMode.Practice)
			{
				_userInterface.Output($"\nYour secret number is: {_game.GetGoal()}\n");
			}
			else
			{
				_userInterface.Output("\nNew game has started!\n");
			}
		}

		private void HandleGuess()
		{
			var guess = _userInterface.PromptForGuess();

			var guessResult = ProcessGuess(guess);

			while (!guessResult.IsCorrect)
			{
				_game.GuessAttempts++;
				guess = _userInterface.PromptForGuess();
				guessResult = ProcessGuess(guess);
			}
		}

		private GuessResult ProcessGuess(string guess)
		{
			var guessResult = _game.CheckGuess(guess);
			_userInterface.Output($"\n{guessResult.Message}\n");
			return guessResult;
		}

		private void SavePlayerData(string playerName, int guessAttempts)
		{
			_playerDataRepository.SavePlayerData(playerName, guessAttempts);
		}

		private void ShowTopList()
		{
			var playerData = _playerDataRepository.GetPlayerData();
			_userInterface.DisplayTopList(playerData);
		}

		private bool PlayAgain()
		{
			var choice = _userInterface.PromptForContinue();
			return choice.ToUpper() != "N";
		}
	}
}
