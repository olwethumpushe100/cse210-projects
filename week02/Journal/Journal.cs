using System;
using System.Collections.Generic;
using System.IO;

class Journal
{
    private List<Entry> _entries;

    public Journal()
    {
        _entries = new List<Entry>();
    }

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void DisplayAll()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("No entries in your journal yet.");
            return;
        }

        Console.WriteLine("\n========== Your Journal ==========");
        foreach (Entry entry in _entries)
        {
            Console.WriteLine(entry.ToString());
        }
        Console.WriteLine("==================================\n");
    }

    public void SaveToFile(string filename)
    {
        try
        {
            using (StreamWriter outputFile = new StreamWriter(filename))
            {
                foreach (Entry entry in _entries)
                {
                    outputFile.WriteLine(entry.ToFileFormat());
                }
            }
            Console.WriteLine($"Journal saved to {filename}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving file: {ex.Message}");
        }
    }

    public void LoadFromFile(string filename)
    {
        try
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine($"File {filename} not found.");
                return;
            }

            _entries.Clear();
            string[] lines = System.IO.File.ReadAllLines(filename);

            foreach (string line in lines)
            {
                string[] parts = line.Split("~|~");

                if (parts.Length == 3)
                {
                    string date = parts[0];
                    string prompt = parts[1];
                    string response = parts[2];

                    Entry entry = new Entry(prompt, response, date);
                    _entries.Add(entry);
                }
            }

            Console.WriteLine($"Journal loaded from {filename}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
        }
    }
}
