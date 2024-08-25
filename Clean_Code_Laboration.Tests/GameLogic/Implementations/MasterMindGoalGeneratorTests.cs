using Clean_Code_Laboration.GameLogic.Implementations;

namespace Clean_Code_Laboration.Tests.GameLogic.Implementations
{
    [TestClass]
    public class MasterMindGoalGeneratorTests
    {
        private MasterMindGoalGenerator? _masterMindGoalGenerator;

        [TestInitialize]
        public void Setup()
        {
            _masterMindGoalGenerator = new MasterMindGoalGenerator();
        }

        [TestMethod]
        public void GenerateGoal_ShouldReturnStringOfFourNumbers()
        {
            var goal = _masterMindGoalGenerator.GenerateGoal();

            Assert.IsNotNull(goal);
            Assert.IsInstanceOfType<string>(goal);
            Assert.AreEqual(4, goal.Length);
            Assert.IsTrue(goal.All(char.IsDigit));
            Assert.IsTrue(goal.Any(n => int.Parse(n.ToString()) >= 1 && int.Parse(n.ToString()) <= 6));
        }
    }
}
