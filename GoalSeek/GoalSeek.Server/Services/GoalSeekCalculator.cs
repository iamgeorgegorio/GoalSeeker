using NCalc;

namespace GoalSeek.Server.Services;

public class GoalSeekCalculator : Budoom.IGoalSeek
{
    public string  Formula { get; set; }
    public decimal Calculate(decimal input)
    {
        var formula = Formula;
        decimal res;

        formula = formula.Replace("input", input.ToString());

        try
        {
            var expr = new Expression(formula);
            var output = expr.Evaluate();
            return Convert.ToDecimal(output);
        }
        catch (Exception ex)
        {

            res = 0;
        }

        return res;
    }
}
