using ComradeMajor;

Initializer.Execute();

var pluginManager = new PluginManager();
pluginManager.AddPlugin(new ProcessExplorerClientPlugin("Общее", "Основная информация по активности в течение дня"));
pluginManager.LoadPlugins();

var plugins = pluginManager.GetPlugins();

var stats = new StatisticsRepository().GetStatsByDate(DateTime.Now);

Console.WriteLine("СТАТИСТИКА");
foreach (var plugin in plugins)
{
    Console.WriteLine($"\n================================\n\n\n{plugin.Name.ToUpper()}\n{plugin.Description}\n\n");
    plugin.ProcessInfo(stats);
    Console.WriteLine("\n================================\n");

    Console.Read();
}