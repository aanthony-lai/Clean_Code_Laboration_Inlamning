using Clean_Code_Laboration.GameLogic.Implementations;

namespace Clean_Code_Laboration.Tests.GameLogic.Implementations
{
    [TestClass]
    public class MooGoalGeneratorTests
    {
        private MooGoalGenerator? _mooGoalGenerator;

        [TestInitialize]
        public void Setup()
        {
            _mooGoalGenerator = new MooGoalGenerator();
        }

        [TestMethod]
        public void GenerateGoal_ShouldReturnStringOfFourUniqueNumbers()
        {
            var goal = _mooGoalGenerator.GenerateGoal();

            Assert.IsNotNull(goal);
            Assert.IsInstanceOfType<string>(goal);
            Assert.AreEqual(4, goal.Length);
            Assert.IsTrue(goal.All(char.IsDigit));
            Assert.AreEqual(goal.Distinct().Count(), goal.Length);
        }
    }
}
