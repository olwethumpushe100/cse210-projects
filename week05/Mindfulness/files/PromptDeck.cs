using System;
using System.Collections.Generic;

// Hands out random items, never repeating one until all have been used.
public class PromptDeck
{
    private List<string> _items;
    private List<string> _remaining;
    private Random _random;

    public PromptDeck(List<string> items)
    {
        _items = items;
        _remaining = new List<string>();
        _random = new Random();
    }

    public string GetNext()
    {
        if (_remaining.Count == 0)
        {
            _remaining = new List<string>(_items);
        }

        int index = _random.Next(_remaining.Count);
        string item = _remaining[index];
        _remaining.RemoveAt(index);

        return item;
    }
}
