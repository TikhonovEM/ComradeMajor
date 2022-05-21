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
            info[scan.ActiveProcess] += 60;
        }

        foreach (var (activeProcess, time) in info)
        Console.WriteLine($"Имя приложения - {activeProcess.Name}, потрачено - {time / 60d} мин.");
    }
} 