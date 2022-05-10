using ComradeMajor;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<PluginManager>(_ => 
        {
            var manager = new PluginManager();
            manager.AddPlugin(new ProcessExplorerPlugin());
            manager.LoadPlugins();
            return manager;
        });
    })
    .Build();

await host.RunAsync();
