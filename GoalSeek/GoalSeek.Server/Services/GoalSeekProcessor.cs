using GoalSeek.Server.Interfaces;
using GoalSeek.Server.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GoalSeek.Server.Services;

public class GoalSeekProcessor : IGoalSeekProcessor
{
    private readonly GoalSeekCalculator _seekCalculator;

    public GoalSeekProcessor(GoalSeekCalculator seekCalculator)
    {
        _seekCalculator = seekCalculator;
    }

    public GoalSeekResult Process(GoalSeekData seekData)
    {
        _seekCalculator.Formula = seekData.formula.ToString();
        _seekCalculator.Calculate(Convert.ToDecimal(seekData.input));
        var goalSeek = new Budoom.GoalSeek(_seekCalculator);
        goalSeek.MaxIterations = Convert.ToInt32(seekData.maximumIterations);
        var goalSeekResult = goalSeek.TrySeek(Convert.ToInt32(seekData.targetResult));

        var result = new GoalSeekResult()
        {
            targetInput = goalSeekResult.ClosestValue.ToString(),
            iteration = goalSeekResult.Iterations.ToString()
        };

        return result;

    }
}
