using ComradeMajor.Interfaces;
using ComradeMajor.Interfaces.Client;

namespace ComradeMajor;

public class ProcessExplorerClientPlugin : IClientPlugin
{
    public ProcessExplorerClientPlugin() {}

    public ProcessExplorerClientPlugin(string name, string description) : this()
    {
        Name = name;
        Description = description;
    }
    public string Identifier { get; set; } = "ProcessExplorerPlugin";

    public string Name { get; set; }
    public string Description { get; set;}

    public void ProcessInfo(IEnumerable<IScanResult> scanResults)
    {
        // Запоминаем все активные процессы на каждый момент скана.
        var info = new Dictionary<IProcessInfo, long>();

        foreach (var scan in scanResults)
        {
            if (!info.Any(p => p.Key.Equals(scan.ActiveProcess)))
                info.Add(scan.ActiveProcess, 0);
            info[scan.ActiveProcess] += scan.ScanPeriod;
        }

        foreach (var (activeProcess, time) in info)
        Console.WriteLine($"Имя приложения - {activeProcess.Name}, потрачено - {Math.Round(time / 60d, 2)} мин.");
    }
} 