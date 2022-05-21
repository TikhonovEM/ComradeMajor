using System.Diagnostics;
using ComradeMajor.Interfaces;

namespace ComradeMajor;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IStatisticsRepository<IScanResult> _repo;
    private readonly PluginManager _pluginManager;

    public Worker(ILogger<Worker> logger, PluginManager pluginManager, IStatisticsRepository<IScanResult> repository)
    {
        _logger = logger;
        _repo = repository;
        _pluginManager = pluginManager;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var context = new HostContext();
            context.Logger = _logger;
            context.Repository = _repo;
            foreach (var plugin in _pluginManager.GetScanPlugins())
            {
                plugin.Execute(context);
            }
            await context.Repository.SaveAsync();
            await Task.Delay(60000, stoppingToken);
        }
    }
}
