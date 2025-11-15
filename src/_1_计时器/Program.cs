namespace _1_计时器;

public class Program
{

    public static void Main(string[] args)
    {
        Console.WriteLine($"MainThread[{Thread.CurrentThread.ManagedThreadId:00}]:StartTest");
        // Test1().Wait();
        Test2().Wait();
        // Test3().Wait();
        Console.ReadLine();
    }

    /// <summary>
    /// 使用循环达到计时的目的,
    /// 但是每次都会得到更长的计时，无法得到准确的计时，但不会有线程安全问题
    /// </summary>
    public static async Task Test1()
    {
        while (true)
        {
            Console.WriteLine($"Tick {DateTime.Now:HH:mm:ss.fff}");
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }

    public static int J = 0;
    /// <summary>
    /// 使用官方所给的Timer达到计时的目的
    /// 使用时中间的代码有可能出现同时有多个线程同时运行的情况（线程安全问题）
    /// </summary>
    /// <returns></returns>
    public static async Task Test2()
    {
        Timer timer = new Timer(async void (state) =>
        {
            J++;
            int k = J;
            // I++;
            int i = 1;
            Console.WriteLine($"Thread[{Thread.CurrentThread.ManagedThreadId:00}]-{k}-Tick {DateTime.Now:HH:mm:ss.fff}");
            for (int j = 0; j < 3; j++)
            {
                Console.WriteLine($"Thread[{Thread.CurrentThread.ManagedThreadId:00}]-{k}-s-{i++}");
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }, null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(2));
    }

    /// <summary>
    /// 周期性的Timer实现计时
    /// 每次计时非常的准确，当进行的任务超出计时器的时间后，不会出现多个线程同时运行的情况
    /// </summary>
    /// <returns></returns>
    public static async Task Test3()
    {
        PeriodicTimer periodicTimer = new PeriodicTimer(TimeSpan.FromSeconds(1));
        while (await periodicTimer.WaitForNextTickAsync())
        {
            Console.WriteLine($"Tick {DateTime.Now:HH:mm:ss.fff}");
            await Task.Delay(TimeSpan.FromSeconds(0.5));
            Console.WriteLine("stop");
        }
    }

}