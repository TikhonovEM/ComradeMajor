using ComradeMajor;
using System.IO;
using System.Reflection;
using Microsoft.Data.Sqlite;

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

    private async static Task CreateStatsTableAsync()
    {
        using (var connection = new SqliteConnection("Data Source=stats.db"))
        {
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
                CREATE TABLE IF NOT EXISTS Stats(PluginIdentifier TEXT, ScanPeriod INT, Date TEXT, ActiveProcess TEXT, Processes TEXT);
            ";

            await command.ExecuteNonQueryAsync();

            await connection.CloseAsync();
        }
    }
    public async static Task ExecuteAsync()
    {
        CreatePluginFolder();
        await CreateStatsTableAsync();
    }
}