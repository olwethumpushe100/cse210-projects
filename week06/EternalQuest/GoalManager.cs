using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private List<Goal> _goals;
    private int _score;
    private int _pointsPerLevel = 1000;
    private string[] _levelTitles =
    {
        "Wandering Pilgrim",
        "Faithful Apprentice",
        "Steady Traveler",
        "Diligent Seeker",
        "Valiant Pathfinder",
        "Noble Guardian",
        "Legendary Ninja Unicorn"
    };

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
    }

    public void Start()
    {
        string choice = "";

        while (choice != "6")
        {
            Console.WriteLine();
            DisplayPlayerInfo();
            Console.WriteLine();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Create New Goal");
            Console.WriteLine("  2. List Goals");
            Console.WriteLine("  3. Save Goals");
            Console.WriteLine("  4. Load Goals");
            Console.WriteLine("  5. Record Event");
            Console.WriteLine("  6. Quit");
            Console.Write("Select a choice from the menu: ");
            choice = Console.ReadLine();
            Console.WriteLine();

            if (choice == "1")
            {
                CreateGoal();
            }
            else if (choice == "2")
            {
                ListGoalDetails();
            }
            else if (choice == "3")
            {
                SaveGoals();
            }
            else if (choice == "4")
            {
                LoadGoals();
            }
            else if (choice == "5")
            {
                RecordEvent();
            }
            else if (choice != "6")
            {
                Console.WriteLine("Please choose a number from 1 to 6.");
            }
        }

        Console.WriteLine("Keep going on your Eternal Quest!");
    }

    public void DisplayPlayerInfo()
    {
        int level = GetLevel();
        int pointsIntoLevel = Math.Max(0, _score) % _pointsPerLevel;

        Console.WriteLine($"You have {_score} points.");
        Console.WriteLine($"Level {level}: {GetLevelTitle(level)} ({pointsIntoLevel}/{_pointsPerLevel} to next level)");
    }

    public void ListGoalNames()
    {
        Console.WriteLine("The goals are:");

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetShortName()}");
        }
    }

    public void ListGoalDetails()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("You have no goals yet.");
            return;
        }

        Console.WriteLine("The goals are:");

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
    }

    public void CreateGoal()
    {
        Console.WriteLine("The types of Goals are:");
        Console.WriteLine("  1. Simple Goal");
        Console.WriteLine("  2. Eternal Goal");
        Console.WriteLine("  3. Checklist Goal");
        Console.WriteLine("  4. Negative Goal (bad habit)");
        int type = AskForNumber("Which type of goal would you like to create? ");

        if (type < 1 || type > 4)
        {
            Console.WriteLine("That is not a goal type.");
            return;
        }

        string name = AskForText("What is the name of your goal? ");
        string description = AskForText("What is a short description of it? ");
        int points = AskForNumber("What is the amount of points associated with this goal? ");

        if (type == 1)
        {
            _goals.Add(new SimpleGoal(name, description, points));
        }
        else if (type == 2)
        {
            _goals.Add(new EternalGoal(name, description, points));
        }
        else if (type == 3)
        {
            int target = AskForNumber("How many times does this goal need to be accomplished for a bonus? ");
            int bonus = AskForNumber("What is the bonus for accomplishing it that many times? ");
            _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
        }
        else
        {
            _goals.Add(new NegativeGoal(name, description, points));
        }
    }

    public void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("You have no goals yet. Create one first.");
            return;
        }

        ListGoalNames();
        int choice = AskForNumber("Which goal did you accomplish? ");

        if (choice < 1 || choice > _goals.Count)
        {
            Console.WriteLine("That is not one of your goals.");
            return;
        }

        Goal goal = _goals[choice - 1];

        if (goal.IsComplete())
        {
            Console.WriteLine("That goal is already complete.");
            return;
        }

        int oldLevel = GetLevel();
        int points = goal.RecordEvent();
        _score += points;

        if (points >= 0)
        {
            Console.WriteLine($"Congratulations! You have earned {points} points!");
        }
        else
        {
            Console.WriteLine($"Oh no! You lost {-points} points. Tomorrow is a new day.");
        }

        Console.WriteLine($"You now have {_score} points.");

        int newLevel = GetLevel();

        if (newLevel > oldLevel)
        {
            Console.WriteLine();
            Console.WriteLine($"*** LEVEL UP! You are now level {newLevel}: {GetLevelTitle(newLevel)} ***");
        }
    }

    public void SaveGoals()
    {
        string filename = AskForText("What is the filename for the goal file? ");

        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            outputFile.WriteLine(_score);

            foreach (Goal goal in _goals)
            {
                outputFile.WriteLine(goal.GetStringRepresentation());
            }
        }

        Console.WriteLine($"Saved {_goals.Count} goals to {filename}.");
    }

    public void LoadGoals()
    {
        string filename = AskForText("What is the filename for the goal file? ");

        if (!File.Exists(filename))
        {
            Console.WriteLine("That file was not found.");
            return;
        }

        try
        {
            string[] lines = File.ReadAllLines(filename);
            List<Goal> loadedGoals = new List<Goal>();
            int loadedScore = int.Parse(lines[0]);

            for (int i = 1; i < lines.Length; i++)
            {
                if (lines[i].Trim() != "")
                {
                    loadedGoals.Add(CreateGoalFromString(lines[i]));
                }
            }

            _score = loadedScore;
            _goals = loadedGoals;
            Console.WriteLine($"Loaded {_goals.Count} goals from {filename}.");
        }
        catch (Exception)
        {
            Console.WriteLine("That file could not be read. Your current goals were not changed.");
        }
    }

    private Goal CreateGoalFromString(string line)
    {
        string[] parts = line.Split('|');
        string type = parts[0];
        string name = parts[1];
        string description = parts[2];
        int points = int.Parse(parts[3]);

        if (type == "SimpleGoal")
        {
            return new SimpleGoal(name, description, points, bool.Parse(parts[4]));
        }
        else if (type == "EternalGoal")
        {
            return new EternalGoal(name, description, points);
        }
        else if (type == "ChecklistGoal")
        {
            int bonus = int.Parse(parts[4]);
            int target = int.Parse(parts[5]);
            int amountCompleted = int.Parse(parts[6]);
            return new ChecklistGoal(name, description, points, target, bonus, amountCompleted);
        }
        else if (type == "NegativeGoal")
        {
            return new NegativeGoal(name, description, points);
        }

        throw new FormatException("Unknown goal type.");
    }

    private int GetLevel()
    {
        return Math.Max(0, _score) / _pointsPerLevel + 1;
    }

    private string GetLevelTitle(int level)
    {
        int index = Math.Min(level - 1, _levelTitles.Length - 1);
        return _levelTitles[index];
    }

    // The save file uses | as a separator, so it can't appear in text.
    private string AskForText(string prompt)
    {
        Console.Write(prompt);
        string text = Console.ReadLine();
        return text.Replace("|", "-");
    }

    private int AskForNumber(string prompt)
    {
        int number;
        Console.Write(prompt);

        while (!int.TryParse(Console.ReadLine(), out number) || number < 0)
        {
            Console.Write("Please enter a whole number (0 or more): ");
        }

        return number;
    }
}
