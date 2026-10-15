public abstract class Goal
{
    private string _shortName;
    private string _description;
    protected int _points;

    public Goal(string name, string description, int points)
    {
        _shortName = name;
        _description = description;
        _points = points;
    }

    public string GetShortName()
    {
        return _shortName;
    }

    protected string GetDescription()
    {
        return _description;
    }

    // Records one accomplishment and returns the points earned (or lost).
    public abstract int RecordEvent();

    public abstract bool IsComplete();

    public abstract string GetStringRepresentation();

    public virtual string GetDetailsString()
    {
        string checkbox = IsComplete() ? "[X]" : "[ ]";
        return $"{checkbox} {_shortName} ({_description})";
    }

    // The values every goal saves, so derived classes don't repeat them.
    protected string GetBaseValues()
    {
        return $"{_shortName}|{_description}|{_points}";
    }
}
