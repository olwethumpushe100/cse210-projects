using System;
using System.Collections.Generic;

/*
 * JOURNAL PROGRAM - CSE 210
 * 
 * CORE REQUIREMENTS MET:
 * - Write new entries with random prompts
 * - Display all journal entries with date and prompt
 * - Save journal to file with custom filename
 * - Load journal from file
 * - Menu system for user navigation
 * - At least 5 prompts in the list
 * - Entry and Journal classes with proper abstraction
 * - Proper naming conventions (TitleCase classes/methods, _underscoreCamelCase member vars)
 * 
 * EXCEEDS REQUIREMENTS:
 * - Error handling for file operations (catch exceptions for missing files, etc.)
 * - User-friendly formatted output with visual separators
 * - Input validation and helpful error messages
 * - Proper use of classes with single responsibility (Entry handles entry data, Journal handles collection)
 */

class Program
{
    static void Main(string[] args)
    {
        Journal myJournal = new Journal();
        List<string> prompts = GetPromptList();
        Random random = new Random();

        bool continueRunning = true;

        Console.WriteLine("Welcome to the Journal Program!");
        Console.WriteLine("===============================\n");

        while (continueRunning)
        {
            DisplayMenu();
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    WriteNewEntry(myJournal, prompts, random);
                    break;

                case "2":
                    myJournal.DisplayAll();
                    break;

                case "3":
                    SaveJournal(myJournal);
                    break;

                case "4":
                    LoadJournal(myJournal);
                    break;

                case "5":
                    continueRunning = false;
                    Console.WriteLine("Thank you for journaling today. Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.\n");
                    break;
            }
        }
    }

    static void DisplayMenu()
    {
        Console.WriteLine("What would you like to do?");
        Console.WriteLine("1. Write a new entry");
        Console.WriteLine("2. Display the journal");
        Console.WriteLine("3. Save the journal to a file");
        Console.WriteLine("4. Load the journal from a file");
        Console.WriteLine("5. Quit");
        Console.Write("Enter your choice: ");
    }

    static List<string> GetPromptList()
    {
        List<string> promptList = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?",
            "What are three things I'm grateful for today?",
            "What challenge did I overcome today and what did I learn from it?",
            "How did I show kindness to someone today?"
        };

        return promptList;
    }

    static void WriteNewEntry(Journal journal, List<string> prompts, Random random)
    {
        string prompt = prompts[random.Next(prompts.Count)];
        Console.WriteLine($"\n{prompt}");
        Console.Write("Your response: ");
        string response = Console.ReadLine();

        string date = DateTime.Now.ToShortDateString();
        Entry newEntry = new Entry(prompt, response, date);
        journal.AddEntry(newEntry);

        Console.WriteLine("Entry saved!\n");
    }

    static void SaveJournal(Journal journal)
    {
        Console.Write("\nWhat is the filename? ");
        string filename = Console.ReadLine();
        journal.SaveToFile(filename);
        Console.WriteLine();
    }

    static void LoadJournal(Journal journal)
    {
        Console.Write("\nWhat is the filename? ");
        string filename = Console.ReadLine();
        journal.LoadFromFile(filename);
        Console.WriteLine();
    }
}
