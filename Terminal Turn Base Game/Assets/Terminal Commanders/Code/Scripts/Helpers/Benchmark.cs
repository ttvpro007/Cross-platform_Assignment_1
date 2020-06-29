using System.Collections.Generic;
using System.Diagnostics;

public static class Benchmark
{
    private static Stopwatch timer = new Stopwatch();
    private static void StartTimer()
    {
        if (timer.IsRunning)
            timer.Stop();

        timer.Reset();
        timer.Start();
    }
    private static void StopTimer(out string time)
    {
        timer.Stop();
        time = timer.ElapsedMilliseconds + " ms";
    }

    public delegate List<T> Func<T>(List<T> list);
    public static List<T> MeasureExecutionTime<T>(List<T> list, out string time, Func<T> func)
    {
        StartTimer();
        List<T> result = func(list);
        StopTimer(out time);
        return result;
    }

    public delegate List<T> Func<T, U>(List<U> list);
    public static List<T> MeasureExecutionTime<T, U>(List<U> list, out string time, Func<T, U> func)
    {
        StartTimer();
        List<T> result = func(list);
        StopTimer(out time);
        return result;
    }

    public delegate void Func();
    public static void MeasureExecutionTime(out string time, Func func)
    {
        StartTimer();
        func();
        StopTimer(out time);
    }
}
