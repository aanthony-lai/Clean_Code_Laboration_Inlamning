using Clean_Code_Laboration.UI.AbstractClasses;
using Clean_Code_Laboration.UI.Interfaces;

namespace Clean_Code_Laboration.UI.Implementations
{
    public class MasterMindUI : UserInterface
    {
        public MasterMindUI(IConsoleInterface consoleUserInterface) : base(consoleUserInterface) { }

        public override string PromptForGuess()
        {
            Console.Output(
                "\n1 = Blue\n" +
                "2 = Green\n" +
                "3 = Red\n" +
                "4 = Orange\n" +
                "5 = Brown\n" +
                "6 = Black\n");

            Console.Output("\nEnter your guess: ");

            return Console.Input();
        }
    }
}
