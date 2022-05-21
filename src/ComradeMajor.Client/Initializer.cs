using ComradeMajor;
using System.IO;
using System.Reflection;
using Microsoft.Data.Sqlite;

namespace ComradeMajor;

public static class Initializer
{
    private static void CreatePluginFolder()
    {
        var pluginPath = Path.Combine(
            Path.GetDirectoryName(Assembly.GetAssembly(typeof(Program)).Location), 
            "Plugins");
        if (!Directory.Exists(pluginPath))
            Directory.CreateDirectory(pluginPath);
    }

    public static void Execute()
    {
        CreatePluginFolder();
    }
}