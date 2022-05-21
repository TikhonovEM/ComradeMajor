using ComradeMajor;
using System.IO;
using System.Reflection;

public static class Initializer
{
    public static void Execute()
    {
        var pluginPath = Path.Combine(
            Path.GetDirectoryName(Assembly.GetAssembly(typeof(Program)).Location), 
            "Plugins");
        if (!Directory.Exists(pluginPath))
            Directory.CreateDirectory(pluginPath);
    }
}