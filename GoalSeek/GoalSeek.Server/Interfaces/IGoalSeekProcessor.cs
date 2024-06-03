using GoalSeek.Server.Models;

namespace GoalSeek.Server.Interfaces;

public interface IGoalSeekProcessor
{
    public GoalSeekResult Process(GoalSeekData seekData);
}
