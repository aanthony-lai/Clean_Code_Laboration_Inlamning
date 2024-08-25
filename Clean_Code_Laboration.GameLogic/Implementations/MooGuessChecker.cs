using Clean_Code_Laboration.GameLogic.Interfaces;
using Clean_Code_Laboration.GameLogic.Models;

namespace Clean_Code_Laboration.GameLogic.Implementations
{
    public class MooGuessChecker : IGuessChecker
    {
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
                    Message = "Invalid guess. Your guess must consists of four UNIQUE numbers.",
                    IsValid = false
                };
            }

            string guessResult = CalculateCowsAndBulls(goal, guess);

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

            if (guess.Distinct().Count() != guess.Length)
                return false;

            return true;
        }

        private string CalculateCowsAndBulls(string goal, string guess)
        {
            int cows = 0;
            int bulls = 0;

            for (int i = 0; i < guess.Length; i++)
            {
                if (guess[i] == goal[i])
                {
                    bulls++;
                    continue;
                }
                if (goal.Contains(guess[i]))
                    cows++;
            }

            return $"{new string('B', bulls)},{new string('C', cows)}";
        }
    }
}