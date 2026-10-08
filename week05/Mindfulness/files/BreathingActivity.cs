using System;
using System.Threading;

public class BreathingActivity : Activity
{
    private const int BarWidth = 20;
    private const int BreatheInSeconds = 4;
    private const int BreatheOutSeconds = 6;

    public BreathingActivity() : base(
        "Breathing Activity",
        "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    protected override void PerformActivity()
    {
        DateTime endTime = DateTime.Now.AddSeconds(_duration);

        while (SecondsRemaining(endTime) > 0)
        {
            int inSeconds = Math.Min(BreatheInSeconds, SecondsRemaining(endTime));
            Console.WriteLine();
            Console.Write("Breathe in...  ");
            AnimateBreath(inSeconds, true);
            Console.WriteLine();

            if (SecondsRemaining(endTime) <= 0)
            {
                break;
            }

            int outSeconds = Math.Min(BreatheOutSeconds, SecondsRemaining(endTime));
            Console.Write("Breathe out... ");
            AnimateBreath(outSeconds, false);
            Console.WriteLine();
        }
    }

    // A bar grows (in) or shrinks (out). Each step takes a little longer than
    // the last, so the movement is quick at first and slows near the end.
    private void AnimateBreath(int seconds, bool growing)
    {
        int totalMilliseconds = seconds * 1000;
        int weightTotal = BarWidth * (BarWidth + 1) / 2;

        if (!growing)
        {
            Console.Write(new string('#', BarWidth));
        }

        for (int step = 1; step <= BarWidth; step++)
        {
            int delay = totalMilliseconds * step / weightTotal;
            Thread.Sleep(delay);

            if (growing)
            {
                Console.Write("#");
            }
            else
            {
                Console.Write("\b \b");
            }
        }
    }
}
