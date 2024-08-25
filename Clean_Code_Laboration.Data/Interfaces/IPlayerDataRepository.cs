using Clean_Code_Laboration.Data.Models;
using System.Diagnostics;

namespace Clean_Code_Laboration.Data.Interfaces
{
	public interface IPlayerDataRepository
	{
		void SavePlayerData(string playerName, int guessCount);
		List<Player> GetPlayerData();
	}
}
