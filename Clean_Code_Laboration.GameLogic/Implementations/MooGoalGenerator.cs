using Clean_Code_Laboration.GameLogic.Interfaces;

namespace Clean_Code_Laboration.GameLogic.Implementations
{
    public class MooGoalGenerator : IGoalGenerator
    {
        private readonly Random _randomNumberGenerator;

        public MooGoalGenerator()
        {
            _randomNumberGenerator = new Random();
        }

        public string GenerateGoal()
        {
            return string.Concat(Enumerable.Range(0, 10)
                .OrderBy(n => _randomNumberGenerator.Next())
                .Take(4));
        }
    }
}
