using Clean_Code_Laboration.Data.Interfaces;

namespace Clean_Code_Laboration.Data.Implementations
{
	public class FileHandler : IFileHandler
	{
		public void WriteLine(string filePath, string line)
		{
			using (StreamWriter writer = new StreamWriter(filePath, append: true))
			{
				writer.WriteLine(line);
			}
		}

		public IEnumerable<string> ReadLine(string filePath)
		{
			var lines = new List<string>();

			using (StreamReader reader = new StreamReader(filePath))
			{
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					lines.Add(line);
				}
			}
			return lines;
		}
	}
}
