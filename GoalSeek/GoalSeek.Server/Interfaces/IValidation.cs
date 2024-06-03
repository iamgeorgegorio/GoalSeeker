using GoalSeek.Server.Models;

namespace GoalSeek.Server.Interfaces;

public interface IValidation
{
    string CheckExpression(GoalSeekData data);
}
