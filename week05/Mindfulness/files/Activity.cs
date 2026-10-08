using System;
using System.Collections.Generic;
using System.Threading;

public abstract class Activity
{
    private string _name;
    private string _description;
    protected int _duration;

    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
        _duration = 0;
    }

    public string GetName()
    {
        return _name;
    }

    public int GetDuration()
    {
        return _duration;
    }

    // Template method: every activity starts and ends the same way,
    // only the middle part differs.
    public void Run()
    {
        DisplayStartingMessage();
        PerformActivity();
        DisplayEndingMessage();
    }

    protected abstract void PerformActivity();

    protected void DisplayStartingMessage()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to the {_name}.");
        Console.WriteLine();
        Console.WriteLine(_description);
        Console.WriteLine();

        _duration = AskForDuration();

        Console.Clear();
        Console.WriteLine("Get ready...");
        ShowSpinner(5);
        Console.WriteLine();
    }

    protected void DisplayEndingMessage()
    {
        Console.WriteLine();
        Console.WriteLine("Well done!!");
        ShowSpinner(3);
        Console.WriteLine();
        Console.WriteLine($"You have completed another {_duration} seconds of the {_name}.");
        ShowSpinner(5);
    }

    private int AskForDuration()
    {
        int seconds = 0;
        bool valid = false;

        while (!valid)
        {
            Console.Write("How long, in seconds, would you like for your session? ");
            string input = Console.ReadLine();
            valid = int.TryParse(input, out seconds) && seconds > 0;

            if (!valid)
            {
                Console.WriteLine("Please enter a whole number greater than zero.");
            }
        }

        return seconds;
    }

    protected void ShowSpinner(int seconds)
    {
        List<string> frames = new List<string> { "|", "/", "-", "\\" };
        DateTime endTime = DateTime.Now.AddSeconds(seconds);
        int index = 0;

        while (DateTime.Now < endTime)
        {
            Console.Write(frames[index]);
            Thread.Sleep(250);
            Console.Write("\b \b");

            index = (index + 1) % frames.Count;
        }
    }

    protected void ShowCountDown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            string number = i.ToString();
            Console.Write(number);
            Thread.Sleep(1000);
            Console.Write(new string('\b', number.Length));
            Console.Write(new string(' ', number.Length));
            Console.Write(new string('\b', number.Length));
        }
    }

    protected int SecondsRemaining(DateTime endTime)
    {
        return (int)Math.Ceiling((endTime - DateTime.Now).TotalSeconds);
    }
}
