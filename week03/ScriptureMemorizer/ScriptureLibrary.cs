using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ScriptureMemorizer
{
    /// <summary>
    /// Loads a collection of scriptures from a data file and hands back a
    /// random one. Each line in the file is formatted as:
    ///   Book|Chapter|StartVerse|EndVerse|Text
    /// Use the same value for StartVerse and EndVerse for a single verse.
    /// This is the "exceeds requirements" feature described in Program.cs.
    /// </summary>
    public class ScriptureLibrary
    {
        private readonly List<Scripture> _scriptures;
        private static readonly Random _random = new Random();

        public ScriptureLibrary(string filePath)
        {
            _scriptures = new List<Scripture>();
            LoadFromFile(filePath);
        }

        private void LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Could not find scripture data file at '{filePath}'.");
            }

            foreach (string line in File.ReadAllLines(filePath))
            {
                if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
                {
                    continue;
                }

                string[] parts = line.Split('|');
                if (parts.Length != 5)
                {
                    continue;
                }

                string book = parts[0].Trim();
                int chapter = int.Parse(parts[1].Trim());
                int startVerse = int.Parse(parts[2].Trim());
                int endVerse = int.Parse(parts[3].Trim());
                string text = parts[4].Trim();

                Reference reference = startVerse == endVerse
                    ? new Reference(book, chapter, startVerse)
                    : new Reference(book, chapter, startVerse, endVerse);

                _scriptures.Add(new Scripture(reference, text));
            }
        }

        public bool HasScriptures => _scriptures.Count > 0;

        public Scripture GetRandomScripture()
        {
            if (_scriptures.Count == 0)
            {
                throw new InvalidOperationException("No scriptures were loaded.");
            }

            int index = _random.Next(_scriptures.Count);
            return _scriptures[index];
        }
    }
}
