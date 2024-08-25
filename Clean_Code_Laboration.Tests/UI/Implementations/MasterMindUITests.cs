using Clean_Code_Laboration.UI.Implementations;
using Clean_Code_Laboration.UI.Interfaces;
using Moq;

namespace Clean_Code_Laboration.Tests.UI.Implementations
{
    [TestClass]
    public class MasterMindUITests
    {
        private MasterMindUI? _masterMindUI;
        private Mock<IConsoleInterface> _consoleMock = new Mock<IConsoleInterface>();

        [TestInitialize]
        public void Setup()
        {
            _masterMindUI = new MasterMindUI(_consoleMock.Object);
        }

        [TestMethod]
        [DataRow("1234")]
        [DataRow("123")]
        [DataRow("ABC123")]
        [DataRow("AnyInput")]
        public void PromptForGuess_ShouldReturnUsersGuess(string input)
        {
            _consoleMock
                .SetupSequence(c => c.Input())
                .Returns(input);

            var result = _masterMindUI.PromptForGuess();

            Assert.AreEqual(input, result);

            _consoleMock.Verify(c => c.Output("\n1 = Blue\n2 = Green\n3 = Red\n4 = Orange\n5 = Brown\n6 = Black\n"), Times.Once);
            _consoleMock.Verify(c => c.Output("\nEnter your guess: "), Times.Once);
        }
    }
}
