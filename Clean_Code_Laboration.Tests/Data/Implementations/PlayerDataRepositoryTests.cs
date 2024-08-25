using Clean_Code_Laboration.Data.Implementations;
using Clean_Code_Laboration.Data.Interfaces;
using Moq;

namespace Clean_Code_Laboration.Tests.Data.Implementations
{
	[TestClass]
	public class PlayerDataRepositoryTests
	{
		private PlayerDataRepository _repository;
		private readonly Mock<IFileHandler> _fileHandlerMock = new Mock<IFileHandler>();

		[TestInitialize]
		public void Setup()
		{
			_repository = new PlayerDataRepository(_fileHandlerMock.Object);
		}

		[TestMethod]
		public void SavePlayerData_ShouldWriteCorrectDataToFile()
		{
			var playerName = "John";
			var guessCount = 5;
			
			_repository.SavePlayerData(playerName, guessCount);
			
			_fileHandlerMock.Verify(fh => fh.WriteLine(It.IsAny<string>(), $"{playerName}#&#{guessCount}"), Times.Once);
		}

		[TestMethod]
		public void GetPlayerData_ShouldReturnProcessedPlayerList()
		{
			var lines = new List<string>
			{
				"Anthony#&#5",
				"Patrik#&#7",
				"Anthony#&#3"
			};

			_fileHandlerMock
				.Setup(fh => fh.ReadLine(It.IsAny<string>()))
				.Returns(lines);
			
			var players = _repository.GetPlayerData();

			Assert.AreEqual(2, players.Count);
			Assert.AreEqual("Anthony", players[0].Name);
			Assert.AreEqual(4, players[0].Average());
			Assert.AreEqual("Patrik", players[1].Name);
			Assert.AreEqual(7, players[1].Average());
		}
	}
}
