using ComradeMajor.Interfaces;
using Newtonsoft.Json;
using Microsoft.Data.Sqlite;

namespace ComradeMajor;

public class StatisticsRepository : IStatisticsRepository<IScanResult>
{
    private readonly List<IScanResult> _results;
    private readonly ILogger<StatisticsRepository> _logger;

    public StatisticsRepository(ILogger<StatisticsRepository> logger)
    {
        _results = new List<IScanResult>();
        _logger = logger;
    }
    public void AddResult(IScanResult scanResult)
    {
        _results.Add(scanResult);
    }

    public async Task SaveAsync()
    {
        if (!_results.Any())
            return;
        var resultsSQLText = _results.Select(r => r.ToString());
        using (var connection = new SqliteConnection("Data Source=stats.db"))
        {
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText =
            @$"
                INSERT INTO Stats (PluginIdentifier, Date, ActiveProcess, Processes)
                VALUES {string.Join(",\n", resultsSQLText)};
            ";
            await command.ExecuteNonQueryAsync();

            await connection.CloseAsync();
        }       
        _results.Clear();
    }

    public void Save()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IScanResult> GetStatsByDate(DateTime date)
    {
        return Array.Empty<IScanResult>();
    }
}