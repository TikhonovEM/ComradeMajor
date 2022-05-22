using ComradeMajor.Interfaces.Client;
using ComradeMajor.Interfaces;

namespace MyPlugin;

public class MyClientPlugin : IClientPlugin
{
    public string Identifier { get; set; } = "ProcessExplorerPlugin";
    public string Name { get; set; } = "Группировка по ключевой фразе";
    public string Description { get; set; } = "Статистика затраченного времени в определенном приложении";

    public void ProcessInfo(IEnumerable<IScanResult> scanResults)
    {
        Console.Write("Введите название приложения: ");
        var input = Console.ReadLine();

        var resultsByProcess = scanResults.Where(r => r.ActiveProcess?.Name?.Contains(input) == true);

        var spentTime = resultsByProcess.Sum(r => r.ScanPeriod);

        Console.WriteLine($"Всего в приложении '{input}' потрачено {Math.Round(spentTime / 60d, 2)} мин.");
    }
}
