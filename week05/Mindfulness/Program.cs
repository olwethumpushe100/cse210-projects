using System;

/*
 * Exceeding the core requirements:
 * 1. No repeated prompts/questions: the PromptDeck class only repeats a prompt
 *    or question after every one has been used.
 * 2. Saving and loading a log file: every completed activity is appended to
 *    mindfulness_log.txt, and menu option 4 reads that file to show how many
 *    sessions and how many seconds you have spent on each activity (across runs).
 * 3. A more meaningful breathing animation: a bar grows while you breathe in
 *    and shrinks while you breathe out, moving quickly at first and slowing
 *    toward the end of each breath (6 seconds out vs 4 seconds in).
 * 4. Activities never run past the duration: breathing and reflecting
 *    shorten their last step to fit the time left.
 */
class Program
{
    static void Main(string[] args)
    {
        ActivityLog log = new ActivityLog("mindfulness_log.txt");
        string choice = "";

        while (choice != "5")
        {
            Console.Clear();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Start breathing activity");
            Console.WriteLine("  2. Start reflecting activity");
            Console.WriteLine("  3. Start listing activity");
            Console.WriteLine("  4. View activity log");
            Console.WriteLine("  5. Quit");
            Console.Write("Select a choice from the menu: ");
            choice = Console.ReadLine();

            Activity activity = null;

            if (choice == "1")
            {
                activity = new BreathingActivity();
            }
            else if (choice == "2")
            {
                activity = new ReflectingActivity();
            }
            else if (choice == "3")
            {
                activity = new ListingActivity();
            }
            else if (choice == "4")
            {
                log.DisplaySummary();
                Console.WriteLine();
                Console.Write("Press enter to return to the menu.");
                Console.ReadLine();
            }

            if (activity != null)
            {
                activity.Run();
                log.Record(activity.GetName(), activity.GetDuration());
            }
        }

        Console.WriteLine("Thank you for taking time to be mindful.");
    }
}
