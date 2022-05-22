using ComradeMajor.Interfaces.Agent;
using ComradeMajor.Interfaces;
using ComradeMajor.Models;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.Options;

namespace ComradeMajor;

public class ProcessExplorerPlugin : IScanPlugin
{
    private readonly IWindowGetter _windowGetter;
    private readonly AppSettings _settings;

    public string Identifier { get; set; } = "ProcessExplorerPlugin";

    public ProcessExplorerPlugin(IWindowGetter windowGetter, IOptions<AppSettings> settings)
    {
        _windowGetter = windowGetter;
        _settings = settings.Value;
    }
    public void Execute(IHostContext context)
    {
        var processes = Process.GetProcesses().Where(p => p.MainWindowHandle != IntPtr.Zero && !string.IsNullOrWhiteSpace(p.MainWindowTitle));
        var scanResult = new ScanResult();

        scanResult.PluginIdentifier = Identifier;
        scanResult.ScanPeriod = _settings.ScanPeriod;
        scanResult.Date = DateTime.Now;

        scanResult.ActiveProcess = _windowGetter.GetActiveWindow();

        scanResult.Processes.AddRange(_windowGetter.GetOpenedWindows());

        context.Repository.AddResult(scanResult);
    }
}