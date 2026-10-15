/*
 * Exceeding the core requirements:
 * 1. Leveling system: every 1000 points raises your level, and each level has a
 *    title (from "Wandering Pilgrim" up to "Legendary Ninja Unicorn"). Your level
 *    and progress to the next level are shown with your score, and a LEVEL UP
 *    message appears when you cross a level boundary. Level is calculated from
 *    the score, so it is restored automatically when you load a file.
 * 2. Negative goals: a fourth kind of goal (NegativeGoal) for bad habits.
 *    Recording one subtracts points. It is saved and loaded like the others.
 * 3. Safer saving/loading: a bad or missing file does not erase your current
 *    goals, and input is checked so typing letters instead of numbers doesn't crash.
 */
class Program
{
    static void Main(string[] args)
    {
        GoalManager goalManager = new GoalManager();
        goalManager.Start();
    }
}
