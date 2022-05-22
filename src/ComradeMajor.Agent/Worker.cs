using System.Diagnostics;
using ComradeMajor.Interfaces;
using Microsoft.Extensions.Options;

namespace ComradeMajor;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IStatisticsRepository<IScanResult> _repo;
    private readonly PluginManager _pluginManager;
    private readonly AppSettings _settings;

    public Worker(ILogger<Worker> logger, PluginManager pluginManager, IStatisticsRepository<IScanResult> repository, IOptions<AppSettings> appSettings)
    {
        _logger = logger;
        _repo = repository;
        _pluginManager = pluginManager;
        _settings = appSettings.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var delayInSeconds = _settings.ScanPeriod * 1000;
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
            await Task.Delay(delayInSeconds, stoppingToken);
        }
    }
}
