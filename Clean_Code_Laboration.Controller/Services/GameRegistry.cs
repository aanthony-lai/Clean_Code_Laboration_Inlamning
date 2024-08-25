using Clean_Code_Laboration.Controller.Interfaces;
using Clean_Code_Laboration.GameLogic.Games;
using Clean_Code_Laboration.GameLogic.Implementations;
using Clean_Code_Laboration.GameLogic.Interfaces;
using Clean_Code_Laboration.UI.AbstractClasses;
using Clean_Code_Laboration.UI.Implementations;

namespace Clean_Code_Laboration.Controller.Services
{
	public class GameRegistry: IGameRegistry
	{
		private readonly Dictionary<string, (IGame gameLogic, UserInterface gameUI)> _gameCatalog;

		public GameRegistry()
		{
			_gameCatalog = new Dictionary<string, (IGame gameLogic, UserInterface gameUI)>()
			{
				{
					"Moo game",
					(
						new MooGame(new MooGuessChecker(), new MooGoalGenerator()),
						new MooUI(new ConsoleUserInterface())
					)
				},
				{
					"Master mind",
					(
						new MasterMind(new MasterMindGuessChecker(), new MasterMindGoalGenerator()),
						new MasterMindUI(new ConsoleUserInterface())
					)
				}
			};
		}

		public List<string> GetAllGames()
		{
			return _gameCatalog.Select(game => game.Key).ToList();
		}

		public Dictionary<string, (IGame gameLogic, UserInterface gameUI)> GetGameCatalog()
		{
			return _gameCatalog;
		}
	}
}
