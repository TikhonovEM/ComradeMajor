using ComradeMajor.Interfaces;
using Newtonsoft.Json;

namespace ComradeMajor;

public class StatisticsRepository<IScanResult> : IStatisticsRepository<IScanResult>
{
    private readonly List<IScanResult> _results;
    private readonly ILogger<StatisticsRepository<IScanResult>> _logger;

    public StatisticsRepository(ILogger<StatisticsRepository<IScanResult>> logger)
    {
        _results = new List<IScanResult>();
        _logger = logger;
    }
    public void AddResult(IScanResult scanResult)
    {
        _results.Add(scanResult);
    }

    public void Save()
    {
        _logger.LogInformation($"\n================================\n\n\n\nScan Result is {JsonConvert.SerializeObject(_results)}\n\n\n\n==============================");
        _results.Clear();
    }

    public IEnumerable<IScanResult> GetStatsByDate(DateTime date)
    {
        return Array.Empty<IScanResult>();
    }
}