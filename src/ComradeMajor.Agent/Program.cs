using ComradeMajor;
using ComradeMajor.Interfaces;
using System.Runtime.InteropServices;

Initializer.Execute();

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();

        services.AddTransient<IStatisticsRepository<IScanResult>, StatisticsRepository<IScanResult>>();

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            services.AddSingleton<IWindowGetter, WindowsWindowGetter>();
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            services.AddSingleton<IWindowGetter, LinuxWindowGetter>();
       
        services.AddTransient<ProcessExplorerPlugin>();

        services.AddSingleton<PluginManager>(_ => 
        {
            var manager = new PluginManager();
            manager.AddPlugin(services.BuildServiceProvider().GetService<ProcessExplorerPlugin>());
            manager.LoadPlugins();
            return manager;
        });
    })
    .Build();

await host.RunAsync();
