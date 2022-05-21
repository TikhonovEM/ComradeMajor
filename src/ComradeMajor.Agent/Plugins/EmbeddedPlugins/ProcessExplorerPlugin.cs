using ComradeMajor.Interfaces.Agent;
using ComradeMajor.Interfaces;
using ComradeMajor.Models;
using System.Diagnostics;
using System.Linq;

namespace ComradeMajor;

public class ProcessExplorerPlugin : IScanPlugin
{
    private readonly IWindowGetter _windowGetter;

    public ProcessExplorerPlugin(IWindowGetter windowGetter)
    {
        _windowGetter = windowGetter;
    }
    public void Execute(IHostContext context)
    {
        var processes = Process.GetProcesses().Where(p => p.MainWindowHandle != IntPtr.Zero && !string.IsNullOrWhiteSpace(p.MainWindowTitle));
        var scanResult = new ScanResult()
        {
            Date = DateTime.Now
        };

        scanResult.ActiveProcess = _windowGetter.GetActiveWindow();

        scanResult.Processes.AddRange(_windowGetter.GetOpenedWindows());

        context.Repository.AddResult(scanResult);
    }
}