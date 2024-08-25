using Clean_Code_Laboration.GameLogic.Interfaces;
using Clean_Code_Laboration.UI.AbstractClasses;

namespace Clean_Code_Laboration.Controller.Interfaces
{
	public interface IGameFactory
	{
		IGame CreateGame(string choice);
		event EventHandler<UserInterface> GameChangeEvent;
	}
}
