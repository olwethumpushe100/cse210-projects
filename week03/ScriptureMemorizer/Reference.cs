using System;

namespace ScriptureMemorizer
{
    /// <summary>
    /// Represents the reference for a scripture (e.g. "John 3:16" or
    /// "Proverbs 3:5-6"). Handles both a single verse and a verse range.
    /// </summary>
    public class Reference
    {
        private readonly string _book;
        private readonly int _chapter;
        private readonly int _startVerse;
        private readonly int _endVerse;

        /// <summary>
        /// Constructor for a reference that covers a single verse,
        /// e.g. new Reference("John", 3, 16) -> "John 3:16".
        /// </summary>
        public Reference(string book, int chapter, int verse)
            : this(book, chapter, verse, verse)
        {
        }

        /// <summary>
        /// Constructor for a reference that covers a range of verses,
        /// e.g. new Reference("Proverbs", 3, 5, 6) -> "Proverbs 3:5-6".
        /// </summary>
        public Reference(string book, int chapter, int startVerse, int endVerse)
        {
            _book = book;
            _chapter = chapter;
            _startVerse = startVerse;
            _endVerse = endVerse;
        }

        public override string ToString()
        {
            if (_startVerse == _endVerse)
            {
                return $"{_book} {_chapter}:{_startVerse}";
            }

            return $"{_book} {_chapter}:{_startVerse}-{_endVerse}";
        }
    }
}
