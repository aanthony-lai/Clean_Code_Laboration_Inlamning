using Clean_Code_Laboration.Controller.Interfaces;
using Clean_Code_Laboration.GameLogic.Interfaces;
using Clean_Code_Laboration.UI.AbstractClasses;

namespace Clean_Code_Laboration.Controller.Factories
{
	public class GameFactory : IGameFactory
	{
		private readonly IGameRegistry _gameRegistry;

		public GameFactory(IGameRegistry gameRegistry)
		{
			_gameRegistry = gameRegistry;
		}

		public event EventHandler<UserInterface> GameChangeEvent;

		public IGame CreateGame(string choice)
		{
			var entry = _gameRegistry
				.GetGameCatalog()
				.FirstOrDefault(game => game.Key.ToUpper()
				.Contains(choice.ToUpper())).Value;

			if (entry != default)
			{
				GameChangeEvent.Invoke(this, entry.gameUI);
				return entry.gameLogic;
			}

			throw new InvalidOperationException("\nThe game that you've selected doesn't exist.\n");
		}
	}
}
