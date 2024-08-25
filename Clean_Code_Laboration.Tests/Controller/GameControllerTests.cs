using Clean_Code_Laboration.Controller;
using Clean_Code_Laboration.Controller.Interfaces;
using Clean_Code_Laboration.Data.Interfaces;
using Clean_Code_Laboration.GameLogic.Interfaces;
using Clean_Code_Laboration.GameLogic.Models;
using Clean_Code_Laboration.UI.AbstractClasses;
using Clean_Code_Laboration.UI.Implementations;
using Clean_Code_Laboration.UI.Interfaces;
using Moq;

namespace Clean_Code_Laboration.Tests.Controller
{
	[TestClass]
	public class GameControllerTests
	{
		private readonly Mock<IGame> _gameMock = new Mock<IGame>();
		private readonly Mock<IPlayerDataRepository> _repositoryMock = new Mock<IPlayerDataRepository>();
		private readonly Mock<IGameFactory> _gameFactoryMock = new Mock<IGameFactory>();
		private readonly Mock<IGameRegistry> _gameRegistryMock = new Mock<IGameRegistry>();
		private readonly Mock<IConsoleInterface> _consoleMock = new Mock<IConsoleInterface>();

		private MooUI? _userInterface;
		private GameController? _gameController;

		[TestInitialize]
		public void Setup()
		{
			_userInterface = new MooUI(_consoleMock.Object);

			_gameController = new GameController(
				_gameMock.Object, 
				_gameFactoryMock.Object, 
				_userInterface, 
				_gameRegistryMock.Object, 
				_repositoryMock.Object
			);
		}
	}
}
