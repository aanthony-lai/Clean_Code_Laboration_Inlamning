using Clean_Code_Laboration.Data.Interfaces;
using Clean_Code_Laboration.Data.Models;

namespace Clean_Code_Laboration.Data.Implementations
{
	public class PlayerDataRepository : IPlayerDataRepository
	{
		private readonly IFileHandler _fileHandler;
		private readonly string _filePath;

		public PlayerDataRepository(IFileHandler fileHandler)
		{
			_fileHandler = fileHandler;
			_filePath = Path.Combine(GetProjectDirectory(), "result.txt");
			EnsureFileExists(_filePath);
		}

		public void SavePlayerData(string playerName, int guessCount)
		{
			var data = $"{playerName}#&#{guessCount}";
			_fileHandler.WriteLine(_filePath, data);
		}

		public List<Player> GetPlayerData()
		{
			var lines = _fileHandler.ReadLine(_filePath);
			return ProcessPlayerData(lines);
		}

		private List<Player> ProcessPlayerData(IEnumerable<string> lines)
		{
			var results = new List<Player>();

			foreach (var line in lines)
			{
				var nameAndScore = line.Split(new[] { "#&#" }, StringSplitOptions.None);
				var name = nameAndScore[0];
				var guesses = Convert.ToInt32(nameAndScore[1]);
				var playerData = new Player(name, guesses);

				var existingPlayer = results.FirstOrDefault(p => p.Name == name);
				if (existingPlayer == null)
				{
					results.Add(playerData);
				}
				else
				{
					existingPlayer.Update(guesses);
				}
			}

			results.Sort((p1, p2) => p1.Average().CompareTo(p2.Average()));
			return results;
		}

		private string GetProjectDirectory()
		{
			return Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
		}

		private void EnsureFileExists(string filePath)
		{
			if (!File.Exists(filePath))
			{
				File.Create(filePath).Dispose();
			}
		}
	}
}
