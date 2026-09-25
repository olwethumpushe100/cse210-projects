using System;
using System.IO;

namespace ScriptureMemorizer
{

    // Exceeding the core requirements:
    //
    // 1. Scripture library: instead of a single hard-coded scripture,
    //    this program loads a small library of scriptures from
    //    Data/scriptures.txt (see ScriptureLibrary.cs) and picks one at
    //    random each time the program runs.
    //
    // 2. Only-unhidden-word selection (the stretch challenge): each
    //    time the user presses Enter, HideRandomWords only chooses from
    //    words that are not already hidden, so every keypress reveals
    //    real progress instead of re-hiding a word that's already gone.
    //
    // 3. Punctuation-aware hiding: Word.ToString() only replaces letters
    //    with underscores, leaving punctuation (commas, periods) visible
    //    so the hidden text still reads naturally.


    class Program
    {
        private const int WordsToHidePerStep = 3;

        static void Main(string[] args)
        {
            Scripture scripture = LoadScripture();

            while (true)
            {
                Console.Clear();
                Console.WriteLine(scripture);
                Console.WriteLine();

                if (scripture.AllWordsHidden())
                {
                    break;
                }

                Console.Write("Press Enter to continue, or type 'quit' to exit: ");
                string input = Console.ReadLine();

                if (string.Equals(input?.Trim(), "quit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                scripture.HideRandomWords(WordsToHidePerStep);
            }
        }

        /// <summary>
        /// Loads a random scripture from the data file if it can be found,
        /// otherwise falls back to a single built-in scripture so the
        /// program still runs even if the data file is missing.
        /// </summary>
        private static Scripture LoadScripture()
        {
            string dataPath = Path.Combine(AppContext.BaseDirectory, "Data", "scriptures.txt");

            try
            {
                var library = new ScriptureLibrary(dataPath);
                if (library.HasScriptures)
                {
                    return library.GetRandomScripture();
                }
            }
            catch (Exception)
            {
                // Fall through to the built-in fallback scripture below.
            }

            Reference fallbackReference = new Reference("Proverbs", 3, 5, 6);
            string fallbackText =
                "Trust in the Lord with all thine heart, and lean not unto thine own " +
                "understanding. In all thy ways acknowledge him, and he shall direct thy paths.";
            return new Scripture(fallbackReference, fallbackText);
        }
    }
}
