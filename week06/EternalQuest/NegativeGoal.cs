// A "bad habit" goal: recording it costs the player points.
public class NegativeGoal : Goal
{
    public NegativeGoal(string name, string description, int points) : base(name, description, points)
    {
    }

    public override int RecordEvent()
    {
        return -_points;
    }

    public override bool IsComplete()
    {
        return false;
    }

    public override string GetDetailsString()
    {
        return $"[!] {GetShortName()} ({GetDescription()}) -- Costs {_points} points each time";
    }

    public override string GetStringRepresentation()
    {
        return $"NegativeGoal|{GetBaseValues()}";
    }
}
