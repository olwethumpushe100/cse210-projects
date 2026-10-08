using System;
using System.Collections.Generic;
using System.IO;

// Saves each completed activity to a text file and summarizes it on request.
public class ActivityLog
{
    private string _filePath;

    public ActivityLog(string filePath)
    {
        _filePath = filePath;
    }

    public void Record(string activityName, int seconds)
    {
        string line = $"{DateTime.Now:yyyy-MM-dd HH:mm}|{activityName}|{seconds}";
        File.AppendAllText(_filePath, line + Environment.NewLine);
    }

    public void DisplaySummary()
    {
        Console.Clear();
        Console.WriteLine("Your Mindfulness Log");
        Console.WriteLine();

        if (!File.Exists(_filePath))
        {
            Console.WriteLine("No activities have been logged yet.");
            return;
        }

        Dictionary<string, int> counts = new Dictionary<string, int>();
        Dictionary<string, int> totals = new Dictionary<string, int>();
        int totalSeconds = 0;

        foreach (string line in File.ReadAllLines(_filePath))
        {
            string[] parts = line.Split('|');

            if (parts.Length != 3 || !int.TryParse(parts[2], out int seconds))
            {
                continue;
            }

            string name = parts[1];
            counts[name] = counts.GetValueOrDefault(name) + 1;
            totals[name] = totals.GetValueOrDefault(name) + seconds;
            totalSeconds += seconds;
        }

        foreach (string name in counts.Keys)
        {
            Console.WriteLine($"{name}: {counts[name]} session(s), {totals[name]} seconds");
        }

        Console.WriteLine();
        Console.WriteLine($"Total mindful time: {totalSeconds} seconds");
    }
}
