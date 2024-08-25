using Clean_Code_Laboration.UI.AbstractClasses;
using Clean_Code_Laboration.UI.Interfaces;

namespace Clean_Code_Laboration.UI.Implementations
{
    public class MooUI : UserInterface
    {
        public MooUI(IConsoleInterface consoleUserInterface) : base(consoleUserInterface) { }
    }
}
