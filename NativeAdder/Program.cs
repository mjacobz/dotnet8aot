using System.Runtime.InteropServices;

namespace NativeAdder;

public class Program
{
    private static System.Timers.Timer _timer = new (1000);

    [UnmanagedCallersOnly(EntryPoint = "Add")]
    public static int Add(int a, int b)
    {
        _timer.AutoReset = true;
        _timer.Enabled = true;
        _timer.Elapsed += (x, y) => { Console.WriteLine("delegate tiggered"); };
        System.Threading.Thread.Sleep(10000);
        //_timer.Enabled = false;
        
        return a + b;
    }
}
