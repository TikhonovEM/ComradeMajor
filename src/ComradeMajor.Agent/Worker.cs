using System.Diagnostics;

namespace ComradeMajor;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly PluginManager _pluginManager;

    public Worker(ILogger<Worker> logger, PluginManager pluginManager)
    {
        _logger = logger;
        _pluginManager = pluginManager;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var context = new HostContext();
            context.Logger = _logger;
            foreach (var plugin in _pluginManager.GetScanPlugins())
            {
                plugin.Execute(context);
            }     
            await Task.Delay(60000, stoppingToken);
        }
    }
}
