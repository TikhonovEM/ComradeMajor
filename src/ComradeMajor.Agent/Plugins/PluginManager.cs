using System.Reflection;
using ComradeMajor.Interfaces.Agent;
using ComradeMajor.Interfaces;


namespace ComradeMajor;

public class PluginManager
{
    private ICollection<IAgentPlugin> _plugins;

    public PluginManager()
    {
        _plugins = new List<IAgentPlugin>();
    }

    public void AddPlugin(IAgentPlugin plugin)
    {
        _plugins.Add(plugin);
    }

    public void LoadPlugins()
    {
        LoadPlugins("Plugins");
    }

    public void LoadPlugins(string relativePath)
    {
        var absolutePath = Path.Combine(
            Path.GetDirectoryName(Assembly.GetAssembly(typeof(Program)).Location), 
            relativePath.Replace('\\', Path.DirectorySeparatorChar));

        var dlls = Directory.GetFiles(absolutePath, "*.dll", SearchOption.AllDirectories);
        foreach (var dll in dlls)
        {
            var plugin = Assembly.LoadFrom(dll);
            var pluginClasses = plugin.GetTypes().Where(x => typeof(IAgentPlugin).IsAssignableFrom(x));
            foreach (var pluginClass in pluginClasses)
            {
                var pluginInstance = Activator.CreateInstance(pluginClass);
                _plugins.Add(pluginInstance as IAgentPlugin);
            }
        }

    }

    public IEnumerable<IAgentPlugin> GetScanPlugins()
    {
        return _plugins.Where(p => p is IScanPlugin);
    }

    public IEnumerable<IAgentPlugin> GetSummaryPlugins()
    {
        return _plugins.Where(p => p is ISummaryPlugin);
    }
}