using System;

namespace ScriptureMemorizer
{
    /// <summary>
    /// Represents a single word within a scripture, including whether it is
    /// currently shown or hidden. The word is responsible for its own
    /// hidden/shown state and for rendering itself accordingly.
    /// </summary>
    public class Word
    {
        private readonly string _text;
        private bool _isHidden;

        public Word(string text)
        {
            _text = text;
            _isHidden = false;
        }

        public bool IsHidden => _isHidden;

        public void Hide()
        {
            _isHidden = true;
        }

        public void Show()
        {
            _isHidden = false;
        }

        /// <summary>
        /// Returns the word as it should currently be displayed: the
        /// original text if shown, or the same text with every letter
        /// replaced by an underscore if hidden (the underscore count
        /// matches the number of letters in the word). Punctuation, such
        /// as commas or periods, is left in place so hidden words still
        /// read naturally in the sentence.
        /// </summary>
        public override string ToString()
        {
            if (!_isHidden)
            {
                return _text;
            }

            var result = new char[_text.Length];
            for (int i = 0; i < _text.Length; i++)
            {
                char c = _text[i];
                result[i] = char.IsLetter(c) ? '_' : c;
            }

            return new string(result);
        }
    }
}
