using System.Runtime.InteropServices;

namespace NativeAdder;

public class Program
{
    private static System.Timers.Timer _timer = new (1000);
    private static Thread _thread;
    private static bool _isRunning;

    [UnmanagedCallersOnly(EntryPoint = "Add")]
    public static int Add(int a, int b)
    {
        _timer.AutoReset = true;
        _timer.Enabled = true;
        _timer.Elapsed += (x, y) => { Console.WriteLine("delegate tiggered"); };
        System.Threading.Thread.Sleep(10000);
        //_timer.Enabled = false;

        _thread = new Thread(new ThreadStart(KeepThreadAlive));
        _isRunning = true;
        _thread.Start();

        return a + b;
    }

     static void KeepThreadAlive()
    {
        while (true)
        {
            Console.WriteLine("Thread is still running...");
            Thread.Sleep(1000);  // Sleep for 1 second
        }
    }
}
