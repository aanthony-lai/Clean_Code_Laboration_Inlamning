namespace Clean_Code_Laboration.Data.Interfaces
{
	public interface IFileHandler
	{
		void WriteLine(string filePath, string line);
		IEnumerable<string> ReadLine(string filePath);
	}
}
