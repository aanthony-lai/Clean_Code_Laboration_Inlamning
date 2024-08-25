using Clean_Code_Laboration.GameLogic.Interfaces;
using System.Text;

namespace Clean_Code_Laboration.GameLogic.Implementations
{
    public class MasterMindGoalGenerator : IGoalGenerator
    {
        private readonly Random _randomNumberGenerator;

        public MasterMindGoalGenerator()
        {
            _randomNumberGenerator = new Random();
        }

        public string GenerateGoal()
        {
            StringBuilder goalBuilder = new StringBuilder();

            for (int i = 0; i < 4; i++)
            {
                var randomNumber = _randomNumberGenerator.Next(1, 7);
                goalBuilder.Append(randomNumber.ToString());
            }

            return goalBuilder.ToString();
        }
    }
}
