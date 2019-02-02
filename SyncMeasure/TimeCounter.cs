using System;
using System.Diagnostics;


namespace SyncMeasure
{
    public static class TimeCounter
    {
        private static readonly Stopwatch StopWatch = new Stopwatch();


        public static void Start()
        {
            if (StopWatch.IsRunning)
                StopWatch.Restart();
            else StopWatch.Start();
        }

        public static void Stop()
        {
            StopWatch.Stop();
        }

        public static TimeSpan Elapsed()
        {
            return StopWatch.Elapsed;
        }

        public static string ElapsedString()
        {
            var timeElapsed = "Time passed: ";
            if (StopWatch.Elapsed.Hours > 0)
            {
                timeElapsed += StopWatch.Elapsed.Hours + "h ";
            }
            if (StopWatch.Elapsed.Minutes > 0)
            {
                timeElapsed += StopWatch.Elapsed.Minutes + "m ";
            }
            if (StopWatch.Elapsed.Seconds > 0)
            {
                timeElapsed += StopWatch.Elapsed.Seconds + "s ";
            }
            if (StopWatch.Elapsed.Milliseconds > 0)
            {
                timeElapsed += StopWatch.Elapsed.Milliseconds + "ms";
            }

            return timeElapsed;
        }
    }
}
