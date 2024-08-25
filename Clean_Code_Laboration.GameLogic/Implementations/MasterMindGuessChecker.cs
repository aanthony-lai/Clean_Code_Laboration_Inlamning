using Clean_Code_Laboration.Data.Models;
using Clean_Code_Laboration.GameLogic.Interfaces;
using Clean_Code_Laboration.GameLogic.Models;
using System.Text;

namespace Clean_Code_Laboration.GameLogic.Implementations
{
    public class MasterMindGuessChecker : IGuessChecker
    {
        private readonly Dictionary<char, string> _colors;

        public MasterMindGuessChecker()
        {
            _colors = new Dictionary<char, string>()
            {
                { '1', "Blue" },
                { '2', "Green" },
                { '3', "Red" },
                { '4', "Orange" },
                { '5', "Brown" },
                { '6', "Black" },
            };
        }

        public GuessResult CheckGuess(string goal, string guess)
        {
            if (CorrectGuess(goal, guess))
            {
                return new GuessResult
                {
                    IsCorrect = true
                };
            }

            if (!ValidateGuess(guess))
            {
                return new GuessResult
                {
                    Message = "Invalid guess. " +
                    "Your guess must consists of four numbers, and within the span of 1-6.",
                    IsValid = false
                };
            }

            string guessResult = CalculateCorrectColors(goal, guess);
            return new GuessResult
            {
                Message = guessResult,
                IsCorrect = false
            };
        }

        private bool CorrectGuess(string goal, string guess)
        {
            return goal == guess;
        }

        private bool ValidateGuess(string guess)
        {
            if (guess.Length != 4)
                return false;

            if (!guess.All(char.IsDigit))
                return false;

            if (!guess.All(n => int.Parse(n.ToString()) > 0 && int.Parse(n.ToString()) <= 6))
                return false;

            return true;
        }

        private string CalculateCorrectColors(string goal, string guess)
        {
            StringBuilder guessResultBuilder = new StringBuilder("{ ");

            for (int i = 0; i < guess.Length; i++)
            {
                if (guess[i] == goal[i])
                {
                    _colors.TryGetValue(guess[i], out var correctColor);
                    guessResultBuilder.Append($"[{correctColor}] ");
                    continue;
                }
                guessResultBuilder.Append("[X] ");
            }
            guessResultBuilder.Append("}");

            return guessResultBuilder.ToString();
        }

        static void showTopList()
        {
            StreamReader input = new StreamReader("result.txt");
            List<Player> results = new List<Player>();
            string line;
            while ((line = input.ReadLine()) != null)
            {
                string[] nameAndScore = line.Split(new string[] { "#&#" }, StringSplitOptions.None);
                string name = nameAndScore[0];
                int guesses = Convert.ToInt32(nameAndScore[1]);
                Player pd = new Player(name, guesses);
                int pos = results.IndexOf(pd);
                if (pos < 0)
                {
                    results.Add(pd);
                }
                else
                {
                    results[pos].Update(guesses);
                }


            }
            results.Sort((p1, p2) => p1.Average().CompareTo(p2.Average()));
            Console.WriteLine("Player   games average");
            foreach (Player p in results)
            {
                Console.WriteLine(string.Format("{0,-9}{1,5:D}{2,9:F2}", p.Name, p.NumberOfGames, p.Average()));
            }
            input.Close();
        }
    }
}
