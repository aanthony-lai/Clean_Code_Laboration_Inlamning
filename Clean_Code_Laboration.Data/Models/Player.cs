namespace Clean_Code_Laboration.Data.Models
{
	public class Player
	{
		public string Name { get; init; }
		public int NumberOfGames { get; private set; }
		public int TotalAttempts { get; private set; }

		public Player(string name, int attempts)
		{
			Name = name;
			NumberOfGames = 1;
			TotalAttempts = attempts;
		}

		public void Update(int attempts)
		{
			TotalAttempts += attempts;
			NumberOfGames++;
		}

		public double Average()
		{
			return (double)TotalAttempts / NumberOfGames;
		}
	}
}
