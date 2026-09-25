using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureMemorizer
{
    /// <summary>
    /// Represents a scripture: a reference plus the text, broken up into
    /// individual Word objects. Responsible for hiding words and for
    /// rendering the full scripture (reference + text) for display.
    /// </summary>
    public class Scripture
    {
        private readonly Reference _reference;
        private readonly List<Word> _words;
        private static readonly Random _random = new Random();

        public Scripture(Reference reference, string text)
        {
            _reference = reference;
            _words = text
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(w => new Word(w))
                .ToList();
        }

        /// <summary>
        /// Hides up to <paramref name="count"/> words that are not already
        /// hidden, chosen at random. If fewer than <paramref name="count"/>
        /// words remain visible, all remaining visible words are hidden.
        /// </summary>
        public void HideRandomWords(int count)
        {
            List<Word> eligibleWords = _words.Where(w => !w.IsHidden).ToList();

            for (int i = 0; i < count && eligibleWords.Count > 0; i++)
            {
                int index = _random.Next(eligibleWords.Count);
                eligibleWords[index].Hide();
                eligibleWords.RemoveAt(index);
            }
        }

        /// <summary>
        /// True once every word in the scripture has been hidden.
        /// </summary>
        public bool AllWordsHidden()
        {
            return _words.All(w => w.IsHidden);
        }

        public override string ToString()
        {
            string verseText = string.Join(" ", _words.Select(w => w.ToString()));
            return $"{_reference}{Environment.NewLine}{verseText}";
        }
    }
}
