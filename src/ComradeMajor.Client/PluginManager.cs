using System.Reflection;
using ComradeMajor.Interfaces.Client;
using ComradeMajor.Interfaces;

namespace ComradeMajor;

public class PluginManager
{
    private ICollection<IClientPlugin> _plugins;

    public PluginManager()
    {
        _plugins = new List<IClientPlugin>();
    }

    public void AddPlugin(IClientPlugin plugin)
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
            var pluginClasses = plugin.GetTypes().Where(x => typeof(IClientPlugin).IsAssignableFrom(x));
            foreach (var pluginClass in pluginClasses)
            {
                var pluginInstance = Activator.CreateInstance(pluginClass);
                _plugins.Add(pluginInstance as IClientPlugin);
            }
        }

    }

    public IEnumerable<IClientPlugin> GetPlugins()
    {
        return _plugins;
    }

}