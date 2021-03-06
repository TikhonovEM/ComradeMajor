using ComradeMajor;
using ComradeMajor.Interfaces;
using System.Runtime.InteropServices;

await Initializer.ExecuteAsync();

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();

        services.Configure<AppSettings>(hostContext.Configuration.GetSection("Settings"));

        services.AddTransient<IStatisticsRepository<IScanResult>, StatisticsRepository>();

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
