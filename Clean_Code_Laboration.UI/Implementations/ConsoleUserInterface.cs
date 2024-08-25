using Clean_Code_Laboration.UI.Interfaces;

namespace Clean_Code_Laboration.UI.Implementations
{
    public class ConsoleUserInterface : IConsoleInterface
    {
        public string Input()
        {
            return Console.ReadLine();
        }

        public void Output(string message)
        {
            Console.Write(message);
        }
    }
}
