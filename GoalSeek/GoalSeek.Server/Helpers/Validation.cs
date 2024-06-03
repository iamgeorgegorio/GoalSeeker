using GoalSeek.Server.Interfaces;
using GoalSeek.Server.Models;
using NCalc;

namespace GoalSeek.Server.Helpers;

public class Validation : IValidation
{
    public string CheckExpression(GoalSeekData data)
    {
        string result = string.Empty;
        string expression = string.Empty;

        if (data == null)
        {
            result = "Invalid data!";
            return result;
        }

        if (string.IsNullOrEmpty(data.formula))
        {
            result = "Formula is empty.";
            return result;
        }

        if (string.IsNullOrEmpty(data.input.ToString()))
        {
            result = "Input value is empty.";
            return result;
        }
        else
        {
            int i;
            bool success = int.TryParse(data.input.ToString(), out i);
            if (!success)
            {
                result = "Input value is invalid.";
                return result;
            }
        }


        if (string.IsNullOrEmpty(data.targetResult.ToString()))
        {
            result = "Target result value is empty.";
            return result;
        }
        else
        {
            int i;
            bool success = int.TryParse(data.targetResult.ToString(), out i);
            if (!success)
            {
                result = "Target result value is invalid.";
                return result;
            }
        }

        if (string.IsNullOrEmpty(data.maximumIterations.ToString()))
        {
            result = "Maximum iterations value is empty.";
            return result;
        }
        else
        {
            int i;
            bool success = int.TryParse(data.maximumIterations.ToString(), out i);
            if (!success)
            {
                result = "Maximum iterations value is invalid.";
                return result;
            }
        }

        try
        {
            expression = data.formula.Replace("input", data.input.ToString());
            var expr = new Expression(expression);
            var exprResult = expr.Evaluate();
        }
        catch (Exception ex)
        {
            result = "Invalid formula";
        }

        return result;
    }
}
