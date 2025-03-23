using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("program starts");

        Task longTask = LongTask();

        Task<string> shortTask = ShortTask();

        LongSyncTask();

        ShortSyncTask();

        // await Task.CompletedTask;

        await longTask;

        Task<List<int>> getNumberListTask = GetNumberList();

        string shortresult = await shortTask;

        Console.WriteLine(shortresult);

        List<int> numList = await getNumberListTask;

        _ = Task.Run(LongSyncTask);

        // Task[] tasks = {longTask, shortTask};
        // Task.WaitAll(tasks);

        foreach (var item in numList) Console.WriteLine(item);

        Console.WriteLine("fin.");
    }

    static async Task<string> ShortTask() {
        Console.WriteLine("short async task starts");
        for (int i = 1; i <= 3; i++) {
            await Task.Delay(1000);
            Console.WriteLine($"short async sec {i}");
        }
        Console.WriteLine("short async task done");
        return "result accessible";
    }

    static async Task LongTask() {
        Console.WriteLine("long async task starts");
        for (int i = 1; i <= 7; i++) {
            await Task.Delay(1000);
            Console.WriteLine($"long async sec {i}");
        }
        Console.WriteLine("long async task done");
    }

    static void ShortSyncTask() {
        Console.WriteLine("short sync task starts");
        for (int i = 1; i <= 4; i++) {
            Task.Delay(1000).Wait(); // blocks
            Console.WriteLine($"short sync sec {i}");
        }
        Console.WriteLine("short sync task done");
    }

    static void LongSyncTask() {
        Console.WriteLine("long sync task starts");
        for (int i = 1; i <= 8; i++) {
            Task.Delay(1000); // pointless
            Console.WriteLine($"long sync sec {i}");
        }
        Console.WriteLine("long sync task done");
    }

    static async Task<List<int>> GetNumberList() {
        List<int> numList = [];
        for (int i = 1; i <= 5; i++) {
            await Task.Delay(500);
            numList.Add(i);
        }
        return numList;
    }

}