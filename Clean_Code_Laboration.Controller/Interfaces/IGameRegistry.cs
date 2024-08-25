using Clean_Code_Laboration.GameLogic.Interfaces;
using Clean_Code_Laboration.UI.AbstractClasses;

namespace Clean_Code_Laboration.Controller.Interfaces
{
	public interface IGameRegistry
	{
		public List<string> GetAllGames();
		public Dictionary<string, (IGame gameLogic, UserInterface gameUI)> GetGameCatalog();
	}
}
